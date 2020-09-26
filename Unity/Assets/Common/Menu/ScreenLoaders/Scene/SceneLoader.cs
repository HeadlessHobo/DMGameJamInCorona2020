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
                SceneManager.LoadScene(_screenLoaderData.SceneToLoad);
            }
        }
    }
}