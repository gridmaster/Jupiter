using System;
using LogWriter4.Core.Interface;
using Services.Interface;

namespace Services.Service
{
    public class MyFakeService : BaseService, IMyFakeService
    {
        public MyFakeService(ILogger logger) : base(logger)
        {
            ThrowIfIsInitialized();
            IsInitialized = true;
        }

        public void DoSomething(int id)
        {
            ThrowIfNotInitialized();

            logger.DebugFormat("{0}write something here: {1}", Environment.NewLine, "I did something...");

            logger.ErrorFormat("Unable to process {4}{0}surveyId: {1}{0}token: {2}{0}{3}"
                                    , Environment.NewLine
                                    , 1
                                    , id
                                    , "Error Message!"
                                    , GetThisMethodName());
        }
    }
}
