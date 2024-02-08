using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickup : Interactable
{
    public float normalStaminaValue = 10f;
    public float easyStaminaValue = 20f;
    public float hardStaminaValue = 6f;
    public float extremeStaminaValue = 4f;

    public override void OnInteract()
    {
        base.OnInteract();
        var player = OmnicatLabs.CharacterControllers.CharacterController.Instance;

        // Get the current difficulty level
        DifficultyLevel difficulty = GameManager.Instance.GetDifficultyLevel();

        // Determine the stamina value based on difficulty
        float staminaValue = GetStaminaValue(difficulty);

        if (player.currentStamina + staminaValue > player.maxStamina)
        {
            player.ChangeStamina(player.maxStamina);
        }
        else
        {
            player.ChangeStamina(player.currentStamina + staminaValue);
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    // Function to get stamina value based on difficulty level
    private float GetStaminaValue(DifficultyLevel difficulty)
    {
        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                return easyStaminaValue;
            case DifficultyLevel.Normal:
                return normalStaminaValue;
            case DifficultyLevel.Hard:
                return hardStaminaValue;
            case DifficultyLevel.Extreme:
                return extremeStaminaValue;
            default:
                return normalStaminaValue; // Default to normal stamina value
        }
    }
}
