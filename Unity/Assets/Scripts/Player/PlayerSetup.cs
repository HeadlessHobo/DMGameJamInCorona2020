using System;
using UnityEngine;

[Serializable]
public class PlayerSetup
{
    [SerializeField]
    private GameObject _attractedFollowTriggerGo;

    [SerializeField] 
    private GameObject _attractedCheerTriggerGo;

    public GameObject AttractedFollowTriggerGo => _attractedFollowTriggerGo;

    public GameObject AttractedCheerTriggerGo => _attractedCheerTriggerGo;
}