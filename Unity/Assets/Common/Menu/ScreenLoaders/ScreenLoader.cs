using System;

namespace Common.Menu.ScreenLoaders
{
    public interface IScreenLoader
    {
        string ScreenName { get; }
        
        string InTransitionEffectName { get; }
        string OutTransitionEffectName { get; }

        event Action<IScreenLoader> ScreenLoaded;
        
        void LoadScreen();
    }
    
    public abstract class ScreenLoader<TScreenLoaderData> : IScreenLoader where TScreenLoaderData : ScreenLoaderData
    {
        protected TScreenLoaderData _screenLoaderData;

        public string ScreenName => _screenLoaderData.ScreenName;
        public string InTransitionEffectName => _screenLoaderData.InTransitionEffectName;
        public string OutTransitionEffectName => _screenLoaderData.OutTransitionEffectName;
        
        public event Action<IScreenLoader> ScreenLoaded;

        protected ScreenLoader(TScreenLoaderData loaderData)
        {
            _screenLoaderData = loaderData;
        }
        
        public abstract void LoadScreen();

        protected void OnScreenLoaded()
        {
            ScreenLoaded?.Invoke(this);
        }
    }
}