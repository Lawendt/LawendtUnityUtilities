using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// Class to manipulate the gameview sizes.
/// 
/// Developed by Fábio Damian (@Biodam)
/// Original available @ https://gist.github.com/Biodam/b0616918ea5c50c2c9e4b16e5bb1034b
/// </summary>
/// 
[InitializeOnLoad]
public static class GameViewUtils
{
    public enum GameViewSizeType
    {
        AspectRatio,
        FixedResolution
    }

    private static object gameViewSizesInstance;
    private static MethodInfo getGroup;

    static GameViewUtils()
    {
        // gameViewSizesInstance  = ScriptableSingleton<GameViewSizes>.instance;
        var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
        var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
        var instanceProp = singleType.GetProperty("instance");
        getGroup = sizesType.GetMethod("GetGroup");
        gameViewSizesInstance = instanceProp.GetValue(null, null);

        LoadQuickSelectSettings();
    }

    #region Examples

    [MenuItem("Tools/GameViewUtils/Test/AddSize")]
    public static void AddTestSize()
    {
        AddCustomSize(GameViewSizeType.AspectRatio, GameViewSizeGroupType.Standalone, 123, 456, "Test size");
    }

    [MenuItem("Tools/GameViewUtils/Test/SizeTextQuery")]
    public static void SizeTextQueryTest()
    {
        Debug.Log(SizeExists(GameViewSizeGroupType.Standalone, "Test size"));
    }

    [MenuItem("Tools/GameViewUtils/Test/Query16:9Test")]
    public static void WidescreenQueryTest()
    {
        Debug.Log(SizeExists(GameViewSizeGroupType.Standalone, "16:9"));
    }

    [MenuItem("Tools/GameViewUtils/Test/Set16:9")]
    public static void SetWidescreenTest()
    {
        SetSize(FindSize(GameViewSizeGroupType.Standalone, "16:9"));
    }

    [MenuItem("Tools/GameViewUtils/Test/SetTestSize")]
    public static void SetTestSize()
    {
        int idx = FindSize(GameViewSizeGroupType.Standalone, 123, 456);
        if (idx != -1)
        {
            SetSize(idx);
        }
    }

    [MenuItem("Tools/GameViewUtils/Test/SizeDimensionsQuery")]
    public static void SizeDimensionsQueryTest()
    {
        Debug.Log(SizeExists(GameViewSizeGroupType.Standalone, 123, 456));
    }

    [MenuItem("Tools/GameViewUtils/Test/LogCurrentGroupType")]
    public static void LogCurrentGroupType()
    {
        Debug.Log(GetCurrentGroupType());
    }

    [MenuItem("Tools/GameViewUtils/Test/GetBuiltInCount")]
    public static void GetBuiltInCount()
    {
        Debug.Log("GetBuiltInCount: " + GetBuiltInCount(GetCurrentGroupType()));
    }

    #endregion

    #region Methods

