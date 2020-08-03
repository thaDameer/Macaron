using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StageHandler))]
    public class StageHandlerEditor : Editor 
    {
        public override void OnInspectorGUI() 
        {

            var levelHandler = (StageHandler)target;
            
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Generate startpos",GUILayout.Width(150),GUILayout.Height(50)))
            {
                if(levelHandler.startPosition)
                {
                    Debug.Log("Start position already exists");
                }
                else
                {
                    Debug.Log("Created a start position");
                    levelHandler.CreatePositionObjects("start");
                }
            }
             if(GUILayout.Button("Generate goalpos",GUILayout.Width(150),GUILayout.Height(50)))
            {
                if(levelHandler.goalPosition)
                {
                    Debug.Log("Goal already exists");
                }
                else
                {
                    levelHandler.CreatePositionObjects("goal");
                    Debug.Log("Created a start goal");
                }
            }
            GUILayout.EndHorizontal();
            base.OnInspectorGUI();
            
        }
    }
