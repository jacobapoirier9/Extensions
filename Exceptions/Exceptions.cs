using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public class MemberDoesNotExistException : Exception
    {
        public MemberDoesNotExistException() : base("Member does not exist") { }
    }

    public class MismatchedReturnTypesException : Exception
    {
        public MismatchedReturnTypesException() : base("The given return type does not match the expected return type") { }
    }

    public class TooManyTablesOutputtedException : Exception
    {
        public TooManyTablesOutputtedException() : base("LinqPad query outtputted too many tables") { }
    }

    [Obsolete]
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException() : base("Key not found") { }
    }

    public class KeyAlreadyExistsException : Exception
    {
        public KeyAlreadyExistsException() : base("Key already exists") { }
    }
}
