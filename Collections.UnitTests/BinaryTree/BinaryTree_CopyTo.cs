using Collections.Tree.BTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Collections.UnitTests.BinaryTree
{
    public class BinaryTreeCopyTo
    {
        [Fact]
        public void CopyTo_ThrowsArgumentNullException()
        {
            var tree = new BinaryTree<int>(5);

            Assert.Throws<ArgumentNullException>(() =>
            {
                tree.CopyTo(null, 0);
            });
        }

        [Fact]
        public void CopyTo_ThrowsArgumentOutOfRangeException()
        {
            var tree = new BinaryTree<int>(5);
            var dest = new int[1];

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                tree.CopyTo(dest, -10);
            });
        }

        [Fact]
        public void CopyTo_DestinationTooSmall_ThrowsArgumentException()
        {
            var tree = new BinaryTree<int>(5);
            tree.Add(2);

            var dest = new int[2];

            Assert.Throws<ArgumentException>(() =>
            {
                tree.CopyTo(dest, 1);
            });
        }

        [Fact]
        public void CopyTo_ZeroOffset_Success()
        {
            var elems = new int[] { 1, 2, 3, 4 };
            var tree = new BinaryTree<int>();
            foreach(var e in elems)
            {
                tree.Add(e);
            }

            var dest = new int[4];

            tree.CopyTo(dest, 0);

            Assert.Equal(elems, dest);
        }

        [Fact]
        public void CopyTo_NonZeroOffset_Success()
        {
            var elems = new int[] { 1, 2, 3, 4 };
            var tree = new BinaryTree<int>();
            foreach (var e in elems)
            {
                tree.Add(e);
            }

            var dest = new int[6];

            tree.CopyTo(dest, 2);

            Assert.Equal(new int[] { 0, 0, 1, 2, 3, 4 }, dest);
        }
    }
}
