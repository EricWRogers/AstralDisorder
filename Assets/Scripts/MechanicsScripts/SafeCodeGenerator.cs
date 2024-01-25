using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SafeCodeGenerator : RandNumGen
{
    public PortraitPuzzleController controller;
    private Dictionary<string, int> nameAndNumbers = new Dictionary<string, int>();
    public override void Generate()
    {
        foreach (var portrait in controller.portraitInstances)
        {
            nameAndNumbers.Add(portrait.firstName, portrait.associatedNumber);
            nameAndNumbers.Add(portrait.lastName, portrait.associatedNumber);
        }

        int randomNumber = Random.Range(1111, 9999);

        var textComponent = GetComponentInChildren<TextMeshProUGUI>();

        for (int i = 0; i < 4; i++)
        {
            var random = nameAndNumbers.ElementAt(Random.Range(0, nameAndNumbers.Keys.Count));
            textComponent.SetText($"{textComponent.text}\n{random.Key}");
            RandNum += random.Value;
        }
    }
}
