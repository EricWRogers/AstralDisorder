using UnityEngine;

public class StaminaPickup : Interactable
{
    public float easystaminaValue = 20f;
    public float normalstaminaValue = 10f;
    public float hardstaminaValue = 6f;
    public float extremestaminaValue = 4f;

    public override void OnInteract()
    {
        base.OnInteract();
        var player = OmnicatLabs.CharacterControllers.CharacterController.Instance;
        Difficulty difficulty = GameManager.Instance.GetDifficulty(); // Assuming GameManager has a method to get the current difficulty

        if (difficulty == Difficulty.Easy)
        {
            if (player.currentStamina + easystaminaValue > player.maxStamina)
            {
                player.ChangeStamina(player.maxStamina);
            }
            else
            {
                player.ChangeStamina(player.currentStamina + easystaminaValue);
            }
        }
        else if (difficulty == Difficulty.Normal)
        {
            if (player.currentStamina + normalstaminaValue > player.maxStamina)
            {
                player.ChangeStamina(player.maxStamina);
            }
            else
            {
                player.ChangeStamina(player.currentStamina + normalstaminaValue);
            }
        }
        else if (difficulty == Difficulty.Hard)
        {
            if (player.currentStamina + hardstaminaValue > player.maxStamina)
            {
                player.ChangeStamina(player.maxStamina);
            }
            else
            {
                player.ChangeStamina(player.currentStamina + hardstaminaValue);
            }
        }
        else if (difficulty == Difficulty.Extreme)
        {
            if (player.currentStamina + extremestaminaValue > player.maxStamina)
            {
                player.ChangeStamina(player.maxStamina);
            }
            else
            {
                player.ChangeStamina(player.currentStamina + extremestaminaValue);
            }
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
