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

    public TileType[,] ConvertTilemapToMatrix()
    {
        // Determinar bounds del tilemap
        BoundsInt bounds = tilemap.cellBounds;

        int width = bounds.size.x;
        int height = bounds.size.y;

        TileType[,] matrix = new TileType[width, height];

        // Recorrer celda por celda
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            int x = pos.x - bounds.min.x;
            int y = pos.y - bounds.min.y;

            TileBase tile = tilemap.GetTile(pos);



            if (tile == null)
            {
                // Si quieres tratar 'sin tile' como Wall o Floor, elegí acá
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
            else if (tile == startTile && startCount < 1)
            {
                matrix[x, y] = TileType.Entrance;
                startCount++;
            }
            else if (tile == exitTile && exitCount < 1)
            {
                matrix[x, y] = TileType.Exit;
                exitCount++;
            }
            else
            {
                // default fallback
                matrix[x, y] = TileType.Wall;
            }
        }

        return matrix;
    }
}
