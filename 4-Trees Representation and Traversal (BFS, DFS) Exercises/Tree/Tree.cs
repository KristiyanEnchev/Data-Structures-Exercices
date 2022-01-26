namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.AddParent(this);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            return this.GetAsString(0).Trim();
        }

        private string GetAsString(int indentation = 0)
        {
            //var result = new string(' ', indentation) + this.Key + Environment.NewLine;
            var result = new string(' ', indentation) + this.Key + "\r\n";

            foreach (var child in this.Children)
            {
                result += child.GetAsString(indentation + 2);
            }

            return result;
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafNodes = GetLeafs();
            Tree<T> deepestLeaf = null;
            int startDepth = 0;
            foreach (var leaf in leafNodes)
            {
                int depth = GetDepth(leaf);
                if (startDepth < depth)
                {
                    startDepth = depth;
                    deepestLeaf = leaf;
                }
            }

            return deepestLeaf;
        }

        private int GetDepth(Tree<T> currentNode)
        {
            int depth = 0;
            while (currentNode.Parent != null)
            {
                depth++;
                currentNode = currentNode.Parent;
            }

            return depth;
        }

        private List<Tree<T>> OrderBfsNodes()
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                result.Add(currentNode);

                foreach (var currentNodeChild in currentNode.Children)
                {
                    nodes.Enqueue(currentNodeChild);
                }
            }
            return result;
        }

        public List<T> GetLeafKeys()
        {
            var leafNodes = GetLeafs();
            return leafNodes.Select(x => x.Key).ToList();
            //List<T> leafValues = new List<T>();
            //foreach (var leaf in leafNodes)
            //{
            //    leafValues.Add(leaf.Key);
            //}

            //return leafValues;
        }

        public List<T> GetMiddleKeys()
        {
            var middleNodes = OrderBfsNodes().Where(node => node.Parent != null && node.Children.Count > 0);
            List<T> middleKeys = new List<T>();
            foreach (var node in middleNodes)
            {
                middleKeys.Add(node.Key);
            }

            return middleKeys;
        }

        public List<T> GetLongestPath()
        {
            var deepestLeaf = this.GetDeepestLeftomostNode();
            Stack<T> pathValues = new Stack<T>();

            while (deepestLeaf != null)
            {
                pathValues.Push(deepestLeaf.Key);
                deepestLeaf = deepestLeaf.Parent;
            }

            return new List<T>(pathValues);
        }

        public List<List<T>> PathsWithGivenSum2(int sum)
        {
            var leafs = GetLeafs();
            var result = new List<List<T>>();
            foreach (var leaf in leafs)
            {
                var node = leaf;
                var currentSum = 0;
                var currentNodes = new List<T>();
                while (node != null)
                {
                    currentNodes.Add(node.Key);
                    currentSum += int.Parse(node.Key.ToString());
                    node = node.Parent;
                }

                if (currentSum == sum)
                {
                    currentNodes.Reverse();
                    result.Add(currentNodes);
                }
            }
            return result;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();
            var path = new List<T>();
            path.Add(this.Key);
            int currentSum = Convert.ToInt32(this.Key);
            this.Dfs(this, result, path, sum, ref currentSum);

            return result;
        }

        private void Dfs(Tree<T> current, List<List<T>> result, List<T> currentPath, int sumToCompare, ref int currentSum)
        {
            foreach (var child in current.Children)
            {
                currentPath.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);
                Dfs(child, result, currentPath, sumToCompare, ref currentSum);
            }

            if (currentSum == sumToCompare)
            {
                result.Add(new List<T>(currentPath));
            }

            currentSum -= Convert.ToInt32(current.Key);
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            this.DfsSerch(this, sum, 0, result);
            return result;
        }

        private int DfsSerch(Tree<T> tree, int sum, int v, List<Tree<T>> result)
        {
            v = int.Parse(tree.Key.ToString());
            foreach (var child in tree.Children)
            {
                v += DfsSerch(child, sum, v, result);
            }

            if (v == sum)
            {
                result.Add(tree);
            }

            return v;
        }

        public List<Tree<T>> SubTreesWithGivenSum2(int sum)
        {
            var subtreenodes = OrderBfsNodes().Where(x => x.Children.Count > 0);
            var currentSum = 0;
            var result = new List<Tree<T>>();
            foreach (var root in subtreenodes)
            {
                currentSum += int.Parse(root.Key.ToString());

                foreach (var child in root.Children)
                {
                    currentSum += int.Parse(child.Key.ToString());
                }

                if (currentSum == sum)
                {
                    result.Add(root);
                }

                currentSum = 0;
            }

            return result;
        }

        private IOrderedEnumerable<Tree<T>> GetLeafs()
        {
            var leafNodes = OrderBfsNodes().Where(node => node.Children.Count == 0).OrderBy(x => x.Key);
            return leafNodes;
        }
    }
}
