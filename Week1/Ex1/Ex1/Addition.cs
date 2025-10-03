using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    internal class Addition : ICalculate
    {
        public double Execute(double a, double b)=>a+b;
    }
}
