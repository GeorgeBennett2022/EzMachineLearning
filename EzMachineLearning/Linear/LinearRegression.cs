using System.Configuration;
using EzMachineLearning.LossFunctions;

namespace EzMachineLearning.Linear
{
    /// <summary>
    /// Simple Linear Regression Model
    /// </summary>
    internal class LinearRegressionModel
    {
        /// <summary>
        /// Simple Linear Regression Model
        /// </summary>
        /// <param name="x_dimensions">Number of features the model will expect</param>
        /// <param name="loss_function"></param>
        /// <param name="learning_rate"></param>
        public LinearRegressionModel(int x_dimensions,
                                     LossFunc loss_function = LossFunc.MeanSquaredError,
                                     double? learning_rate = null)
        {
            this.x_dimensions = x_dimensions;
            this.loss_function = loss_function;
            this.learning_rate = learning_rate ?? double.Parse(ConfigurationManager.AppSettings["DefaultLearningRate"]);
            this.weights = InitializeWeights(x_dimensions);
        }
        /// <summary>
        /// Number of features the model expects
        /// </summary>
        public int x_dimensions { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Different loss functions have their pros and cons.
        /// Mean Squared Error is good for low variance data.
        /// Mean Absolute Error is good for high variance data.
        /// </remarks>
        public LossFunc loss_function { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// A higher learning rate increases speed of learning, but can lead to overfitting.
        /// Learning rate is often called 'alpha'.
        /// </remarks>
        public double learning_rate { get; }

        /// <summary>
        /// Learned relationship(s) between the feature and target data
        /// </summary>
        public double[] weights { get; }
        
        /// <summary>
        /// Fits a set of features to the target
        /// </summary>
        /// <param name="x">feature array</param>
        /// <param name="y">target</param>
        public void Fit(double[][] x, double[] y)
        {
            double[] error = new double[y.Length];
            for(int i = 0; i < y.Length; i++)
            {
                error[i] = y[i] - Aggregate(x[i]);
            }

            double loss = LossFunctions.LossFunctions.LossFuncs[loss_function](error);

        }

        /// <summary>
        /// Function to aggregate feature data into target prediction data
        /// </summary>
        /// <param name="x">feature data</param>
        /// <returns></returns>
        private double Aggregate(double[] x)
        {
            double output = 0;
            for (int i = 0; i < x.Length; i++) output += weights[i] * x[i];
            return output;
        }

        /// <summary>
        /// Method to tune the weights of the model based on loss.
        /// Weights are insentivised in proportion to their magnitide
        /// </summary>
        /// <param name="loss"></param>
        private void Tune(double loss)
        {
            for(int i = 0; i < weights.Length; i++)
            {
                weights[i] = weights[i] - (loss * weights[i]);
            }
        }

        #region Helpers
        /// <summary>
        /// Initializes weights to positive or negative values close to zero;
        /// </summary>
        /// <param name="x_dimensions">number of features</param>
        /// <returns></returns>
        private double[] InitializeWeights(int x_dimensions)
        {
            Random rng = new Random();
            int seed = Convert.ToInt32(ConfigurationManager.AppSettings["WeightInitializationSeed"]);
            double[] init_weights = new double[x_dimensions + 1];
            for(int i = 0; i < init_weights.Length; i++)
            {
                init_weights[i] = rng.Next(seed) / seed * 1000d;
                if (rng.Next(2) > 0) init_weights[i] *= -1;
            }
            return init_weights;
        }
        #endregion Helpers
    }
}
