using System;
using UnityEngine;

namespace Common.Generating
{
    [Serializable]
    public class ConstClassGenerateSettings
    {
        [SerializeField]
        private string _generatedConstClassName;
        
        [SerializeField]
        private bool _generateConstClass;

        public string GeneratedConstClassName
        {
            get { return _generatedConstClassName; }
            set { _generatedConstClassName = value; }
        }

        public bool GenerateConstClass
        {
            get { return _generateConstClass; }
            set { _generateConstClass = value; }
        }
    }
}