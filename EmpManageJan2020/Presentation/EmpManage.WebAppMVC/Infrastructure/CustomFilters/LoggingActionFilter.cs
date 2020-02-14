namespace CompName.ManageStocks.WebAppMVC.Infrastructure.CustomFilters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using NLog;

    public class LoggingActionFilter : IAsyncActionFilter
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        private Logger _nlogger = LogManager.GetCurrentClassLogger(); // creates a logger using the class name

        public LoggingActionFilter(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<LoggingActionFilter>();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            this._nlogger = LogManager.GetLogger(context.ActionDescriptor.DisplayName);

            try
            {
                this._nlogger.Info(
                     "Executing Method - " + context.ActionDescriptor.DisplayName);

                var resultContext = await next();

                if (resultContext.Exception == null)
                {
                    this._nlogger.Info(
                             "Successfuly Executed Method - " + context.ActionDescriptor.DisplayName);
                }
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    this._nlogger.Error("Error Occured on Executing Method - " + resultContext.Exception.Message, resultContext.Exception);
#pragma warning restore CS0618 // Type or member is obsolete

                    throw resultContext.Exception;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}