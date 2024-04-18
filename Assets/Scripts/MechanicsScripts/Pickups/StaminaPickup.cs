using UnityEngine;

public class StaminaPickup : Interactable
{
    public float easyStaminaValue = 100f;
    public float normalStaminaValue = 80f;
    public float hardStaminaValue = 60f;
    public float extremeStaminaValue = 40f;

    public override void OnInteract()
    {
        base.OnInteract();
        var player = OmnicatLabs.CharacterControllers.CharacterController.Instance;

        // Determine the stamina value based on difficulty
        float staminaValue = GetStaminaValue();

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
    private float GetStaminaValue()
    {
        switch (DifficultySelector.Instance.currentDifficulty)
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
