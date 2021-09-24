using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CopyGUIDToClipboard : EditorWindow
{

    [MenuItem("Assets/Copy Selected Item GUID to Clipboard", false, 0)]
    public static void GUIDToClipboard()
    {
        string t = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));
        GUIUtility.systemCopyBuffer = t;
        Debug.Log("Current Selection GUID: " + t);
    }
}
