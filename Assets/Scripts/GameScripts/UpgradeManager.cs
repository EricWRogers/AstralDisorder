using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static List<UpgradeIds> ownedUpgrades = new List<UpgradeIds>();

    private List<UpgradeIds> savedUpgrades = new List<UpgradeIds>();

    private void Start()
    {
        SaveManager.Instance.onSave.AddListener(SaveUpgrades);
        SaveManager.Instance.onReset.AddListener(ResetUpgrades);
    }

    public static void AddToOwned(UpgradeIds upgrade)
    {
        ownedUpgrades.Add(upgrade);
        Debug.Log(ownedUpgrades.Count);
    }

    public static bool Owns(UpgradeIds upgradeToQuery)
    {
        return ownedUpgrades.Contains(upgradeToQuery);
    }

    public void SaveUpgrades()
    {
        savedUpgrades.Clear();
        savedUpgrades.AddRange(ownedUpgrades);
    }

    public void ResetUpgrades()
    {
        foreach (var upgrade in ownedUpgrades)
        {
            switch (upgrade)
            {
                case UpgradeIds.WallRunning:
                    OmnicatLabs.CharacterControllers.CharacterController.Instance.wallRunningUnlocked = false;
                    break;
                case UpgradeIds.Grapple:
                    OmnicatLabs.CharacterControllers.CharacterController.Instance.grappleUnlocked = false;
                    break;
            }
        }

        ownedUpgrades.Clear();
        ownedUpgrades.AddRange(savedUpgrades);

        foreach (var upgrade in savedUpgrades)
        {
            switch (upgrade)
            {
                case UpgradeIds.WallRunning:
                    OmnicatLabs.CharacterControllers.CharacterController.Instance.wallRunningUnlocked = true;
                    break;
                case UpgradeIds.Grapple:
                    OmnicatLabs.CharacterControllers.CharacterController.Instance.grappleUnlocked = true;
                    break;
            }
        }
    }
}
