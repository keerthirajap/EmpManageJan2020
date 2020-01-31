using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManage.WebAppMVC.Infrastructure.CustomFilters
{
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
                    this._nlogger.Error(
                          "Error Occured on Executing Method - "
                          + resultContext.Exception.Message
                          , resultContext.Exception);

                    throw resultContext.Exception;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}