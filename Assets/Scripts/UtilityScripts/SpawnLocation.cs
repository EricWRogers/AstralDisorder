using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public string name;
        public Transform point;
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public int spawnIndex = 0;

    private void Start()
    {
        OmnicatLabs.CharacterControllers.CharacterController.Instance.transform.position = spawnPoints[spawnIndex].point.position;
    }
}
