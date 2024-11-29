using Serilog;
using System.Diagnostics;
using System.Reflection;
using System;

namespace Todo.Common.Helpers
{
    public class FunctionTracer : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly string _className;
        private readonly string _methodName;
        private readonly bool _loginfile;
        private readonly ILogger _logger;

        public FunctionTracer(bool loginfile = false)
        {
            // Create a StackTrace to examine the call stack
            StackTrace stackTrace = new StackTrace();

            // Get the calling method (the second frame in the stack trace)
            StackFrame frame = stackTrace.GetFrame(1);
            MethodBase method = frame.GetMethod();

            // Get the class name and method name
            _className = (method.DeclaringType.FullName.ToLower().Contains("aspnetcore")) ? method.DeclaringType.DeclaringType.Name : method.DeclaringType.Name; // Class name
            _methodName = !(method.DeclaringType.FullName.ToLower().Contains("aspnetcore")) ? method.Name : "ScreenBinding";              // Method name
            _logger = Log.Logger;
            _loginfile = loginfile;
            if (_loginfile)
            {
                _stopwatch = Stopwatch.StartNew();
                _logger.Information($"{_className} -> {_methodName} - Execution Start");
            }
        }

        public void Dispose()
        {
            if (_loginfile)
            {
                _stopwatch.Stop();
                _logger.Information($"{_className} -> {_methodName} - Total Execution Time: {_stopwatch.Elapsed.TotalSeconds} sec(s)");
            }
        }
    }
}
