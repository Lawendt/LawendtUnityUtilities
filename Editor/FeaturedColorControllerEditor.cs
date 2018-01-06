using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(FeaturedColorController))]
public class FeaturedColorControllerEditor : Editor
{
    ReorderableList listColors;
    FeaturedColorController fcc;

    void OnEnable()
    {
        fcc = (FeaturedColorController)target;
        SetupReorderableList();
    }

    void SetupReorderableList()
    {
        listColors = new ReorderableList(serializedObject, serializedObject.FindProperty("featuredColorsPalettes"));

        listColors.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Featured Colors Palettes");

            if (GUI.Button(new Rect(rect.width - 40, rect.y, 20, rect.height), "+"))
            {
                Undo.RecordObject(fcc, "Added new color palette");
                fcc.featuredColorsPalettes.Add(new FeaturedColorPalette(fcc.colorsPerPalette)); 
            }
            if (GUI.Button(new Rect(rect.width - 20, rect.y, 20, rect.height), "-"))
            {
                Undo.RecordObject(fcc, "Remove last color palette");
                fcc.featuredColorsPalettes.RemoveAt(fcc.featuredColorsPalettes.Count - 1);
            }
            //ReorderableList.defaultBehaviours.DoRemoveButton(listColors);      
        };

        listColors.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = listColors.serializedProperty.GetArrayElementAtIndex(index);
            var subElement = element.FindPropertyRelative("featuredColors");
            
            if (subElement == null)
            {
                Debug.Log("SubElemente null");
                return;
            }

            Rect numberRect = new Rect(rect);
            numberRect.width = 20;

            EditorGUI.LabelField(numberRect, index.ToString());

            Rect tempRect = new Rect(rect);
            float eachSize = (rect.width - 20) / subElement.arraySize;
            tempRect.x = rect.x + 20;
            tempRect.width = eachSize; 

            for (int i = 0; i < subElement.arraySize; i++)
            {                    
                EditorGUI.PropertyField(tempRect, subElement.GetArrayElementAtIndex(i), GUIContent.none);
                tempRect.x += eachSize;
            }
        };

        listColors.onAddCallback = (ReorderableList list) =>
        {
            Undo.RecordObject(fcc, "Added new color palette");
            fcc.featuredColorsPalettes.Add(new FeaturedColorPalette(fcc.colorsPerPalette));            
        };
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        listColors.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}