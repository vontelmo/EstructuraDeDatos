using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public TileType tileType;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void OnMouseDown()
    {
        tileType = TilePaintManager.SelectedType;
        UpdateColor();
    }

    private void UpdateColor()
    {
        switch (tileType)
        {
            case TileType.Wall:
                sr.color = Color.black;
                break;
            case TileType.Floor:
                sr.color = Color.white;
                break;
            case TileType.Entrance:
                sr.color = Color.blue;
                break;
            case TileType.Exit:
                sr.color = Color.red;
                break;
        }
    }
}
