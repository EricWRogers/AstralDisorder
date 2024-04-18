using System;
using UnityEngine;
using System.ComponentModel;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum EditorStringLists
{
    [Description("AllBuildScenes")]
    BuildScenes,
}


public class StringDropdown : PropertyAttribute
{
    public delegate string[] GetStringList();

    public StringDropdown(params string[] list)
    {
        List = list;
    }

    public StringDropdown(EditorStringLists listType)
    {
        var field = listType.GetType().GetField(listType.ToString());
        DescriptionAttribute attr = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
        var method = typeof(StringDropdownHelpers).GetMethod(attr.Description);
        if (method != null)
        {
            List = method.Invoke(null, null) as string[];
        }
    }

    public string[] List
    {
        get;
        private set;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(StringDropdown))]
public class StringDropdownDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var stringInList = attribute as StringDropdown;
        var list = stringInList.List;
        if (property.propertyType == SerializedPropertyType.String)
        {
            int index = Mathf.Max(0, Array.IndexOf(list, property.stringValue));
            index = EditorGUI.Popup(position, property.displayName, index, list);

            property.stringValue = list[index];
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, list);
        }
        else
        {
            base.OnGUI(position, property, label);
        }
    }
}
#endif
