using Collections.Tree.BTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CollectionsTests
{
    public class BinaryTree_Contains
    {
        [Theory]
        [InlineData(5, true)]
        [InlineData(9, true)]
        [InlineData(-1, true)]
        [InlineData(2, true)]
        [InlineData(0, false)]
        public void Contains_IsCorrect(int v, bool expected)
        {
            int[] elems = { 5, 3, 7, -1, 6, 9, 1, 8, 2 };

            var tree = new BinaryTree<int>();

            foreach (var elem in elems)
            {
                tree.Add(elem);
            }

            Assert.Equal(expected, tree.Contains(v));
        }
    }
}
