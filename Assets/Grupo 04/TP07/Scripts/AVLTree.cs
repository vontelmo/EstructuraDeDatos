using System;

namespace MyBST
{
    public class AVLTree<T> : BST<T> where T : IComparable<T>
    {
        public override void Insert(T value)
        {
            root = InsertRecursive(value, root);
        }

        protected override Node<T> InsertRecursive(T value, Node<T> current)
        {
            //Node<T> node = base.InsertRecursive(value, root);

            //Chequeamos si el nodo NO existe
            if (current == null)

                //Si no existe, insertamos ahi
                return new Node<T>(value);

            //Si existe, chequeamos si es mayor
            int cmp = value.CompareTo(current.Value);

            //Si es mayor, llamamos a InsertRecursive con el hijo derecho
            if (cmp < 0)
                current.left = InsertRecursive(value, current.left);

            //Si es menor, llamamos a InsertRecursive con el hijo izquierdo
            else if (cmp >= 0)
                current.right = InsertRecursive(value, current.right);

            current = BalanceTree(current);
            

            return current;
        }

        private Node<T> BalanceTree(Node<T> node)
        {
            // casos Lx FB > 1
            if (GetBalanceFactor(node) > 1)
            {
                // right rotation
                if (GetBalanceFactor(node.left) >= 0)
                {
                    return RotateRight(node);
                }
                
                // LR -> Left, right
                if (GetBalanceFactor(node.left) <= 0)
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }
            }

            // casos Rx FB < -1
            if (GetBalanceFactor(node) < -1)
            {
                // left rotation
                if (GetBalanceFactor(node.right) <= 0)
                {
                    return RotateLeft(node);
                }

                // RL -> right, left
                if (GetBalanceFactor(node.right) >= 0)
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> pivot = node.left;
            Node<T> tree2 = pivot.right;

            pivot.right = node;
            node.left = tree2;

            return pivot;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> pivot = node.right;
            Node<T> tree2 = pivot.left;

            pivot.left = node;
            node.right = tree2;

            return pivot;
        }
    }
}
