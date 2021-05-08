using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public class MemberDoesNotExistException : Exception
    {
        [Obsolete]
        public MemberDoesNotExistException() : base("Member does not exist") { }
    }

    public class MismatchedReturnTypesException : Exception
    {
        [Obsolete]
        public MismatchedReturnTypesException() : base("The given return type does not match the expected return type") { }
    }

    public class KeyNotFoundException : Exception
    {
        [Obsolete]
        public KeyNotFoundException() : base("Key not found") { }

        public KeyNotFoundException(string key) : base($"Key '{key}' was not found") { }
    }

    public class SectionNotFoundException : Exception
    {
        public SectionNotFoundException(string section) : base($"Section '{section}' was not found") { }
    }

    public class KeyAlreadyExistsException : Exception
    {
        [Obsolete]
        public KeyAlreadyExistsException() : base("Key already exists") { }
    }
}