    public static void SetSize(int index)
    {
        var gameViewWindowType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var selectedSizeIndexProp = gameViewWindowType.GetProperty("selectedSizeIndex",
                                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var gameViewWindow = EditorWindow.GetWindow(gameViewWindowType);
        selectedSizeIndexProp.SetValue(gameViewWindow, index, null);
    }

    public static void AddCustomSize(GameViewSizeType viewSizeType, GameViewSizeGroupType sizeGroupType, int width, int height, string text)
    {
        // GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupTyge);
        // group.AddCustomSize(new GameViewSize(viewSizeType, width, height, text);

        var group = GetGroup(sizeGroupType);
        var addCustomSize = getGroup.ReturnType.GetMethod("AddCustomSize"); // or group.GetType().
        var gvsType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSize");
        //var ctor = gvsType.GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(string) });
        var gvstType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizeType");
        var ctor = gvsType.GetConstructor(new Type[] { gvstType, typeof(int), typeof(int), typeof(string) });
        var newSize = ctor.Invoke(new object[] { (int)viewSizeType, width, height, text });
        addCustomSize.Invoke(group, new object[] { newSize });
    }

    /// <summary>
    /// Return the count of the builtin list of sizes, anything after this is a custom size
    /// 
    /// Added by Fábio Damian
    /// </summary>
    /// <param name="gameViewSizeGroupType">The current platform</param>
    /// <returns>Count of the builtin sizes</returns>
    public static int GetBuiltInCount(GameViewSizeGroupType sizeGroupType)
    {
        var group = GetGroup(sizeGroupType);
        var getBuiltinCount = getGroup.ReturnType.GetMethod("GetBuiltinCount");
        return (int)getBuiltinCount.Invoke(group, null);
    }
    /// <summary>
    /// Return the count of the custom list of sizes, to access it one need to start iterating from
    /// the builtin count
    /// 
    /// Added by Fábio Damian
    /// </summary>
    /// <param name="gameViewSizeGroupType">The current platform</param>
    /// <returns>Count of the custom sizes</returns>
    public static int GetCustomCount(GameViewSizeGroupType sizeGroupType)
    {
        var group = GetGroup(sizeGroupType);
        var getCustomCount = getGroup.ReturnType.GetMethod("GetCustomCount");
        return (int)getCustomCount.Invoke(group, null);
    }
    /// <summary>
    /// Return the count of the total list of sizes
    /// 
    /// Added by Fábio Damian
    /// </summary>
    /// <param name="gameViewSizeGroupType">The current platform</param>
    /// <returns>Count of the total sizes</returns>
    public static int GetTotalCount(GameViewSizeGroupType sizeGroupType)
    {
        var group = GetGroup(sizeGroupType);
        var getTotalCount = getGroup.ReturnType.GetMethod("GetTotalCount");
        return (int)getTotalCount.Invoke(group, null);
    }

    public static void UpdateZoomAreaAndParent()
    {
        var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var updateZoomAreaAndParentMethod = gvWndType.GetMethod("UpdateZoomAreaAndParent",
                                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var gvWnd = EditorWindow.GetWindow(gvWndType);
        updateZoomAreaAndParentMethod.Invoke(gvWnd, null);
    }

    public static bool SizeExists(GameViewSizeGroupType sizeGroupType, string text)
    {
        return FindSize(sizeGroupType, text) != -1;
    }

    public static int FindSize(GameViewSizeGroupType sizeGroupType, string text)
    {
        // GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupType);
        // string[] texts = group.GetDisplayTexts();
        // for loop...

        var group = GetGroup(sizeGroupType);
        var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
        var displayTexts = getDisplayTexts.Invoke(group, null) as string[];
        for (int i = 0; i < displayTexts.Length; i++)
        {
            string display = displayTexts[i];
            // the text we get is "Name (W:H)" if the size has a name, or just "W:H" e.g. 16:9
            // so if we're querying a custom size text we substring to only get the name
            // You could see the outputs by just logging
            //Debug.Log(display);
            int pren = display.IndexOf('(');
            if (pren != -1)
            {
                display = display.Substring(0, pren - 1); // -1 to remove the space that's before the prens. This is very implementation-depdenent
            }

            if (display == text)
            {
                return i;
            }
        }
        return -1;
    }

    public static bool SizeExists(GameViewSizeGroupType sizeGroupType, int width, int height)
    {
        return FindSize(sizeGroupType, width, height) != -1;
    }

    public static string[] GetArrayOfAllGameViewResolutions()
    {
        var group = GetGroup(GetGameViewSizeGroupTypeByCurrentBuild());
        var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
        var displayTexts = getDisplayTexts.Invoke(group, null) as string[];

        return displayTexts;
    }

    public static GameViewSizeGroupType GetGameViewSizeGroupTypeByCurrentBuild()
    {
        switch (EditorUserBuildSettings.activeBuildTarget)
        {
            case BuildTarget.Android:
                return GameViewSizeGroupType.Android;

            case BuildTarget.StandaloneOSX:
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneLinux:
            case BuildTarget.StandaloneWindows64:
                return GameViewSizeGroupType.Standalone;

            case BuildTarget.iOS:
                return GameViewSizeGroupType.iOS;

            //DONT KNOW
            case BuildTarget.XboxOne:
            case BuildTarget.tvOS:
            case BuildTarget.Switch:
            case BuildTarget.Lumin:
            case BuildTarget.WebGL:
            case BuildTarget.WSAPlayer:
            case BuildTarget.PS4:
                break;
        }

        return GameViewSizeGroupType.Standalone;
    }

    public static int FindSize(GameViewSizeGroupType sizeGroupType, int width, int height)
    {
        // goal:
        // GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupType);
        // int sizesCount = group.GetBuiltinCount() + group.GetCustomCount();
        // iterate through the sizes via group.GetGameViewSize(int index)

        var group = GetGroup(sizeGroupType);
        var groupType = group.GetType();
        var getBuiltinCount = groupType.GetMethod("GetBuiltinCount");
        var getCustomCount = groupType.GetMethod("GetCustomCount");
        int sizesCount = (int)getBuiltinCount.Invoke(group, null) + (int)getCustomCount.Invoke(group, null);
        var getGameViewSize = groupType.GetMethod("GetGameViewSize");
        var gvsType = getGameViewSize.ReturnType;
        var widthProp = gvsType.GetProperty("width");
        var heightProp = gvsType.GetProperty("height");
        var indexValue = new object[1];
        for (int i = 0; i < sizesCount; i++)
        {
            indexValue[0] = i;
            var size = getGameViewSize.Invoke(group, indexValue);
            int sizeWidth = (int)widthProp.GetValue(size, null);
            int sizeHeight = (int)heightProp.GetValue(size, null);
            if (sizeWidth == width && sizeHeight == height)
            {
                return i;
            }
        }
        return -1;
    }

    private static object GetGroup(GameViewSizeGroupType type)
    {
        return getGroup.Invoke(gameViewSizesInstance, new object[] { (int)type });
    }

    public static GameViewSizeGroupType GetCurrentGroupType()
    {
        var getCurrentGroupTypeProp = gameViewSizesInstance.GetType().GetProperty("currentGroupType");
        return (GameViewSizeGroupType)(int)getCurrentGroupTypeProp.GetValue(gameViewSizesInstance, null);
    }

    #endregion

    #region Preferences

    [MenuItem("Tools/GameViewUtils/QuickSelectCustomSizeA &1")]
    public static void QuickSelectCustomSizeA()
    {
        SetSizeAndRepaint(quickSelect[0]);
    }

    [MenuItem("Tools/GameViewUtils/QuickSelectCustomSizeB &2")]
    public static void QuickSelectCustomSizeB()
    {
        SetSizeAndRepaint(quickSelect[1]);
    }

    [MenuItem("Tools/GameViewUtils/QuickSelectCustomSizeC &3")]
    public static void QuickSelectCustomSizeC()
    {
        SetSizeAndRepaint(quickSelect[2]);
    }

    [MenuItem("Tools/GameViewUtils/QuickSelectCustomSizeD &4")]
    public static void QuickSelectCustomSizeD()
    {
        SetSizeAndRepaint(quickSelect[3]);
    }

    private static void SetSizeAndRepaint(int index)
    {
        var currType = GetCurrentGroupType();
        if (index < GetTotalCount(currType))
        {
            SetSize(index);
            UpdateZoomAreaAndParent();
            //The UpdateZoomAreaAndParent already repaints the game view
            //InternalEditorUtility.RepaintAllViews();
        }
        else
        {
            Debug.LogError("Invalid quick select index.");
        }
    }

    private static List<GameViewSizeGroupType> groupTypes;
    [Serializable]
    private class CustomGameViewSize
    {
        public string name = "1280x720";
        public GameViewSizeType type = GameViewSizeType.FixedResolution;
        public int width = 1280;
        public int height = 720;

        public CustomGameViewSize(string name, int width, int height, GameViewSizeType type = GameViewSizeType.FixedResolution)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.type = type;
        }

    }
    private class CustomGameViewCollection : ScriptableObject
    {
        public List<CustomGameViewSize> customGameViewSizes;

        public CustomGameViewCollection()
        {
            customGameViewSizes = new List<CustomGameViewSize>
            {
                //Landscape
                new CustomGameViewSize("HD Landscape", 1280, 720),
                new CustomGameViewSize("Full HD Landscape", 1920, 1080),
                new CustomGameViewSize("Galaxy S9 Landscape", 2960, 1440),
                new CustomGameViewSize("iPhone Xs Max Landscape", 2688, 1242),
                new CustomGameViewSize("iPad Pro 10.5 Landscape", 2224, 1668),

                //Portrait
                new CustomGameViewSize("HD Portrait", 720, 1280),
                new CustomGameViewSize("Full HD Portrait", 1080, 1920),
                new CustomGameViewSize("Galaxy S9 Portrait", 1440, 2960),
                new CustomGameViewSize("iPhone Xs Max Portrait", 1242, 2688),
                new CustomGameViewSize("iPad Pro 10.5 Portrait", 1668, 2224)
            };
        }
    }
    private static CustomGameViewCollection customGameViewCollection;
    private static SerializedObject customGameViewCollectionSO;
    private static SerializedProperty customGameViewSizesSP;
    private static ReorderableList customGamViewSizesRL;
    private static ReorderableList groupTypesRL;

    //TODO: Save on EditorPrefs
    private static int[] quickSelect = { 0, 1, 2, 3 };

    [PreferenceItem("Game view utils")]
    private static void GameViewUtils_Prefs()
    {
        EditorGUI.BeginChangeCheck();
        DrawQuickSelectSettings();
        if (EditorGUI.EndChangeCheck())
        {
            SaveQuickSelectSettings();
        }
        EditorGUILayout.Space();
        DrawBulkAdd();
    }

    private static void SaveQuickSelectSettings()
    {
        for (int i = 0; i < quickSelect.Length; i++)
        {
            EditorPrefs.SetInt(QuickSelectKey(i.ToString()), quickSelect[i]);
        }
    }

    private static void LoadQuickSelectSettings()
    {
        for (int i = 0; i < quickSelect.Length; i++)
        {
            if (EditorPrefs.HasKey(QuickSelectKey(i.ToString())))
            {
                quickSelect[i] = EditorPrefs.GetInt(QuickSelectKey(i.ToString()));
            }
        }
    }

    private static string QuickSelectKey(string key)
    {
        return "GameViewQuickSelectSize" + PlayerSettings.productName + "_" + key;
    }

    private static void DrawBulkAdd()
    {
        EditorGUILayout.LabelField("Bulk add sizes:", EditorStyles.boldLabel);

        if (groupTypes == null)
        {
            groupTypes = new List<GameViewSizeGroupType>(2)
            {
                GameViewSizeGroupType.Android,
                GameViewSizeGroupType.iOS,
                GameViewSizeGroupType.Standalone
            };
            SetupGroupTypesRL();
        }
        if (groupTypes != null)
        {
            if (groupTypesRL != null)
            {
                groupTypesRL.DoLayoutList();
            }
        }

        if (customGameViewCollection != null && customGameViewCollectionSO != null && customGamViewSizesRL != null)
        {
            customGameViewCollectionSO.Update();
            customGamViewSizesRL.DoLayoutList();
            customGameViewCollectionSO.ApplyModifiedProperties();
        }
        else
        {
            customGameViewCollection = new CustomGameViewCollection();
            customGameViewCollectionSO = new SerializedObject(customGameViewCollection);
            customGameViewSizesSP = customGameViewCollectionSO.FindProperty("customGameViewSizes");
            SetupCustomGamViewSizesRL();
        }

        EditorGUILayout.HelpBox("This button will create all the custom sizes on all the group types.", MessageType.Info, true);

        if (GUILayout.Button("Create custom resolutions"))
        {
            for (int i = 0; i < groupTypes.Count; i++)
            {
                for (int j = 0; j < customGameViewCollection.customGameViewSizes.Count; j++)
                {
                    var custom = customGameViewCollection.customGameViewSizes[j];
                    if (SizeExists(groupTypes[i], custom.width, custom.height) == false)
                    {
                        AddCustomSize(custom.type, groupTypes[i], custom.width, custom.height, custom.name);
                    }
                    else
                    {
                        Debug.Log("Size : " + custom.name + " already exists, skipping.");
                    }
                }
            }

            Debug.Log("Added custom sizes. Because of Unity internal serializations it may be needed for you to manually add one custom size (you can delete it right away) to force Unity to save the custom sizes. If you don't do this, when opening Unity again all the sizes may be gone.");

            EditorUtility.SetDirty((ScriptableObject)gameViewSizesInstance);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private static void DrawQuickSelectSettings()
    {
        EditorGUILayout.LabelField("Quick select indexes:", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Use quick select with alt+(1/2/3/4) it will select the game size of the index configured here.", MessageType.Info, true);
        int maxTotal = GetTotalCount(GetCurrentGroupType());

        string[] options = GetArrayOfAllGameViewResolutions();

        for (int i = 0; i < quickSelect.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Alt + " + (i + 1));
            quickSelect[i] = EditorGUILayout.Popup(quickSelect[i], options);
            EditorGUILayout.EndHorizontal();
        }
    }

    private static void SetupGroupTypesRL()
    {
        groupTypesRL = new ReorderableList(groupTypes, typeof(GameViewSizeGroupType))
        {
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Group types to add the custom sizes:");
            },

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2;
                groupTypes[index] = (GameViewSizeGroupType)EditorGUI.EnumPopup(rect, groupTypes[index]);
            }
        };
    }

    private static void SetupCustomGamViewSizesRL()
    {
        customGamViewSizesRL = new ReorderableList(customGameViewCollectionSO, customGameViewSizesSP)
        {
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Custom sizes to add:");
            },

            elementHeight = (EditorGUIUtility.singleLineHeight * 3) + (EditorGUIUtility.singleLineHeight / 2),

            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                float singleLineHeight = EditorGUIUtility.singleLineHeight;
                //EditorGUI.PropertyField(rect, customGameViewSizesSP.GetArrayElementAtIndex(index), true);
                var current = customGameViewSizesSP.GetArrayElementAtIndex(index);
                var rectSingleLineHeight = new Rect(rect)
                {
                    height = singleLineHeight
                };

                EditorGUI.PropertyField(rectSingleLineHeight, current.FindPropertyRelative("name"));
                rectSingleLineHeight.y += singleLineHeight;
                EditorGUI.PropertyField(rectSingleLineHeight, current.FindPropertyRelative("type"));
                rectSingleLineHeight.y += singleLineHeight;

                float fractionWidth = rect.width / 4;
                rectSingleLineHeight.width = fractionWidth;
                EditorGUI.LabelField(rectSingleLineHeight, "Width:");
                rectSingleLineHeight.x += fractionWidth;
                EditorGUI.PropertyField(rectSingleLineHeight, current.FindPropertyRelative("width"), GUIContent.none);
                rectSingleLineHeight.x += fractionWidth;
                EditorGUI.LabelField(rectSingleLineHeight, "Height:");
                rectSingleLineHeight.x += fractionWidth;
                EditorGUI.PropertyField(rectSingleLineHeight, current.FindPropertyRelative("height"), GUIContent.none);

                rectSingleLineHeight.x = rect.x;
                rectSingleLineHeight.y += singleLineHeight + 3;
                rectSingleLineHeight.height = 1;
                rectSingleLineHeight.width = rect.width;
                GUI.Box(rectSingleLineHeight, "");
            }
        };
    }

    private static string Number2String(int number, bool isCaps)
    {
        char c = (char)((isCaps ? 65 : 97) + (number - 1));

        return c.ToString();
    }
    #endregion
}