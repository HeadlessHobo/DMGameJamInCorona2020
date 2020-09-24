using System.Collections.Generic;
using System.IO;
using Plugins;
using Plugins.Enums;
using UnityEditor;
using UnityEngine;

namespace Editor.Generating
{
    public static class ConstClassGeneratorUtil
    {
        private const string GENRATED_CLASS_FOLDER_NAME = "Generated";
        private const string GENRATED_CLASS_NAMESPACE = "Generated";
        private const string SPACE_REPLACE_CHARACTER = "";

        public static void ClearFilesInGeneratedFolder()
        {
            DirectoryInfo generatedFolderPathInfo = new DirectoryInfo(Path.Combine(Application.dataPath, GENRATED_CLASS_FOLDER_NAME));

            foreach (FileInfo generatedFile in generatedFolderPathInfo.GetFiles())
            {
                generatedFile.Delete(); 
            }
        }
        
        public static void GenerateClass(string name, IEnumerable<ConstFieldData> constFieldDataEntries)
        {
            ClassModel generatedClass = new ClassModel(name);
            generatedClass.Fields = GenerateStaticFields(constFieldDataEntries);

            FileModel generatedClassFile = GenerateClasssFile(name, generatedClass);
            SaveGeneratedClassFile(generatedClassFile);
        }

        private static List<Field> GenerateStaticFields(IEnumerable<ConstFieldData> constFieldDataEntries)
        {
            List<Field> staticFields = new List<Field>();

            foreach (var constFieldDataEntry in constFieldDataEntries)
            {
                string nameWithoutSpaces = ReplaceSpacesInNameWithSpecifiedCharacter(constFieldDataEntry.Name, SPACE_REPLACE_CHARACTER);
                staticFields.Add(new Field(constFieldDataEntry.DataType, nameWithoutSpaces)
                {
                    DefaultValue = "\"" + constFieldDataEntry.Value + "\"",
                    AccessModifier = AccessModifier.Public,
                    SingleKeyWord = KeyWord.Const
                });
            }

            return staticFields;
        }

        private static string ReplaceSpacesInNameWithSpecifiedCharacter(string name, string replaceText)
        {
            return name.Replace(" ", replaceText);
        }

        private static FileModel GenerateClasssFile(string className, ClassModel generatedClass)
        {
            FileModel generatedClassFile = new FileModel(className);
            generatedClassFile.Namespace = GENRATED_CLASS_NAMESPACE;
            generatedClassFile.Classes.Add(generatedClass);

            return generatedClassFile;
        }

        private static void SaveGeneratedClassFile(FileModel generatedClassFile)
        {
            MakeSureGeneratedPathExists();
            
            CsGenerator csGenerator = new CsGenerator();
            csGenerator.Path = Application.dataPath;
            csGenerator.OutputDirectory = GENRATED_CLASS_FOLDER_NAME;
            csGenerator.Files.Add(generatedClassFile);
            csGenerator.CreateFiles();
            AssetDatabase.ImportAsset(Path.Combine(GENRATED_CLASS_FOLDER_NAME, generatedClassFile.Name), ImportAssetOptions.Default);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private static void MakeSureGeneratedPathExists()
        {
            string savePath = Path.Combine(Application.dataPath, GENRATED_CLASS_FOLDER_NAME);
            Directory.CreateDirectory(savePath);
        }
    }
}