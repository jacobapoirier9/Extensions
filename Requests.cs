using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    [Obsolete]
    public class ExecuteMethodRequest
    {
        public string MethodName { get; set; }

        public object[] Args { get; set; }
    }

    
}
