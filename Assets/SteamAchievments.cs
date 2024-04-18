using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{
    // Achievement IDs
    public const string ACHIEVEMENT_WIN_GAME = "ACH_WIN_GAME";
    public const string ACHIEVEMENT_GRAPPLING_HOOK = "ACH_GRAPPLING_HOOK";
    public const string ACHIEVEMENT_WALL_RUN_BOOTS = "ACH_WALL_RUNNER";
    public const string ACHIEVEMENT_LOSE_GAME = "ACH_LOSE_GAME";
    public const string ACHIEVEMENT_BEAT_A_DEV = "ACH_BEAT_A_DEV";

    // Record time (in seconds)
    public float recordTime = 300.0f; // Example: 5 minutes
    public static void UnlockAchievement(string achievementID)
    {
        // Check if Steam is initialized
        if (!SteamManager.Initialized)
        {
            return;
        }

        // Unlock achievement
        SteamUserStats.SetAchievement(achievementID);
        SteamUserStats.StoreStats();
    }

    //  method to unlock achievement when player wins the game
    public void ActivateWin()
    {
        UnlockAchievement(ACHIEVEMENT_WIN_GAME);
    }

    //  method to unlock achievement when player obtains the grappling hook
    public void UnlockGrapplingHook()
    {
        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.grappleUnlocked == true)
        {
            UnlockAchievement(ACHIEVEMENT_GRAPPLING_HOOK);
        }
    }

    //  method to unlock achievement when player loses the game
    public void ActivateLose()
    {
      
       UnlockAchievement(ACHIEVEMENT_LOSE_GAME);
        }
           
    //  method to unlock achievement when player obtains the wall run

    public void UnlockWallRunBoots()
    {

        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.wallRunningUnlocked==true)
        {
            UnlockAchievement(ACHIEVEMENT_WALL_RUN_BOOTS);
        }

    }
    // Example method to check if player beats the record time
    public void CheckRecordTime(float playerTime)
    {
        if (playerTime < recordTime)
        {
            UnlockAchievement(ACHIEVEMENT_BEAT_A_DEV);
        }
    }
}

