using System;
using System.Collections.Generic;
using System.Text;

namespace DiScopeDemo.Models
{
    class MyModel : IMyModel
    {
        private int _myModelValue;

        public int MyModelValue 
        { 
            get => _myModelValue; 
            set => _myModelValue = value; 
        }
    }
}
