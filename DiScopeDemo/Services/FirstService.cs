using DiScopeDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiScopeDemo.Services
{
    class FirstService : IFirstService
    {
        IMyModel _myModel;

        public FirstService(IMyModel myModel)
        {
            _myModel = myModel;
        }

        public int SetValue(int value)
        {
            _myModel.MyModelValue = value;
            return _myModel.MyModelValue;
        }

    }
}
