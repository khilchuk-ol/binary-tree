using Collections.Tree.BTree;
using Collections.Tree.Events;
using System;

namespace PlaygroundCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinaryTree<int>();

            tree.NodeAdded += HandleNodeAdded;
            tree.TreeCleared += HandleTreeCleared<int>;
            
            try
            {
                tree.Remove(1);
            } catch(Exception e)
            {
                Console.WriteLine("Exception caught: ", e);
            }

            Console.WriteLine($"Created new tree {tree.GetType().Name}");
            DisplayTreeInfo(tree);

            tree.Add(5);
            tree.Add(3);
            tree.Add(3);
            tree.Add(2);
            tree.Add(10);
            tree.Add(9);
            tree.Add(12);

            DisplayTreeInfo(tree);
            PrintTree(tree);

            Console.WriteLine($"tree contains 2 = {tree.Contains(2)}");
            Console.WriteLine($"tree contains 0 = {tree.Contains(0)}");

            tree.Remove(1);
            DisplayTreeInfo(tree);
            PrintTree(tree);

            tree.Remove(2);
            DisplayTreeInfo(tree);
            PrintTree(tree);

            tree.Remove(10);
            DisplayTreeInfo(tree);
            PrintTree(tree);

            tree.Remove(5);
            DisplayTreeInfo(tree);
            PrintTree(tree);

            Console.Write("Breadth-first traversal: ");
            var bfi = tree.BreadthFirstEnumerator();
            foreach(var item in tree.BreadthFirstWalk())
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();

            tree.Clear();
            DisplayTreeInfo(tree);

            Console.ReadLine();
        }

        private static void DisplayTreeInfo<T>(BinaryTree<T> tree) where T : IComparable<T>
        {
            Console.WriteLine($"{"Current length",14}: {tree.Count} \n {"Current root",14}: {tree.Root}");
        }

        private static void PrintTree<T>(BinaryTree<T> tree) where T : IComparable<T>
        {
            foreach(var item in tree)
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine();
        }

        private static void HandleNodeAdded<T>(Node<T> node, Node<T> parent) where T : IComparable<T>
        {
            Console.WriteLine($"new node ({node.Value}) inserted. parent ({(parent != null ? parent.Value : "null")})");
        }

        private static void HandleTreeCleared<T>(object sender, TreeClearedEventArgs args) where T : IComparable<T>
        {
            Console.WriteLine($"tree has been cleared. amount of nodes cleared = {args.CountOfNodes} [text: {args.Text}]");
        }
    }
}
