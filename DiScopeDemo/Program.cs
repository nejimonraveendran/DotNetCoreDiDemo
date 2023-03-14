using DiScopeDemo.Models;
using DiScopeDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DiScopeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scope Demo");

     

            var serviceProvider = new ServiceCollection()
                .AddScoped<FirstRunner>()
                .AddScoped<SecondRunner>()
                .AddScoped<IFirstService, FirstService>()
                .AddScoped<ISecondService, SecondService>()

                //IMyModel is injected into both IFirstService and ISecondService.  Depending upon the type of DI used, its behvavior will be different.
                //Change this to AddScoped, AddTransient, and AddSingleton and observe the change in behavior of FirstRunner and SecondRunner classes.
                .AddSingleton<IMyModel, MyModel>() 
                
                .BuildServiceProvider();




            //We create 2 scopes to mimic 2 consecutive requests below
            //scope 1 (mimics request 1 in a web app)
            using (var scope1 = serviceProvider.CreateScope()) 
            {
                var runner = scope1.ServiceProvider.GetRequiredService<FirstRunner>();
                runner.Run();
            }

            //scope 2 (mimics request 2 in a web app)
            using (var scope2 = serviceProvider.CreateScope())
            {
                var runner = scope2.ServiceProvider.GetRequiredService<SecondRunner>();
                runner.Run();
            }

            Console.ReadLine();
        }
    }
}
    