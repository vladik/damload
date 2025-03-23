using DamLoad.Abstractions.Modules;
using DamLoad.Abstractions.Startup;
using DamLoad.Assets.Contracts;
using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;
using DamLoad.Assets.Services;
using DamLoad.Core.Contracts;
using DamLoad.Data.Database;
using DamLoad.Workflow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace DamLoad.Assets
{
    public class Module : IModule
    {
        public void Register(IServiceCollection services)
        {


            //services.AddScoped<TestService>();
            //services.AddSingleton<IStartupHook, StartupHook>();

        }
    }
}
