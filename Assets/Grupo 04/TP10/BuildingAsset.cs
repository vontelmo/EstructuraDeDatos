using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (fileName = "Buildable", menuName = "BuildingAssets/Create Asset")]
public class BuildingAsset : ScriptableObject
{
    [SerializeField] TileType tileType;
    [SerializeField] TileBase tileBase;

    public TileBase TileBase
        { get { return tileBase; } }

    public TileType TileType
        { get { return tileType; } }
}
