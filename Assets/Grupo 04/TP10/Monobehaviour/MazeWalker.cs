using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWalker : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private List<Vector3> worldPath;
    private bool isWalking = false;

    private int counter;

    BuildingCreator buildingCreator;


    private void Awake()
    {
        buildingCreator = BuildingCreator.GetInstance();
    }


    public void StartWalking(List<MyGraphNode> path)
    {
        // Convertir nodos a posiciones del mundo

        worldPath = new List<Vector3>();
        foreach (var node in path)
        {
            Vector3 cellPosition = buildingCreator.DefaultMap.GetCellCenterWorld(buildingCreator.DefaultMap.origin + new Vector3Int(node.X, node.Y, 0));
            worldPath.Add(cellPosition);
            Debug.Log(cellPosition);
        }

        if (!isWalking)
            StartCoroutine(WalkPath());
    }

    IEnumerator WalkPath()
    {
        isWalking = true;

        foreach (var targetPos in worldPath)
        {
            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    moveSpeed * Time.deltaTime);

                yield return null;
            }
        }
        isWalking = false;
    }
}
