using Collections.Tree.BTree;
using Xunit;

namespace Collections.UnitTests.BinaryTree
{
    public class BinaryTreeAdd
    {
        [Fact]
        public void Add_TreeRootIsNull()
        {
            var v = 5;

            var tree = new BinaryTree<int>();
            tree.Add(v);

            Assert.Equal(v, tree.Root);
            Assert.Single(tree);
        }

        [Fact]
        public void Add_RootAndValueAreEqual()
        {
            var v = 5;

            var tree = new BinaryTree<int>(v);
            tree.Add(v);

            Assert.Single(tree);
        }

        [Fact]
        public void Add_ItemExists()
        {
            var v = 9;

            var tree = new BinaryTree<int>(7);
            tree.Add(9);

            tree.Add(v);

            Assert.Equal(2, tree.Count);
        }

        [Theory]
        [InlineData(new[] { 1, 2 })]
        [InlineData(new[] {3, -1})]
        [InlineData(new[] { 3, 1 })]
        public void Add_Success(int[] treeElems)
        {
            var v = 0;

            var tree = new BinaryTree<int>();
            
            foreach(var elem in treeElems)
            {
                tree.Add(elem);
            }

            tree.Add(v);

            Assert.Equal(treeElems.Length + 1, tree.Count);
        }
    }
}
