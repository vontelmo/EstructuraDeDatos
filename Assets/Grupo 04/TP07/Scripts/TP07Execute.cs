using MyBST;
using TMPro;
using UnityEngine;

public class TP07Execute : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private RectTransform treeContainer;
    [SerializeField] private float xSpacing = 200f;
    [SerializeField] private float ySpacing = 120f;

    private AVLTree<int> tree;

    private void Start()
    {
        AVLTree<int> tree = new AVLTree<int>();

        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12 };

        foreach (var v in myArray)
        {
            tree.Insert(v);
        }

        SetTree(tree);
        Debug.Log(tree);

        Debug.Log(tree.Root == null);
        Debug.Log("--------LEVEL ORDER--------");
        tree.LevelOrder(tree.Root);
        Debug.Log("--------POST ORDER--------");
        tree.PostOrder();
    }

    public void SetTree(AVLTree<int> avl)
    {
        tree = avl;
        DrawTree();
    }

    public void DrawTree()
    {
        foreach (Transform child in treeContainer)
            Destroy(child.gameObject);

        if (tree.Root != null)
            DrawNode(tree.Root, 0, 0, treeContainer.rect.width / 2f);
        if (tree.Root == null)
            Debug.Log("nulllllll");
    }

    private GameObject DrawNode(Node<int> node, int depth, float xOffset, float parentX)
    {
        if (node == null) return null;

        GameObject newNode = Instantiate(nodePrefab, treeContainer);
        TMP_Text text = newNode.GetComponentInChildren<TMP_Text>();
        text.text = node.Value.ToString();

        // Posición
        float xPos = parentX + xOffset;
        float yPos = -depth * ySpacing;
        RectTransform nodeRect = newNode.GetComponent<RectTransform>();
        nodeRect.anchoredPosition = new Vector2(xPos, yPos);

        float childOffset = Mathf.Max(60, xSpacing / (depth + 1));

        if (node.left != null)
        {
            GameObject leftChild = DrawNode(node.left, depth + 1, -childOffset, xPos);
            DrawLine(nodeRect, leftChild.GetComponent<RectTransform>());
        }

        if (node.right != null)
        {
            GameObject rightChild = DrawNode(node.right, depth + 1, childOffset, xPos);
            DrawLine(nodeRect, rightChild.GetComponent<RectTransform>());
        }

        return newNode;
    }

    private void DrawLine(RectTransform from, RectTransform to)
    {
        if (linePrefab == null) return;

        GameObject line = Instantiate(linePrefab, treeContainer);
        RectTransform rect = line.GetComponent<RectTransform>();

        Vector2 start = from.anchoredPosition;
        Vector2 end = to.anchoredPosition;
        Vector2 direction = (end - start).normalized;

        float distance = Vector2.Distance(start, end);

        rect.sizeDelta = new Vector2(4, distance);
        rect.anchoredPosition = start + (end - start) / 2f;
        float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;
        rect.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

}
