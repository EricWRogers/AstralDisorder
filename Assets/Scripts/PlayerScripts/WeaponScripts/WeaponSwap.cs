using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwap : MonoBehaviour
{
    public WeaponHolder holder;
    //purely front facing for any weapons that want to be added through editor
    public List<GameObject> weapons;
    //actually used by the system.
    private static List<GameObject> weaponsInternal = new List<GameObject>();

    private float scrollInput;

    private int weaponIndex = 0;
    private bool canSwap = true;
    public static WeaponSwap Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //puts the ones from the front facing list into the one to use.
        weaponsInternal.AddRange(weapons);

        holder.onSwapFinish.AddListener(() => canSwap = true);
    }

    public void OnSwap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            scrollInput = ctx.ReadValue<float>();

            if (canSwap && weaponsInternal.Count > 1)
            {
                if (scrollInput > 0)    //scrolling up
                {
                    weaponIndex += 1;

                    if (weaponIndex > weaponsInternal.Count - 1)    //exceeded the end of the weapon list. go to start
                    {
                        weaponIndex = 0;
                    }

                    canSwap = false;
                    holder.StartSwap(weaponsInternal[weaponIndex]);
                }
                else if (scrollInput < 0)   //scrolling down
                {
                    weaponIndex -= 1;

                    if (weaponIndex < 0)    //exceeded the start of the weapon list. go to end
                    {
                        weaponIndex = weaponsInternal.IndexOf(weaponsInternal[weaponsInternal.Count - 1]);
                    }

                    canSwap = false;
                    holder.StartSwap(weaponsInternal[weaponIndex]);
                }
            }
        }
    }

    public void AddNewWeapon(GameObject weaponPrefab)
    {
        //if hands are empty when adding a new weapon play the animation
        if (weaponsInternal.Count == 0)
        {
            canSwap = false;
            weaponsInternal.Add(weaponPrefab);
            holder.StartSwap(weaponsInternal[weaponIndex], true);
        }
        else weaponsInternal.Add(weaponPrefab);
    }
}
