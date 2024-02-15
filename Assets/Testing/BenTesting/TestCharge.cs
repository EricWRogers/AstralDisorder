using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharge : MonoBehaviour, IChargeable
{
    public float chargeLevel = 0f;

    public void OnCharge()
    {
        //chargeLevel++;
        Debug.Log("Charge");
    }
}
