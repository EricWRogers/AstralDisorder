using UnityEditor;
using System.Collections.Generic;

public static class StringDropdownHelpers
{
#if UNITY_EDITOR

    public static string[] AllBuildScenes()
    {
        var temp = new List<string>();
        foreach (EditorBuildSettingsScene S in EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }

#endif
}
