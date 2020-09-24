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
            SceneManager.LoadScene(_screenLoaderData.SceneToLoad);
        }
    }
}