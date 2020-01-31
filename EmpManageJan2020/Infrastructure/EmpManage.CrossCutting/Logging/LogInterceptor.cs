namespace EmpManage.CrossCutting.Logging
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;
    using NLog;

    //Follow anti-pattern only
    public class LogInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public LogInterceptor(ILogger logger)
        {
            this._logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var codeBase = invocation.MethodInvocationTarget.DeclaringType.AssemblyQualifiedName;
            var invocationTarget = invocation.InvocationTarget.ToString();
            var methodName = invocation.Method.Name;

            try
            {
                LogMethodEvent("MethodStart", codeBase, this._logger, invocationTarget, methodName);

                invocation.Proceed();
                var method = invocation.MethodInvocationTarget;

                if (typeof(Task).IsAssignableFrom(method.ReturnType))
                {
                    invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, this._logger, invocationTarget, methodName, codeBase);
                }
                else
                {
                    LogMethodEvent("MethodEnd", codeBase, this._logger, invocationTarget, methodName);
                }
            }
            catch (Exception ex)
            {
                LogMethodEvent("MethodError", codeBase, this._logger, invocationTarget, methodName, ex);

                throw;
            }
        }

        private static async Task InterceptAsync(Task task, ILogger _logger, string invocationTarget, string methodName, string codeBase)
        {
            try
            {
                await task.ConfigureAwait(false);

                LogMethodEvent("MethodEnd", codeBase, _logger, invocationTarget, methodName);
            }
            catch (Exception ex)
            {
                LogMethodEvent("MethodError", codeBase, _logger, invocationTarget, methodName, ex);

                throw;
            }
        }

        private static async Task<T> InterceptAsync<T>(Task<T> task, ILogger _logger, string invocationTarget, string methodName, string codeBase)
        {
            try
            {
                T result = await task.ConfigureAwait(false);
                LogMethodEvent("MethodEnd", codeBase, _logger, invocationTarget, methodName);

                return result;
            }
            catch (Exception ex)
            {
                LogMethodEvent("MethodError", codeBase, _logger, invocationTarget, methodName, ex);

                throw;
            }
        }

        private static void LogMethodEvent(string logEvent, string codeBase
        , ILogger _logger, string invocationTarget
        , string methodName, Exception ex = null)
        {
            var logMethodEvent = new LogEventInfo();

            if (logEvent == "MethodStart")
            {
                logMethodEvent = LogEventInfo.Create(LogLevel.Info,
                 invocationTarget,
                 "Executing Method - " + methodName
                + " | Class - " + invocationTarget);
            }
            else if (logEvent == "MethodEnd")
            {
                logMethodEvent = LogEventInfo.Create(LogLevel.Info, invocationTarget,
                 "Successfuly Executed Method - " + methodName
                + " | Class - " + invocationTarget);
            }
            else if (logEvent == "MethodError")
            {
                logMethodEvent = LogEventInfo.Create(LogLevel.Error, invocationTarget,
                 ex, null, "Error Occured on Executing Method - " + methodName
                 + " | Class - " + invocationTarget +
                 " | Trace - " + ex.InnerException + ex.Message + ex.StackTrace);
            }

            logMethodEvent.SetCallerInfo(invocationTarget,
            methodName + " - ",
            codeBase, 0);
            _logger.Log(logMethodEvent);
        }
    }
}