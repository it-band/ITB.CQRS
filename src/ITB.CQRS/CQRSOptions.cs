using System;
using ITB.ResultModel;
using Microsoft.Extensions.Logging;

namespace ITB.CQRS
{
    public class CQRSOptions
    {
        public Func<Exception, ILogger, ExceptionFailure> ExceptionHandler { get; set; } = (exception, logger) =>
        {
            logger.LogError(exception, exception.Message);

            return new ExceptionFailure(exception);
        };
    }
}
