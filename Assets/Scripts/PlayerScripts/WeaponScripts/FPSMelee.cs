using UnityEngine;
using UnityEngine.InputSystem;
using OmnicatLabs.Timers;
using OmnicatLabs.Audio;

public class FPSMelee : MonoBehaviour
{
    public float attackDistance = 1f;
    public float attackCooldown = .5f;
    public LayerMask interactionLayers;

    private bool shouldAttack = false;
    private bool canAttack = true;
    private int attackCount = 0;
    private Fracture fractureComponent;
    private Camera cam;

    public GameObject sparks;

    private void Start()
    {
        cam = OmnicatLabs.CharacterControllers.CharacterController.Instance.mainCam;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shouldAttack = true;
        }

        if (context.canceled)
        {
            shouldAttack = false;
        }
    }

    private void Update()
    {
        Attack();
        if (fractureComponent != null)
            fractureComponent.pieces.ForEach((piece) => {
                if (piece != null)
                {
                    piece.transform.localScale *= .998f;
                    if (piece.transform.localScale.magnitude < 1f)
                    {
                        Destroy(piece);
                    }
                }
            });
    }

    private void Attack()
    {
        if (shouldAttack && canAttack)
        {
            canAttack = false;
            TimerManager.Instance.CreateTimer(attackCooldown, () => canAttack = true);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, interactionLayers))
            {
                if (hit.transform.TryGetComponent<Fracture>(out var frac))
                {
                    fractureComponent = frac;
                    frac.CauseFracture(-hit.normal);
                    Instantiate(sparks, hit.point, Random.rotation);
                }

                else if (hit.transform.TryGetComponent<HitToPush>(out var pushable))
                {
                    pushable.Push(-hit.normal);
                    Instantiate(sparks, hit.point, Random.rotation);
                }
            }
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit2, attackDistance, ~(1 << LayerMask.NameToLayer("TriggerZone"))))
            {
                Instantiate(sparks, hit2.point, Random.rotation);
                AudioManager.Instance.Play("Hammer");
            }
            else
            {
                AudioManager.Instance.Play("Whiff");
            }

            if (attackCount == 0)
            {
                ArmController.Instance.ChangeAnimationState("Attack 1");
                attackCount++;
            }
            else
            {
                ArmController.Instance.ChangeAnimationState("Attack 2");
                attackCount = 0;
            }
            Debug.DrawRay(cam.transform.position, cam.transform.forward * attackDistance, Color.green, 10f);
        }
    }
}

