using TMPro;
using UnityEngine;

namespace MyBST
{
    public class TP06Execute : MonoBehaviour
    {
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private RectTransform treeContainer;
        [SerializeField] private float xSpacing = 200f;
        [SerializeField] private float ySpacing = 120f;

        private BST<int> tree;


        private void Start()
        {
            BST<int> tree = new BST<int>();
            int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };

            foreach (var v in myArray)
            {
                tree.Insert(v);
            }

            SetTree(tree);
            Debug.Log(tree);
        }

        public void SetTree(BST<int> bst)
        {
            tree = bst;
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

            line.transform.SetAsFirstSibling();

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
}
