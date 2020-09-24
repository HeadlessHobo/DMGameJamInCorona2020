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

        [SerializeField] 
        private PlayerInput _playerInput;
        
        public Transform MovementTransform => _movementTransform;

        public Rigidbody2D Rigidbody2D => _rigidbody2D;

        public PlayerInput PlayerInput => _playerInput;
    }
}