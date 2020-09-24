using System.Collections;
using System.Collections.Generic;
using Common.UnitSystem;
using Generated;
using UnityEngine;

namespace Common.Util
{
    public delegate void UnitEntered(UnitType unitType, IUnit unit);
    public delegate void UnitStayed(UnitType unitType, IUnit unit);
    public delegate void UnitExited(UnitType unitType, IUnit unit);

    public class TriggerNotifier : MonoBehaviour
    {
        private IUnit _owner;
        
        [SerializeField]
        private List<UnitType> _unitsToNotifyOn;

        public event UnitEntered UnitEntered;
        public event UnitStayed UnitStayed;
        public event UnitExited UnitExited;

        public void Init(List<UnitType> unitsToNotifyOn, IUnit owner = null)
        {
            _unitsToNotifyOn = unitsToNotifyOn;
            _owner = owner;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IUnit otherUnit = GetUnitFromCollider(other);
            if (otherUnit != null && otherUnit != _owner && ShouldNotify(otherUnit.UnitType))
            {
                UnitEntered?.Invoke(otherUnit.UnitType, otherUnit);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            IUnit otherUnit = GetUnitFromCollider(other);
            if (otherUnit != null && otherUnit != _owner && ShouldNotify(otherUnit.UnitType))
            {
                UnitStayed?.Invoke(otherUnit.UnitType, otherUnit);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IUnit otherUnit = GetUnitFromCollider(other);
            if (otherUnit != null && otherUnit != _owner && ShouldNotify(otherUnit.UnitType))
            {
                UnitExited?.Invoke(otherUnit.UnitType, otherUnit);
            }
        }

        private bool ShouldNotify(UnitType otherUnitsType)
        {
            return _unitsToNotifyOn.Contains(otherUnitsType) || _unitsToNotifyOn.Contains(UnitType.All);
        }

        private IUnit GetUnitFromCollider(Collider2D other)
        {
            IUnit otherUnit = other.GetComponent<IUnit>();
            if (otherUnit != null)
            {
                return otherUnit;
            }

            return other.GetComponentInParent<IUnit>();
        }
    }
}