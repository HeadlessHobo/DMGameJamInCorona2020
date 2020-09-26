using System;
using Common;
using Common.UnitSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class FollowPlayer : MonoBehaviour
    {
        private void Update()
        {
            Vector2 playerPosition =
                GameManager.Instance.Player.GetSetup<UnitMovementSetup>().MovementTransform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
        }
    }
}