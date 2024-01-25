using UnityEngine;

public class ChangeObjective : MonoBehaviour
{
    public int priority = 0;
    [TextArea]
    public string newObjective;
    public void Change()
    {
        if (priority > ObjectiveText.instance.currObjectivePriority)
        {
            ObjectiveText.instance.SetCurrObjective(newObjective);
            ObjectiveText.instance.SetCurrObjectivePriority(priority);
        }
    }
}
