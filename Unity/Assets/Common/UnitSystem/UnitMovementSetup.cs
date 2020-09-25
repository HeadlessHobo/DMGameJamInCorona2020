using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.UnitSystem
{
    [Serializable]
    public class UnitMovementSetup
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] 
        private Transform _movementTransform;

        public Transform MovementTransform => _movementTransform;

        public Rigidbody2D Rigidbody2D => _rigidbody2D;
    }
}