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
        Difficulty? difficulty = FindObjectOfType<DifficultySetting>()?.currentDifficulty;

        if (difficulty != null)
        {
            // Determine the stamina value based on difficulty
            float staminaValue = GetStaminaValue(difficulty.Value);

            if (player.currentStamina + staminaValue > player.maxStamina)
            {
                player.ChangeStamina(player.maxStamina);
            }
            else
            {
                player.ChangeStamina(player.currentStamina + staminaValue);
            }
        }
        else
        {
            Debug.LogError("DifficultySetting script not found in the scene.");
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    // Function to get stamina value based on difficulty level
    private float GetStaminaValue(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return easyStaminaValue;
            case Difficulty.Normal:
                return normalStaminaValue;
            case Difficulty.Hard:
                return hardStaminaValue;
            case Difficulty.Extreme:
                return extremeStaminaValue;
            default:
                return normalStaminaValue; // Default to normal stamina value
        }
    }
}
