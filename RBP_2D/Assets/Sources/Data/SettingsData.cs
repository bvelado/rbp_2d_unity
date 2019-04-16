using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public MaxRectsBinPack.FreeRectChoiceHeuristic FreeRectChoiceHeuristic;
    public List<Vector2Int> Items;
    public Vector2Int BinSize;
}
