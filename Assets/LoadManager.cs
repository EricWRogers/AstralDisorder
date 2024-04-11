using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OmnicatLabs.OmniEnum;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System;
using UnityEditor;

public class PathAttribute : Attribute
{
    public string path;
}


public enum Area
{
    [Path]
    Tutorial,
    [Path]
    Hub,
    [Path]
    ControlStation,
    [Path]
    CrewQuarters,
}


public class LoadManager : MonoBehaviour
{
    public List<string> sceneNames;
    private List<string> buildScenes = new List<string>();
    private List<FieldInfo> fields = new List<FieldInfo>();

    private void Start()
    {
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            buildScenes.Add(scene.path);
        }
        fields = typeof(Area).GetFields(BindingFlags.Public | BindingFlags.Static).Where(field => field.IsDefined(typeof(PathAttribute), false)).ToList();

        foreach (var field in fields)
        {
            PathAttribute attr = (PathAttribute)field.GetCustomAttribute(typeof(PathAttribute));
            var name = field.Name.Split(" ").Last();

            attr.path = buildScenes.Find(scenePath => scenePath.Contains(name));
        }

        Debug.Log(GetPathFromEnum(Area.ControlStation));

        //fields.ToList().ForEach(field => Debug.Log(field));

        //for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        //{
        //    buildScenes.Add(SceneManager.GetSceneByName(sceneNames[i]));
        //    //buildScenes.Add(SceneUtility.GetScenePathByBuildIndex(i));
        //}
        //Debug.Log(System.Enum.GetName(typeof(Area), Area.ControlStation));
        //FindSceneByEnum(Area.ControlStation);
    }

    private string GetPathFromEnum(Area _enum)
    {
        var attr = (PathAttribute)fields.Find(field => field.Name.Contains(_enum.ToString())).GetCustomAttribute(typeof(PathAttribute));
        return attr.path;
    }

    public static void ChangeScenes(Area areaToUnload, Area areaToLoad)
    {

    }

    public IEnumerator LoadRoutine()
    {
        yield return null;
    }

    public IEnumerator UnloadRoutine()
    {
        yield return null;
    }
}
