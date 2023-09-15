using System;

namespace UTests
{
    public sealed class AssertException : Exception
    {
        public AssertException(string text) : base(text) { }
    }
}