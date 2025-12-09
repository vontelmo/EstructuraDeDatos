using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapToMatrix : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase startTile;
    public TileBase exitTile;

    private int startCount = 0;
    private int exitCount = 0;

    private string message;
    private bool isPathValid;

    public bool IsPathValid => isPathValid;
    public string Message => message;



    TileType[,] matrix;

    public TileType[,] ConvertTilemapToMatrix()
    {
        message = "Path is Invalid";
        isPathValid = true;
        BoundsInt bounds = tilemap.cellBounds;

        int width = bounds.size.x;
        int height = bounds.size.y;

        matrix = new TileType[width, height];

        startCount = 0;
        exitCount = 0;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            int x = pos.x - bounds.min.x;
            int y = pos.y - bounds.min.y;

            TileBase tile = tilemap.GetTile(pos);

            if (tile == null)
            {
                matrix[x, y] = TileType.Wall;
            }
            else if (tile == floorTile)
            {
                matrix[x, y] = TileType.Floor;
            }
            else if (tile == wallTile)
            {
                matrix[x, y] = TileType.Wall;
            }
            else if (tile == startTile)
            {
                matrix[x, y] = TileType.Entrance;
                startCount++;
            }
            else if (tile == exitTile)
            {
                matrix[x, y] = TileType.Exit;
                exitCount++;
            }
            else
            {
                matrix[x, y] = TileType.Wall;
            }


        }

        if (startCount > 1 || exitCount > 1)
        {
            message = "Path is Invalid : more than one entrance or exit";
            Debug.LogWarning("Cant place more than one entrance or exit");
            isPathValid = false;

        }

        return matrix;
    }
}



