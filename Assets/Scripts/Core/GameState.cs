using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EGameState
{
    MainState,
    PauseState,
    MovementDisabledState
}

public enum EGameRegion
{
    None,
    Starting,
    Main,
    Forest,
    Plains,
    Polar,
    Desert
}

[Serializable, CreateAssetMenu(menuName = "CozyData/GameStateData")]

public class GameStateData : ScriptableObject
{
    public List<EGameRegion> UnlockedRegions = new List<EGameRegion>();

    public List<int> OpenedDoors = new List<int>();

}
