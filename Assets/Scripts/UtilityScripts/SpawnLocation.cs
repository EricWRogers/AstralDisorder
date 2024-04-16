using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        [StringDropdown(EditorStringLists.BuildScenes)]
        public int spawnArea;
        public Transform point;
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public int spawnIndex = 0;
    private static SpawnLocation Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {   
        OmnicatLabs.CharacterControllers.CharacterController.Instance.transform.position = spawnPoints[spawnIndex].point.position;
    }

    public static int GetStartingScene()
    {
        return Instance.spawnPoints[Instance.spawnIndex].spawnArea;
    }
}
