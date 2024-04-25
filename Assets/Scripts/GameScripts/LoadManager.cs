using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadManager : MonoBehaviour
{
    private static LoadManager Instance;
    private UnityEvent onComplete = new UnityEvent();
    public static bool isLoading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //Loading tutorial by default when the game boots
        SceneManager.LoadScene(SpawnLocation.GetStartingScene(), LoadSceneMode.Additive);
    }

    public static UnityEvent ChangeScenes(int sceneToLoad, int sceneToUnload)
    {
        Instance.StartCoroutine(Instance.LoadRoutine(sceneToLoad, sceneToUnload));
        return Instance.onComplete;
    }

    public IEnumerator LoadRoutine(int loadIndex, int unloadIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadIndex, LoadSceneMode.Additive);
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(unloadIndex);
        isLoading = true;

        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }

        onComplete.Invoke();
        isLoading = false;
    }
}
