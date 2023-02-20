using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzMachineLearning.Exceptions;

namespace EzMachineLearning.LossFunctions
{
    public static class LossFunctions
    {
        public static Dictionary<LossFunc, Func<double[], double>> LossFuncs { get; }
            = new Dictionary<LossFunc, Func<double[], double>>
            {
                { LossFunc.MeanSquaredError, MeanSquaredError },
                { LossFunc.MSE, MeanSquaredError },
                { LossFunc.RootMeanSquaredError, RootMeanSquaredError },
                { LossFunc.RMSE, RootMeanSquaredError },
                { LossFunc.MeanAbsoluteError, MeanAbsoluteError },
                { LossFunc.MAE, MeanAbsoluteError }
            };

        public static double MeanSquaredError(double[] error)
        {
            return -1 * error.Select(a => Math.Pow(a, 2)).Average();
        }

        public static double RootMeanSquaredError(double[] error)
        {
            return -1 * Math.Sqrt(MeanSquaredError(error));
        }

        public static double MeanAbsoluteError(double[] error)
        {
            return -1 * error.Select(a => Math.Abs(a)).Average();
        }
    }

    public enum LossFunc
    {
        MeanSquaredError,
        MSE,
        RootMeanSquaredError,
        RMSE,
        MeanAbsoluteError,
        MAE
    }
}
