using Collections.Tree.BTree;
using Collections.Tree.Exceptions;
using Xunit;

namespace CollectionsTests
{
    public class BinaryTree_Remove
    {
        [Fact]
        public void Remove_EmptyTree_ThrowsException()
        {
            var tree = new BinaryTree<int>();

            Assert.Throws<InvalidTreeStateException>(() =>
            {
                tree.Remove(5);
            });
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-9)]
        public void Remove_RemoveNonExistinItem_ReturnsFalse(int v)
        {
            var tree = GetTreeFixture();

            Assert.False(tree.Remove(v));
        }

        [Fact]
        public void Remove_OneElementTree_ReturnsTrue()
        {
            var tree = new BinaryTree<int>(5);

            Assert.True(tree.Remove(5));
            Assert.Empty(tree);
        }

        [Fact]
        public void Remove_Root_ReturnsTrue()
        {
            var tree = GetTreeFixture(5);
            var len = tree.Count;

            Assert.True(tree.Remove(5));
            Assert.Equal(len - 1, tree.Count);
            Assert.Equal(6, tree.Root);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(9)]
        [InlineData(6)]
        [InlineData(-1)]
        public void Remove_RemoveItem_ReturnsTrue(int v)
        {
            var tree = GetTreeFixture();
            var len = tree.Count;

            Assert.True(tree.Remove(v));
            Assert.Equal(len - 1, tree.Count);
        }

        private static BinaryTree<int> GetTreeFixture(int root = 5)
        {
            int[] elems = { 5, 3, 7, -1, 6, 9, 1, 8, 2 };

            var tree = new BinaryTree<int>(root);
            foreach(var e in elems)
            {
                tree.Add(e);
            }

            return tree;
        }
    }
}
