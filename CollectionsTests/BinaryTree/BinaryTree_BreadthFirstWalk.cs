using Collections.Tree.BTree;
using Xunit;

namespace CollectionsTests
{
    public class BinaryTree_BreadthFirstWalk
    {
        [Fact]
        public void BreadthFirst_EmptyTree_NoElements()
        {
            var tree = new BinaryTree<int>();

            Assert.Empty(tree.BreadthFirstWalk());
        }

        [Fact]
        public void BreadthFirst_NonEmptyTree_IsCorrect()
        {
            int[] elemsByLevel = { 5, 3, 7, -1, 6, 9, 1, 8, 2 };

            var tree = new BinaryTree<int>();

            foreach(var elem in elemsByLevel)
            {
                tree.Add(elem);
            }

            var bf = tree.BreadthFirstWalk();

            Assert.Equal(elemsByLevel, bf);
        }
    }
}
