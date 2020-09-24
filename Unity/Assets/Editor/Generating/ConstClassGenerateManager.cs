using System;
using System.Collections.Generic;
using Common.Util;
using UnityEditor;
using Object = System.Object;

namespace Editor.Generating
{
    public class ConstClassGenerateManager
    {
        private static List<IConstClassGenerator> _constClassGenerators;
        
        [MenuItem("Tools/Generate all const classes")]
        private static void GenerateAllConstClasses()
        {
            ConstClassGeneratorUtil.ClearFilesInGeneratedFolder();
            CreateAllConstClassGeneratorsIfNotExists();
            foreach (var generateConstClass in _constClassGenerators)
            {
                generateConstClass.Generate();
            }
        }

        private static void CreateAllConstClassGeneratorsIfNotExists()
        {
            if (_constClassGenerators == null)
            {
                CreateAllConstClassGenerators();
            }
        }

        private static void CreateAllConstClassGenerators()
        {
            _constClassGenerators = new List<IConstClassGenerator>();
            foreach (var constClassGeneratorType in TypeUtil.GetTypesThatImplementType(typeof(IConstClassGenerator)))
            {
                Object createdConstClassGeneratorObj = Activator.CreateInstance(constClassGeneratorType);
                _constClassGenerators.Add(createdConstClassGeneratorObj as IConstClassGenerator);
            }
        }
    }
}