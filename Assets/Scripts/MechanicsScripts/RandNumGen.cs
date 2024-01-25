using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandNumGen : MonoBehaviour
{
    public string RandNum;
    public TMP_Text numText;

    public float test = 10f;
    public bool generateOnStart = true;

    void Start()
    {
        if (generateOnStart)
            Generate();
    }

    public virtual void Generate()
    {
        RandNum = Random.Range(1111, 9999).ToString();
        Debug.Log(RandNum);
        numText.SetText(RandNum.ToString());
    }
}
