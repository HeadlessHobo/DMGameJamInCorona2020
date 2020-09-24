namespace Common.Menu.ScreenLoaders
{
    public interface IScreenLoader
    {
        string ScreenName { get; }
        
        string InTransitionEffectName { get; }
        string OutTransitionEffectName { get; }
        
        void LoadScreen();
    }
    
    public abstract class ScreenLoader<TScreenLoaderData> : IScreenLoader where TScreenLoaderData : ScreenLoaderData
    {
        protected TScreenLoaderData _screenLoaderData;

        public string ScreenName => _screenLoaderData.ScreenName;
        public string InTransitionEffectName => _screenLoaderData.InTransitionEffectName;
        public string OutTransitionEffectName => _screenLoaderData.OutTransitionEffectName;

        protected ScreenLoader(TScreenLoaderData loaderData)
        {
            _screenLoaderData = loaderData;
        }
        
        public abstract void LoadScreen();
    }
}