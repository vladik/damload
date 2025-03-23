﻿using DamLoad.Abstractions.Modules;
using DamLoad.Abstractions.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace DamLoad.Assets
{
    public class Module : BaseModule
    {
        public override void Register(IServiceCollection services)
        {
            services.AddSingleton<IStartupHook, StartupHook>();
        }
    }
}
