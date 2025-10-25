using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyBST
{
    public class BST<T> where T : IComparable<T>
    {
        protected Node<T> root;

        public Node<T> Root { get { return root; } }

        public BST()
        {
            root = null;
        }

        public virtual void Insert(T value)
        {
            root = InsertRecursive(value, root);
        }

        protected virtual Node<T> InsertRecursive(T value, Node<T> current)
        {
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

            // Si es igual, no hace nada (no acepta duplicados)
            return current;
        }

        public int GetHeight()
        {
            return GetHeightRecursive(root);
        }

        protected virtual int GetHeightRecursive(Node<T> node)
        {
            if (node == null) return 0;
            return 1 + Mathf.Max(GetHeightRecursive(node.left), GetHeightRecursive(node.right));
        }

        public int GetBalanceFactor()
        {
            return GetBalanceFactor(root);
        }

        protected int GetBalanceFactor(Node<T> node)
        {
            return GetHeightRecursive(node.left) - GetHeightRecursive(node.right);
        }


        public void InOrder()
        {
            InOrderRecursive(root);

            //obtener valores en ascendente
        }

        public List<T> InOrderList()
        {
            List<T> list = new List<T>();
            InOrderListRecursive(root, list);
            return list;
            //obtener valores en ascendente
        }

        private void InOrderListRecursive(Node<T> node, List<T> list)
        {
            if (node == null) return;

            InOrderListRecursive(node.right, list); // derecha primero (mayores)
            list.Add(node.Value);
            InOrderListRecursive(node.left, list); // luego izquierda (menores)
        }

        private void InOrderRecursive(Node<T> Nodo)
        {
            if (Nodo == null)
            {
                return;
            }

            InOrderRecursive(Nodo.left);
            Debug.Log(Nodo.Value);
            InOrderRecursive(Nodo.right);
        }

        public void PreOrder()
        {
            PreOrderRecursive(root);
            //copiar o serializar arbol
        }

        private void PreOrderRecursive(Node<T> Nodo)
        {
            if (Nodo == null)
            {
                return;
            }

            Debug.Log(Nodo.Value);
            PreOrderRecursive(Nodo.left);
            PreOrderRecursive(Nodo.right);
        }

        public void PostOrder()
        {
            PostOrderRecursive(root);
            //eliminar nodos
        }

        private void PostOrderRecursive(Node<T> nodo)
        {
            if (nodo == null)
            {
                return;
            }

            PostOrderRecursive(nodo.left);
            PostOrderRecursive(nodo.right);
            Debug.Log(nodo.Value);
        }

        public void LevelOrder(Node<T> current)
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(current);

            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                Debug.Log(current.Value.ToString());

                if (current.left != null)
                {
                    queue.Enqueue(current.left);
                }

                if (current.right != null)
                {
                    queue.Enqueue(current.right);
                }
            }
        }
    }
}