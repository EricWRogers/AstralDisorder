using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadManager : MonoBehaviour
{
    private static LoadManager Instance;
    private UnityEvent onComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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

        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }

        onComplete.Invoke();
    }
}
