using System;

namespace Collections.Tree.Exceptions
{
    public class InvalidTreeStateException : Exception
    {
        public string Comment { get; }

        public InvalidTreeStateException(string msg, string comment = null) : base(msg)
        {
            Comment = comment;
        }

        public override string ToString()
        {
            return $"{nameof(InvalidTreeStateException)} occured \nEXCEPTION: {Message} {(Comment != null ? $"[comment: {Comment}]" : "")}";
        }
    }
}
