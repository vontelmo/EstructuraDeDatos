using UnityEngine;

public class TilePaintManager : MonoBehaviour
{
    public static TileType SelectedType = TileType.Floor;

    public void SelectWall() => SelectedType = TileType.Wall;
    public void SelectFloor() => SelectedType = TileType.Floor;
    public void SelectEntrance() => SelectedType = TileType.Entrance;
    public void SelectExit() => SelectedType = TileType.Exit;
}
