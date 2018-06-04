using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace LUT
{
    [CustomEditor(typeof(Event))]
    //[CanEditMultipleObjects]
    public class EventInspector : Editor
    {
        Event eTarget;
        void OnEnable()
        {
            // TODO: find properties we want to work with
            //serializedObject.FindProperty();
        }

        public override void OnInspectorGUI()
        {
            eTarget = (Event)target;
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();


            DrawDefaultInspector();

            // TODO: Draw UI here
            EditorGUILayout.HelpBox("There are currently " + eTarget.Count + " listeners registered.", MessageType.Info);

            //var _objAction = typeof(Event).GetField("actions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eTarget);

            //List<Action> actions = (List<Action>)_objAction;
            //int c = 0;
            //foreach(Action a in actions)
            //{
            //    if(a == null)
            //    {
            //        c++;
            //    }
            //}
            //if(c!= 0)
            //{
            //    EditorGUILayout.HelpBox("There are " + c + " null listeners on this event.", MessageType.Warning);
            //}

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
