using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Universal.Managers
{
    public class ExceptionManager
    {
        public static void Launch(Exception exception, [CallerMemberName] string caller = "",[CallerFilePath] string file = "")
        {
            throw new UniversalException(exception,caller,file);
        }
    }

    public class UniversalException : Exception
    {
        public Exception OriginalException;
        public String Method;
        public String File;

        public UniversalException(Exception originalException, string method,string file)
        {
            OriginalException = originalException;
            Method = method;
            File = file;
            Debug.WriteLine(method);
            Debug.WriteLine(file);
        }
    }
}
