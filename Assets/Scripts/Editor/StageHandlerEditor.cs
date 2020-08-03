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
            if(GUILayout.Button("Create startpos",GUILayout.Width(100),GUILayout.Height(50)))
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
             if(GUILayout.Button("Create goalpos",GUILayout.Width(100),GUILayout.Height(50)))
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
            GUILayout.BeginHorizontal();
           if(GUILayout.Button("LeftDeadZone",GUILayout.Width(100),GUILayout.Height(50)))
            {
                if(levelHandler.leftDeadzone)
                {
                    Debug.Log("Deadzone already set");
                }
                else
                {
                    levelHandler.CreateDeadZones(ObjectRotation.LEFT);
                }
            }
             if(GUILayout.Button("RightDeadZone",GUILayout.Width(100),GUILayout.Height(50)))
            {
                if(levelHandler.rightDeadzone)
                {
                    Debug.Log("Deadzone already set");
                }
                else
                {
                    levelHandler.CreateDeadZones(ObjectRotation.RIGHT);
                }
            }
            if(GUILayout.Button("UpDeadZone",GUILayout.Width(100),GUILayout.Height(50)))
            {
                if(levelHandler.upDeadzone)
                {
                    Debug.Log("Deadzone already set");
                }
                else
                {
                    levelHandler.CreateDeadZones(ObjectRotation.UP);
                }
            }
            GUILayout.EndHorizontal();
            base.OnInspectorGUI();
            
        }
    }
