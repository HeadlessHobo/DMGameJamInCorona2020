using System;
using Common.Menu.ScreenLoaders;
using Common.Util.Buttons;
using Common.Util.Pickers;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class _3DScreenLoaderData : ScreenLoaderData
    {
        [SerializeField] 
        private float _moveTime;
        
        [SerializeField] 
        private Vector3 _cameraPosition;
        
        [SerializeField] 
        private Vector3 _cameraRotation;
        
        [SerializeField] 
        private float _cameraFieldOfView;

        [SerializeField] 
        private ScenePicker _scenePicker;
        
        [SerializeField] 
        private _3DScreenLoaderButton _screenLoaderButton;

        public float MoveTime => _moveTime;

        public Vector3 CameraPosition => _cameraPosition;

        public Vector3 CameraRotation => _cameraRotation;

        public float CameraFieldOfView => _cameraFieldOfView;

        public string SceneToLoad => _scenePicker.PickedValue;
    }
}