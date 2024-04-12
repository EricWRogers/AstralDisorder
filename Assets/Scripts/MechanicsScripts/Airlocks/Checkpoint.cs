using UnityEngine;
using UnityEngine.AI;

public class Checkpoint : MonoBehaviour
{
    public bool shouldAITransition = true;
    public static Transform spawnpoint;
    private AIChaseState agent;
    public Animator animator;
    [StringDropdown(EditorStringLists.BuildScenes)]
    public int sceneToLoad;
    [StringDropdown(EditorStringLists.BuildScenes)]
    public int sceneToUnload;

    private void Start()
    {
         agent = FindAnyObjectByType<AIChaseState>();
    }
    private void OnTriggerEnter(Collider other)
    {
        SaveManager.Instance.Save();
        spawnpoint = transform;
        OmnicatLabs.CharacterControllers.CharacterController.Instance.savedStamina = OmnicatLabs.CharacterControllers.CharacterController.Instance.currentStamina;

        animator.SetTrigger("Saving");
        //if (shouldAITransition)
        //    agent.checkPoint = true;

        LoadManager.ChangeScenes(sceneToLoad, sceneToUnload).AddListener(HandleAfterLoad);
    }

    private void HandleAfterLoad()
    {
        Debug.Log("Load Finished");
    }
}
