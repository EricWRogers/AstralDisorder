using UnityEngine;

public class DialogueArea : Dialogue
{
    private void Awake()
    {
        if (TryGetComponent(out Collider col))
        {
            if (!col.isTrigger)
            {
                Debug.LogError($"Collider for DialogueArea on {gameObject.name} is not a trigger");
            }
        }
        else
        {
            Debug.LogError($"No collider attached to DialogueArea on {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            TriggerDialogue();
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            CloseDialogue();
    }
    */
}
