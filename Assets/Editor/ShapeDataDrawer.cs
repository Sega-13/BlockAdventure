using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(ShapeData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class ShapeDataDrawer : Editor
{
    private ShapeData ShapeDataInstance => target as ShapeData;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ClearBoardButton();
        EditorGUILayout.Space();
        DrawColumnsInputFields();
        EditorGUILayout.Space();

        if(ShapeDataInstance.board != null && ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0)
        {
            DrawBoardTable();
        }
        serializedObject.ApplyModifiedProperties();
        if(GUI.changed)
        {
            EditorUtility.SetDirty(ShapeDataInstance);
        }
    }
    private void ClearBoardButton()
    {
        if (GUILayout.Button("Clear Board"))
        {
            ShapeDataInstance.Clear();
        }
    }
    private void DrawColumnsInputFields()
    {
        var columnsTemp = ShapeDataInstance.columns;
        var rowTemp = ShapeDataInstance.rows;

        ShapeDataInstance.columns = EditorGUILayout.IntField("Columns", ShapeDataInstance.columns);
        ShapeDataInstance.rows = EditorGUILayout.IntField("Rows", ShapeDataInstance.rows);

        if((ShapeDataInstance.columns != columnsTemp || ShapeDataInstance.rows != rowTemp)
            && ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0 )
        {
            ShapeDataInstance.CreateNewBoard();
        }
    }

    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10,10);
        tableStyle.margin.left = 32;

        var hearderColumnStyle = new GUIStyle();
        hearderColumnStyle.fixedWidth = 65;
        hearderColumnStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        for(var row = 0; row<ShapeDataInstance.rows; row++)
        {
            EditorGUILayout.BeginHorizontal(hearderColumnStyle);
            for(var column =0 ; column<ShapeDataInstance.columns; column++)
            {
                EditorGUILayout.BeginHorizontal(rowStyle);
                var data = EditorGUILayout.Toggle(ShapeDataInstance.board[row].column[column], dataFieldStyle);
                ShapeDataInstance.board[row].column[column] = data;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
