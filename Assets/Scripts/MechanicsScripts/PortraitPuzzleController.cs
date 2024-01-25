using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class PortraitPuzzleController : MonoBehaviour
{
    public List<GameObject> portaits = new List<GameObject>();
    public int totalPortraits;
    public SafeCodeGenerator generator;
    [HideInInspector]
    public List<PortraitData> portraitInstances = new List<PortraitData>();

    private void Start()
    {
        var tempPortraits = new List<GameObject>();
        tempPortraits.AddRange(portaits);
        var positions = GetComponentsInChildren<Transform>();
        for (int i = 0; i < totalPortraits; i++)
        {
            //skip 0 because it is the parent object this script is on
            int randomPortraitIndex = Random.Range(0, tempPortraits.Count);
            var portrait = Instantiate(tempPortraits[randomPortraitIndex], positions[i + 1].position, Quaternion.identity);

            portrait.GetComponentInChildren<TextMeshProUGUI>().SetText($"{portrait.GetComponent<PortraitData>().firstName} {portrait.GetComponent<PortraitData>().lastName}\nStation Manager {i + 1}");
            portrait.GetComponent<PortraitData>().associatedNumber = i + 1;

            portraitInstances.Add(portrait.GetComponent<PortraitData>());
            tempPortraits.RemoveAt(randomPortraitIndex);
        }

        generator.Generate();
    }
}
