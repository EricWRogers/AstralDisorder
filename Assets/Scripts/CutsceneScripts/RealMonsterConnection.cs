using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealMonsterConnection : MonoBehaviour
{
    public void StartMonsterGrowling()
    {
        GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<MonsterAudio>().StartGrowls();
    }
}
