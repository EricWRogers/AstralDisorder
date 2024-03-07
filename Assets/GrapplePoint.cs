using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplePoint : MonoBehaviour
{
    [Tooltip("The range at which the point becomes visible. Note this should be bigger than the player's grapple distance")]
    public float detectionRadius = 25f;
    [Tooltip("The range at which the point becomes invisible")]
    public float hideDistance = 5f;
    [Tooltip("Parent that holds the on/off mechanics of the point")]
    public GameObject toggleObject;
    [Tooltip("The fill image that is scaled on distance")]
    public Image scalingImage;
    [Tooltip("Describes what layers are affected by the raycast that checks whether the player is valid to grapple")]
    public LayerMask validObstaceFilter;
    public UIStateMachineController tutorialStateMachine;
    public Collider col;

    private bool firstTime1 = true;
    private bool firstTime2 = true;
    private OmnicatLabs.CharacterControllers.CharacterController player;

    private void Start()
    {
        player = OmnicatLabs.CharacterControllers.CharacterController.Instance;
        if (!player.grappleUnlocked)
        {
            toggleObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (player.grappleUnlocked)
        {
            //finding the normalized distance to use in the lerp
            //uses the detection radius as the maximum and the player grapple distance as the minimum
            var distance = Vector3.Distance(player.transform.position, transform.position);
            var norm = (distance - player.maxGrappleDistance) / (detectionRadius - player.maxGrappleDistance);
            norm = Mathf.Clamp01(norm);

            var maxScale = Vector3.one * .95f;
            var minScale = Vector3.one * .01f;
            scalingImage.GetComponent<RectTransform>().localScale = Vector3.Lerp(maxScale, minScale, norm);

            if (Vector3.Distance(transform.position, player.transform.position) <= hideDistance)
            {
                toggleObject.SetActive(false);
                if (!firstTime1 && !firstTime2)
                {
                    tutorialStateMachine.ChangeState(tutorialStateMachine.nullState);
                }
            }
            else if (scalingImage.GetComponent<RectTransform>().localScale == minScale)
            {
                toggleObject.SetActive(false);
            }
            else if (scalingImage.GetComponent<RectTransform>().localScale == maxScale)
            {
                if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out RaycastHit hit, Mathf.Infinity, validObstaceFilter) && hit.transform.CompareTag(player.tag))
                {
                    toggleObject.SetActive(true);
                    scalingImage.color = Color.green;
                    if (firstTime2 && tutorialStateMachine != null)
                    {
                        firstTime2 = false;
                        tutorialStateMachine.ChangeState<TutorialSecondaryGrappleState>();
                    }
                }
                else
                {
                    col.enabled = false;
                    scalingImage.color = Color.red;
                }
            }
            else
            {
                if (firstTime1 && tutorialStateMachine != null)
                {
                    firstTime1 = false;
                    tutorialStateMachine.ChangeState<TutorialGrappleState>();
                }
                toggleObject.SetActive(true);
                scalingImage.color = Color.white;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
