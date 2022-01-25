namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            if (this.IsRootDeleted)
            {
                return result;
            }
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (Tree<T> child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        public ICollection<T> OrderDfs()
        {
            if (this.IsRootDeleted)
            {
                return new List<T>(); ;
            }
            //stack
            //return this.StackDfs();
            //recursion
            var result = new List<T>();
            this.Dfs(this, result);
            return result;
        }

        // recursion 
        private void Dfs(Tree<T> subTree, List<T> result)
        {
            foreach (var subTreeChild in subTree.Children)
            {
                this.Dfs(subTreeChild, result);
            }
            result.Add(subTree.Value);
        }

        private ICollection<T> StackDfs()
        {
            var result = new Stack<T>();
            var stack = new Stack<Tree<T>>();

            stack.Push(this);
            while (stack.Count > 0)
            {
                var sub = stack.Pop();

                foreach (var item in sub.Children)
                {
                    stack.Push(item);
                }
                result.Push(sub.Value);
            }

            return new List<T>(result);
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var node = FindNodeBfs(this, parentKey);
            CheckEmptyNode(node);
            node._children.Add(child);
        }

        private Tree<T> FindNodeBfs(Tree<T> root, T serchedValue)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (serchedValue.Equals(node.Value))
                {
                    return node;
                }

                foreach (var item in node.Children)
                {
                    queue.Enqueue(item);
                }
            }
            return null;
        }

        public void RemoveNode(T nodeKey)
        {
            Tree<T> node = FindNodeBfs(this, nodeKey);
            CheckEmptyNode(node);

            foreach (var child in node.Children)
            {
                child.Parent = null;
            }

            node._children.Clear();
            var nodeParent = node.Parent;

            if (nodeParent == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                nodeParent._children.Remove(node);
                node.Parent = null;
            }

            node.Value = default(T);
        }



        public void Swap(T firstKey, T secondKey)
        {
            var firstNodeToSwap = FindNodeBfs(this, firstKey);
            var secondNodeToSwap = FindNodeBfs(this, secondKey);
            CheckEmptyNode(firstNodeToSwap);
            CheckEmptyNode(secondNodeToSwap);
            var firstParent = firstNodeToSwap.Parent;
            var secondParent = secondNodeToSwap.Parent;
            if (firstParent == null)
            {
                SwapRootIfNeeded(secondNodeToSwap);
                return;
            }
            if (secondParent == null)
            {
                SwapRootIfNeeded(firstNodeToSwap);
                return;
            }

            int firstNodeIndex = firstParent._children.IndexOf(firstNodeToSwap);
            int secondNodeIndex = secondParent._children.IndexOf(secondNodeToSwap);

            firstParent._children[firstNodeIndex] = secondNodeToSwap;
            secondParent._children[secondNodeIndex] = firstNodeToSwap;
        }

        private void SwapRootIfNeeded(Tree<T> node)
        {
            this.Value = node.Value;
            this._children.Clear();
            foreach (var child in node.Children)
            {
                this._children.Add(child);
            }
        }

        private void CheckEmptyNode(Tree<T> parentSubtree)
        {
            if (parentSubtree == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
