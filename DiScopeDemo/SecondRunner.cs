using DiScopeDemo.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiScopeDemo
{
    class SecondRunner : IRunner
    {
        IFirstService _firstService;
        ISecondService _secondService;

        public SecondRunner(IFirstService firstService, ISecondService secondService)
        {
            _firstService = firstService;
            _secondService = secondService;
        }


        public void Run()
        {
            //if you use .AddTransient<IMyModel, MyModel>(), you can see that a separate instance of MyModel class is injected into FirstService and SecondService, thus the val1 and val2 are different.
            //if you use .AddScoped<IMyModel, MyModel>(), you can see that the same instance of MyModel class is injected into FirstService and SecondService, thus the val1 and val2 are the same.

            var curVal = _secondService.GetCurrentValue();
            Console.WriteLine($"Value read back within scope2 is  {curVal}");
        }
    }
}
