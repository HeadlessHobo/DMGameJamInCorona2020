using System;
using System.IO;
using Common.Extensions;
using Common.Util.Pickers;
using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEditor;
using UnityEngine;

namespace Common.Menu.ScreenLoaders.Scene
{
    [Serializable]
    public class SceneLoaderData : ScreenLoaderData
    {
        [SerializeField]
        private ScenePicker _scene;

        public string SceneToLoad => _scene.PickedValue;
    }
}