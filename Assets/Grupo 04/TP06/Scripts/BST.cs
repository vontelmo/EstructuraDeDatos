using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyBST
{
    public class BST<T> where T : IComparable<T>
    {
        protected Node<T> root;

        public Node<T> Root => root;

        public BST()
        {
            root = null;
        }

        public virtual bool Insert(T value) => Insert(root, value);

        protected bool Insert(Node<T> current, T value)
        {
            if (current == null)
            {
                current = new Node<T>(value);
            }
            else if (value.CompareTo(current.Value) < 0)
            {
                Insert(current.left, value);
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                Insert(current.right, value);
            }
            else
            {
                return false;
            }

            return true;
        }

        public int GetHeight()
        {
            return GetHeightRecursive(root);
        }

        private int GetHeightRecursive(Node<T> current)
        {
            if (current == null)
            {
                return 0;
            }

            return Mathf.Max(GetHeightRecursive(current.left), GetHeightRecursive(current.right));
        }

        protected virtual int GetBalanceFactor(Node<T> Node)
        {
            return GetBalanceFactor(root);
        }

        public void InOrder()
        {
            InOrderRecursive(root);
            //obtener valores en ascendente
        }

        private void InOrderRecursive(Node<T> Node)
        {
            if (Node == null)
            {
                return;
            }

            InOrderRecursive(Node.left);
            Debug.Log(Node.Value);
            InOrderRecursive(Node.right);
        }

        public void PreOrder()
        {
            PreOrderRecursive(root);
            //copiar o serializar arbol
        }

        private void PreOrderRecursive(Node<T> Node)
        {
            if (Node == null)
            {
                return;
            }

            Debug.Log(Node.Value);
            PreOrderRecursive(Node.left);
            PreOrderRecursive(Node.right);
        }

        public void PostOrder()
        {
            PostOrderRecursive(root);
            //eliminar Nodes
        }

        private void PostOrderRecursive(Node<T> Node)
        {
            if (Node == null)
            {
                return;
            }

            PostOrderRecursive(Node.left);
            PostOrderRecursive(Node.right);
            Debug.Log(Node.Value);
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