using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzMachineLearning.Interfaces
{
    internal interface IMachineLearningModel
    {
        public void Fit(double[][] x, double y);

        public double[] Predict(double[][] x);
    }
}
