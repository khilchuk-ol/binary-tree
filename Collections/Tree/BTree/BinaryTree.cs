using Collections.Tree.Events;
using Collections.Tree.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections.Tree.BTree
{
    public delegate void NewNodeAddedHandler<T>(Node<T> node, Node<T> parent);

    public class BinaryTree<T> : ITree<T> where T : IComparable<T>
    {
        private Node<T> _root;

        public int Count { get; private set; } = 0;

        public event EventHandler<TreeClearedEventArgs> TreeCleared;

        public event NewNodeAddedHandler<T> NodeAdded;

        public BinaryTree()
        {
        }

        public BinaryTree(T item)
        {
            _root = new Node<T>(item);
            Count++;
        }

        public bool IsReadOnly => false;

        public T Root => _root != null ? _root.Value : default; 

        public void Add(T item)
        {
            var node = FindOrGetParentInDepth(item, _root);
            var toAdd = new Node<T>(item);

            if (node == null)
            {
                _root = toAdd;

                OnNewNodeAdded(toAdd, _root);
                Count++;
                return;
            }

            var compRes = CompareValues(item, node.Value);

            switch(compRes)
            {
                case 0:
                    return;
                case -1:
                    node.Left = toAdd;
                    break;
                case 1:
                    node.Right = toAdd;
                    break;
            }

            OnNewNodeAdded(toAdd, node);
            Count++;
        }

        public IEnumerator<T> BreadthFirstEnumerator()
        {
            return BreadthFirstWalk(_root).GetEnumerator();
        }

        public IEnumerable<T> BreadthFirstWalk()
        {
            return BreadthFirstWalk(_root);
        }

        private IEnumerable<T> BreadthFirstWalk(Node<T> subtree)
        {
            var toProcess = new Node<T>[] { subtree };
            var isFinished = false;

            while (!isFinished)
            {
                var toProcessNext = new Node<T>[toProcess.Length * 2];
                var i = 0;

                foreach (var node in toProcess)
                {
                    if (node == null)
                    {
                        if (i == 0)
                        {
                            isFinished = true;
                            break;
                        }

                        continue;
                    }

                    if (node.Left != null) toProcessNext[i++] = node.Left;
                    if (node.Right != null) toProcessNext[i++] = node.Right;

                    yield return node.Value;
                }

                toProcess = toProcessNext;
            }
        }

        public void Clear()
        {
            OnTreeCleared(nameof(Clear));

            _root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var node = FindOrGetParentInDepth(item, _root);

            return node != null && item.Equals(node.Value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                var paramName = nameof(array);
                throw new ArgumentNullException(paramName, "array for copying cannot be null");
            }

            if (arrayIndex < 0)
            {
                var paramName = nameof(arrayIndex);
                throw new ArgumentOutOfRangeException(paramName, "start index cannot be negative");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("passed array does not have required free space");
            }

            foreach (var elem in this)
            {
                array[arrayIndex++] = elem;
            }
        }

        public IEnumerator<T> GetEnumerator(int order = 0)
        {
            if (order >= 0)
            {
                return WalkAscending(_root).GetEnumerator();
            }

            return WalkDescending(_root).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator(0);
        }

        public bool Remove(T item)
        {
            if (_root == null)
            {
                throw new InvalidTreeStateException("tree is empty", "for remove operation tree should not be empty");
            }

            if (item.Equals(_root.Value))
            {
                if (_root.Left == null && _root.Right == null)
                {
                    OnTreeCleared(nameof(Remove));

                    _root = null;
                    Count = 0;
                    return true;
                }

                var (minRight, minRightParent) = GetMinInSubtree(_root.Right);
                minRightParent.Left = null;
                _root.Value = minRight.Value;
                Count--;
                return true;
            }

            var (parent, node, isLeft) = GetParentAndChildFromRoot(item);

            if (parent == null || node == null)
            {
                return false;
            }

            if (node.Left == null && node.Right == null)
            {
                if (isLeft == true)
                {
                    parent.Left = null;
                } else
                {
                    parent.Right = null;
                }

                Count--;
                return true;
            }

            if (node.Left == null)
            {
                if (isLeft == true)
                {
                    parent.Left = node.Right;
                }
                else
                {
                    parent.Right = node.Right;
                }

                Count--;
                return true;
            }

            if (node.Right == null)
            {
                if (isLeft == true)
                {
                    parent.Left = node.Left;
                }
                else
                {
                    parent.Right = node.Left;
                }

                Count--;
                return true;
            }

            var (min, minParent) = GetMinInSubtree(node.Right);

            node.Value = min.Value;
            if (minParent != null)
            {
                minParent.Left = null;
            } else
            {
                node.Right = null;
            }
            Count--;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(0);
        }

        protected virtual void OnTreeCleared(string methodName)
        {
            var args = new TreeClearedEventArgs(Count, $"tree is cleared. invoked: {methodName}");
            TreeCleared?.Invoke(this, args);
        }

        protected virtual void OnNewNodeAdded(Node<T> node, Node<T> parent)
        {
            NodeAdded?.Invoke(node, parent);
        }

        private Node<T> FindOrGetParentInDepth(T item, Node<T> subtreeRoot)
        {
            if (subtreeRoot == null)
            {
                return null;
            }

            var compRes = CompareValues(item, subtreeRoot.Value);

            var res = compRes switch
            {
                0  => subtreeRoot,
                1  => FindOrGetParentInDepth(item, subtreeRoot.Right),
                -1 => FindOrGetParentInDepth(item, subtreeRoot.Left),
                _  => null
            };

            return res ?? subtreeRoot;
        }

        private Node<T> GetParentInDepth(T item, Node<T> subtree)
        {
            if (subtree == null)
            {
                return null;
            }

            if ((subtree.Left != null && item.Equals(subtree.Left.Value)) || 
                (subtree.Right != null && item.Equals(subtree.Right.Value)))
            {
                return subtree;
            }

            var compRes = CompareValues(item, subtree.Value);

            var res = compRes switch
            {
                1 => GetParentInDepth(item, subtree.Right),
                -1 => GetParentInDepth(item, subtree.Left),
                _ => null
            };

            return res;
        }

        private (Node<T> elem, Node<T> parent, bool isLeftChild) GetParentAndChildFromRoot(T item)
        {
            var parent = GetParentInDepth(item, _root);
            if (parent == null)
            {
                return (null, null, false);
            }

            Node<T> node = null;
            bool isLeft = false;
            if (parent.Left != null && item.Equals(parent.Left.Value))
            {
                node = parent.Left;
                isLeft = true;
            }

            if (parent.Right != null && item.Equals(parent.Right.Value))
            {
                node = parent.Right;
                isLeft = false;
            }

            return (parent, node, isLeft);
        }

        private (Node<T> elem, Node<T> parent) GetMinInSubtree(Node<T> subtree)
        {
            if (subtree == null)
            {
                return (null, null);
            }

            if (subtree.Left == null)
            {
                return (subtree, null);
            }

            while (subtree.Left.Left != null)
            {
                subtree = subtree.Left;
            }

            return (subtree.Left, subtree);
        }

        private IEnumerable<T> WalkAscending(Node<T> subtree)
        {
            if (subtree != null)
            {
                if (subtree.Left != null)
                {
                    foreach (var node in WalkAscending(subtree.Left))
                    {
                        yield return node;
                    }
                }

                yield return subtree.Value;

                if (subtree.Right != null)
                {
                    foreach (var node in WalkAscending(subtree.Right))
                    {
                        yield return node;
                    }
                }
            }
        }

        private IEnumerable<T> WalkDescending(Node<T> subtree)
        {
            if (subtree != null)
            {
                if (subtree.Right != null)
                {
                    foreach (var node in WalkDescending(subtree.Right))
                    {
                        yield return node;
                    }
                }

                yield return subtree.Value;

                if (subtree.Left != null)
                {
                    foreach (var node in WalkDescending(subtree.Left))
                    {
                        yield return node;
                    }
                }
            }
        }

        private static int CompareValues(T first, T second)
        {
            var compRes = first.CompareTo(second);
            return compRes < 0 ? -1 : compRes == 0 ? 0 : 1;
        }
    }
}
