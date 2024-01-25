using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    private GameObject Spawnpoint;
    public GameObject LoseUI;

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Retry()
    {
        var player = OmnicatLabs.CharacterControllers.CharacterController.Instance;
        Destroy(LoseUI);
        player.SetControllerLocked(false, false, false);
        player.rb.velocity = Vector3.zero;
        player.modelCollider.height = 2f;
        player.camHolder.localPosition = new Vector3(player.camHolder.localPosition.x, player.originalHeight, player.camHolder.localPosition.z);
        player.ChangeState(OmnicatLabs.CharacterControllers.CharacterStates.Idle);
        SaveManager.Instance.ResetTracked();
        player.transform.position = Checkpoint.spawnpoint.position;
        player.transform.rotation = Checkpoint.spawnpoint.rotation;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnDestroy()
    {
        GameStateController.isActive = false;
    }
}
