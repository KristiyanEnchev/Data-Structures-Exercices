namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            foreach (var item in input)
            {
                var line = item.Split(' ').Select(int.Parse).ToList();

                this.CreateNodeByKey(line[0]);
                this.CreateNodeByKey(line[1]);

                if (!this.nodesBykeys.ContainsKey(line[1]))
                {
                    var node = this.CreateNodeByKey(line[1]);
                    this.nodesBykeys.Add(line[1], node);
                }

                this.AddEdge(line[0], line[1]);
            }

            return GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            if (!this.nodesBykeys.ContainsKey(key))
            {
                this.nodesBykeys.Add(key, new Tree<int>(key));
            }

            return this.nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            this.nodesBykeys[parent].AddChild(this.nodesBykeys[child]);
            this.nodesBykeys[child].AddParent(this.nodesBykeys[parent]);
        }

        private Tree<int> GetRoot()
        {
            //Tree<int> node = null;
            //foreach (var key in this.nodesBykeys.Keys)
            //{
            //    node = FindNodeBfs(key);
            //    if (node.Parent == null)
            //    {
            //        return node;
            //    }
            //}
            //return node;

            Tree<int> node = this.nodesBykeys.FirstOrDefault().Value;

            while (node.Parent != null)
            {
                node = node.Parent;
            }

            return node;
        }

        private Tree<int> FindNodeBfs(int serchedValue)
        {
            Queue<Tree<int>> queue = new Queue<Tree<int>>();
            queue.Enqueue(this.nodesBykeys[serchedValue]);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (serchedValue.Equals(node))
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
    }
}
