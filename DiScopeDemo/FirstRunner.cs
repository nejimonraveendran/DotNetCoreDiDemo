using DiScopeDemo.Models;
using DiScopeDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiScopeDemo
{
    class FirstRunner : IRunner
    {
        IFirstService _firstService;
        ISecondService _secondService;

        public FirstRunner(IFirstService firstService, ISecondService secondService)
        {
            _firstService = firstService;
            _secondService = secondService;
        }


        public void Run()
        {
            //if you use .AddTransient<IMyModel, MyModel>(), a separate instance of MyModel class is injected into FirstService and SecondService, so _secondService.GetCurrentValue() returns a different value than the one set using _firstService.SetValue().
            //if you use .AddScoped<IMyModel, MyModel>(), the same instance of MyModel class is injected into FirstService and SecondService, so _secondService.GetCurrentValue() returns the same value set using _firstService.SetValue().  But this is persistence is applicable to only the scope1 in Program.cs.  In scope2, _secondService.GetCurrentValue() returns a different value. 
            //if you use .AddSingleton<IMyModel, MyModel>(), the same instance of MyModel class is injected into FirstService and SecondService to all scopes, so _secondService.GetCurrentValue() returns the same value set using _firstService.SetValue() within both scope1 and scope2.

            var setVal = _firstService.SetValue(10);
            Console.WriteLine($"Value set from scope1 is {setVal}");

            var curVal = _secondService.GetCurrentValue();
            Console.WriteLine($"Value read back within scope1 is {curVal}");

        }
    }



}
