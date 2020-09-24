using System;
using DefaultNamespace;
using Plugins.LeanTween.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Menu.ScreenLoaders._3D
{
    public class _3DScreenLoader : ScreenLoader<_3DScreenLoaderData>
    {
        private Func<Camera> _mainCameraFunc;
        
        public _3DScreenLoader(_3DScreenLoaderData loaderData, Func<Camera> mainCameraFunc) : base(loaderData)
        {
            _mainCameraFunc = mainCameraFunc;
        }

        public override void LoadScreen()
        {
            if (Application.isPlaying)
            {
                LoadInGame();
            }
            else
            {
                LoadInEditor();
            }
        }

        private void LoadInGame()
        {
            if (SceneManager.GetActiveScene().name != _screenLoaderData.SceneToLoad)
            {
                SceneManager.LoadScene(_screenLoaderData.SceneToLoad);
            }

            Camera mainCamera = _mainCameraFunc();
            
            var cameraTransform = mainCamera.transform;
            cameraTransform.LeanMove(_screenLoaderData.CameraPosition, _screenLoaderData.MoveTime);
            cameraTransform.LeanRotate(_screenLoaderData.CameraRotation, _screenLoaderData.MoveTime);
            mainCamera.fieldOfView = _screenLoaderData.CameraFieldOfView;
        }

        private void LoadInEditor()
        {
            Camera mainCamera = _mainCameraFunc();
            
            var cameraTransform = mainCamera.transform;
            cameraTransform.position = _screenLoaderData.CameraPosition;
            cameraTransform.eulerAngles = _screenLoaderData.CameraRotation;
            mainCamera.fieldOfView = _screenLoaderData.CameraFieldOfView;
        }
    }
}