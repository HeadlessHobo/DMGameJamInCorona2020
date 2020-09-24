using System.Collections.Generic;
using System.IO;
using Common._2DAnimation;
using Common._2DAnimation.Abstract;
using Common._2DAnimation.SpriteSheet;
using Common.Generating;
using Plugins.Enums;
using UnityEngine;

namespace Editor.Generating.ConstClassGenerators
{
    public class AnimationNamesConstClassGenerator : IConstClassGenerator
    {
        private const string ANIMATION_DATA_RESOURCE_FOLDER_PATH = @"Data\Animation";
        private const string GENERATED_ANIMATIONS_DATA_CLASS_NAME = "Animations";
        private const string RESOURCES_FOLDER_NAME = "Resources";
        
        public void Generate()
        {
            LoadAnimationCreatorsFromAllSubFolders();
        }

        private void LoadAnimationCreatorsFromAllSubFolders()
        {
            string pathToResourcesFolder = Path.Combine(Application.dataPath.Replace("/", @"\"), RESOURCES_FOLDER_NAME, ANIMATION_DATA_RESOURCE_FOLDER_PATH);

            foreach (var subFolderPath in Directory.GetDirectories(pathToResourcesFolder))
            {
                string subFolderName =  Path.GetFileName(subFolderPath);
                ConstClassGeneratorUtil.GenerateClass(subFolderName + GENERATED_ANIMATIONS_DATA_CLASS_NAME, 
                    GenerateFieldDataFromSpriteSheetAnimation(LoadAnimationCreatorsFromResourceSubFolderPath(subFolderName)));
            }
        }

        private List<AnimationCreator> LoadAnimationCreatorsFromResourceSubFolderPath(string subfolder)
        {
            List<AnimationCreator> animationCreators = new List<AnimationCreator>();
            
            foreach (var loadedAnimationCreator in Resources.LoadAll<AnimationCreator>(Path.Combine(ANIMATION_DATA_RESOURCE_FOLDER_PATH, subfolder)))
            {
                if (loadedAnimationCreator != null)
                {
                    animationCreators.Add(loadedAnimationCreator);
                }
            }

            return animationCreators;
        }

        private IEnumerable<ConstFieldData> GenerateFieldDataFromSpriteSheetAnimation(List<AnimationCreator> animationCreators)
        {
            foreach (var animationCreator in animationCreators)
            {
                yield return new ConstFieldData()
                {
                    Name = animationCreator.Name.ToUpper(),
                    DataType = BuiltInDataType.String,
                    Value = animationCreator.Name
                };
            }
        }
    }
}