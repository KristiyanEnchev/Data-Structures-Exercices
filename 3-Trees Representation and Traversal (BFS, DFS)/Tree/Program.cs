using System;
using System.Collections.Generic;
using System.Text;

namespace Tree
{
    public class Program
    {
        static void Main(string[] areg)
        {
            Tree<int> root = new Tree<int>(7,
                new Tree<int>(19,
                    new Tree<int>(1),
                    new Tree<int>(12),
                    new Tree<int>(31)),
                new Tree<int>(21),
                new Tree<int>(14,
                    new Tree<int>(23),
                    new Tree<int>(6))
                );
        }
    }
}
