using DiScopeDemo.Models;
using DiScopeDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiScopeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var serviceProvider = new ServiceCollection()
                .AddScoped<IRunner, Runner>()
                .AddScoped<IFirstService, FirstService>()
                .AddScoped<ISecondService, SecondService>()
                .AddTransient<IMyModel, MyModel>() //change this to AddScoped and debug the Runner class to see the difference in behavior (refer to notes in Runner class for more info)
                .BuildServiceProvider();



            using (var scope = serviceProvider.CreateScope()) 
            {
                var runner = scope.ServiceProvider.GetRequiredService<IRunner>();
                runner.Run();
            }
        }
    }
}
