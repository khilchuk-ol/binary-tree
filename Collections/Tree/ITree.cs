using System.Collections.Generic;

namespace Collections.Tree
{
    //
    // Summary:
    //     Defines methods to manipulate generic tree structures.
    //
    // Type parameters:
    //   T:
    //     The type of the elements in the tree.
    public interface ITree<T> : ICollection<T>, IEnumerable<T>
    {
        //
        // Summary:
        //     Returns an enumerator that iterates through the collection. 
        //     Depth-first traversal algorithm is used for basic iteration.
        //
        // Parameters:
        //   order:
        //     Specifies order, in which to walk the tree for iteration.
        //     Possible values:
        //         non-negative integer - pre (ascending) order
        //         negative integer - reverse (descending) order
        //     Default value: 0
        //
        // Returns:
        //     An enumerator that can be used to iterate through the collection.
        IEnumerator<T> GetEnumerator(int order = 0);

        //
        // Summary:
        //     Returns an enumerator that iterates through the collection. 
        //     Breadth-first traversal algorithm implementation. 
        //
        // Returns:
        //     An enumerator that can be used to iterate through the collection.
        IEnumerator<T> BreadthFirstEnumerator();
    }
}
