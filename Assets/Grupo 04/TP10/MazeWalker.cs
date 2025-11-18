using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWalker : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private List<Vector3> worldPath;
    private bool isWalking = false;

    public void StartWalking(List<MyGraphNode> path)
    {
        // Convertir nodos a posiciones del mundo
        worldPath = new List<Vector3>();
        foreach (var node in path)
        {
            worldPath.Add(new Vector3(node.X, node.Y, 0));
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
