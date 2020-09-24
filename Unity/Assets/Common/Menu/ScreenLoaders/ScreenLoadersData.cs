using System;
using System.Collections.Generic;
using Common.Extensions;
using Common.Menu.ScreenLoaders.Scene;
using DefaultNamespace;
using UnityEngine;

namespace Common.Menu.ScreenLoaders
{
    [Serializable]
    public class ScreenLoadersData : ScriptableObject
    {
        [SerializeField]
        private List<SceneLoaderData> _sceneLoaders;
        
        [SerializeField]
        private List<_3DScreenLoaderData> _3dScreenLoaders;

        public List<SceneLoaderData> SceneLoaderDataEntries => _sceneLoaders;

        public List<_3DScreenLoaderData> _3DScreenLoaderDataEntries => _3dScreenLoaders;
    }
}