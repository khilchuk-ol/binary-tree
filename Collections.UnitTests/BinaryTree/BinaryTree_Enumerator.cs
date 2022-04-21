using Collections.Tree.BTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Collections.UnitTests.BinaryTree
{
    public class BinaryTreeEnumerator
    {
        [Fact]
        public void Enumerate_EmptyRoot()
        {
            var tree = new BinaryTree<int>();
            var res = new List<int>();
            
            foreach(var e in tree)
            {
                res.Add(e);
            }

            Assert.Empty(res);
        }

        [Fact]
        public void Enumerate_Ascending_Correct()
        {
            var elems = new int[] { 3, 2, 5, 6, 1, 7, -1 };

            var tree = new BinaryTree<int>();
            foreach(var e in elems)
            {
                tree.Add(e);
            }

            var res = new List<int>();

            foreach (var e in tree)
            {
                res.Add(e);
            }

            Array.Sort(elems);

            Assert.Equal(elems, res);
        }

        [Fact]
        public void Enumerate_Descending_Correct()
        {
            var elems = new int[] { 3, 2, 5, 6, 1, 7, -1 };

            var tree = new BinaryTree<int>();
            foreach (var e in elems)
            {
                tree.Add(e);
            }

            var res = new List<int>();

            using(var it = tree.GetEnumerator(-1))
            {
                while (it.MoveNext())
                {
                    res.Add(it.Current);
                }
            }

            Array.Sort(elems);
            Array.Reverse(elems);

            Assert.Equal(elems, res);
        }
    }
}
