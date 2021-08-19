using DiScopeDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiScopeDemo.Services
{
    class SecondService : ISecondService
    {
        IMyModel _myModel;

        public SecondService(IMyModel myModel)
        {
            _myModel = myModel;
        }

        public int GetCurrentValue ()
        {
            return _myModel.MyModelValue;
        }
    }
}
