using System;

namespace Collections.Tree.Events
{
    public class TreeClearedEventArgs : EventArgs
    {
        public int CountOfNodes { get; }

        public string Text { get; }

       public TreeClearedEventArgs(int countOfNodes, string text)
        {
            CountOfNodes = countOfNodes;
            Text = text;
        }
        
    }
}
