using System;
using UnityEngine;

namespace LUT.Snippets
{
    [Serializable]
    public class SceneReference
    {
#pragma warning disable 0649
#if UNITY_EDITOR
        public UnityEditor.SceneAsset scene;
#endif

        [InspectorReadOnly]
        [SerializeField]
        private string sceneName;
#pragma warning restore 0649

        public string SceneToLoadName
        {
            get
            {
                return sceneName;
            }
            set
            {
                sceneName = value;
            }
        }
    }
}