using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.Util;
using Plugins.LeanTween.Framework;
using UnityEngine;

public class Explosion : MonoBehaviour, ISpawnedObject<TNT.Data>
{
    private TNT.Data _tntData;
    
    public void OnSpawned(TNT.Data data)
    {
        _tntData = data;
        var leanScale = gameObject.LeanScale(transform.localScale * data.ExplosionRadius.Value,
            data.ExplosionExpandTime.Value);
        TriggerNotifier triggerNotifier = gameObject.AddComponent<TriggerNotifier>();
        triggerNotifier.Init(new List<UnitType>(){ UnitType.All });
        triggerNotifier.UnitEntered += OnUnitEntered;
        Destroy(gameObject, _tntData.ExplosionLiveTime.Value);
    }
    
    private void OnUnitEntered(UnitType unitType, IUnit unit)
    {
        OnUnitHitByExplosion(unitType, unit);
    }

    private void OnUnitHitByExplosion(UnitType unitType, IUnit unit)
    {
        if (unitType == UnitType.Player)
        {
            unit.GetSetup<UnitMovementSetup>().Rigidbody2D.
                AddExplosionForce(_tntData.ExplosionForce.Value, transform.position, _tntData.ExplosionRadius.Value);
        }
        else if (unitType == UnitType.Enemy)
        {
            unit.GetArmor<IArmor>().Die();
        }
    }
}