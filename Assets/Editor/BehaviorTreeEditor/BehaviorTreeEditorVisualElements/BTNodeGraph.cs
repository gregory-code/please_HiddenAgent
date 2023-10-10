using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BTNodeGraph : GraphView
{
    public new class UxmlFactory : UxmlFactory<BTNodeGraph, UxmlTraits> { }
    public BTNodeGraph()
    {
        Insert(0, new GridBackground());
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }
}
