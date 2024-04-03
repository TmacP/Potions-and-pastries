using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EGameState
{
    MainState,
    NightState,
    PauseState,
    MovementDisabledState,
    PlayerCustomizationState,
    QuitState
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

    public int RoomsUnlocked = 0;

    public List<ItemData> UnlockedActionCards = new List<ItemData>();

}
