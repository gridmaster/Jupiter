using System;
using LogWriter4.Core.Interface;
using Services.Interface;

namespace Services.Service
{
    public class MyOtherService : BaseService, IMyOtherService
    {
        public MyOtherService(ILogger logger) : base(logger)
        {
            ThrowIfIsInitialized();
            IsInitialized = true;
        }

        public void DoSomethingElse(int id)
        {
            ThrowIfNotInitialized();

            logger.DebugFormat("{0}write something here: {1}", Environment.NewLine, "I did something else...");

            logger.ErrorFormat("Unable to process {4}{0}surveyId: {1}{0}token: {2}{0}{3}"
                                    , Environment.NewLine
                                    , 1
                                    , id
                                    , "Other Message!"
                                    , GetThisMethodName());
        }
    }
}
