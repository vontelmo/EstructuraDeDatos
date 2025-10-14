using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBST
{
    public class AVLTree<T> : BST<T> where T : IComparable<T>
    {
        public override bool Insert(T value)
        {
            bool inserted = base.Insert(value);

            if (inserted)
            {
                root = BalanceTree(root);
            }

            return inserted;
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
