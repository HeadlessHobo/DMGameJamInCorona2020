using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Menu.ScreenLoaders.Scene
{
    public class SceneLoader : ScreenLoader<SceneLoaderData>
    {
        public SceneLoader(SceneLoaderData loaderData) : base(loaderData)
        {
        }

        public override void LoadScreen()
        {
            if (SceneManager.GetActiveScene().name != _screenLoaderData.SceneToLoad)
            {
                SceneManager.sceneLoaded += OnsceneLoaded;
                SceneManager.LoadScene(_screenLoaderData.SceneToLoad);
            }
        }

        private void OnsceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= OnsceneLoaded;
            OnScreenLoaded();
        }
    }
}