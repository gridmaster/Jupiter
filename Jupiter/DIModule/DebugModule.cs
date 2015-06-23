// -----------------------------------------------------------------------
// <copyright file="DebugModule.cs" company="Magic FireFly">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

//using Core.Interface;
using Ninject.Modules;
using Jupiter.DIModule;
using Jupiter.Contracts;
using Logger;
using Jupiter.Core.Interface;

namespace Jupiter.DIModule
{
    class DebugModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<Log4NetLogger>().InSingletonScope()
                .WithConstructorArgument("loglevel", LogLevelEnum.Debug);
        }
    }
}
