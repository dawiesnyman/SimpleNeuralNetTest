using System;

namespace NeuralNet.Functions
{
    public class FunctionFactory
    {
        public static Func<double, double, double, double> GetWeightAdjustFunction(eWeightAdjustment weightAdjustment)
        {
            switch (weightAdjustment)
            {
                case eWeightAdjustment.Simple:
                default:
                    return new Func<double, double, double, double>((i, o, e) =>
                    {
                        return e * i * o * (1 - o);
                    });
            }
        }
        public static Func<double, double> GetActivationFunction(eActivationFunc act)
        {
            switch (act)
            {
                case eActivationFunc.Sigmoid:
                    return new Func<double, double>((z) =>
                    {
                        if (z < -20.0) return 0.0;
                        else if (z > 20.0) return 1.0;
                        else return 1.0 / (1.0 + Math.Exp(-z));
                    });
                case eActivationFunc.Step: //make step our default
                default:
                    return new Func<double, double>((z) =>
                    {
                        return z > 0 ? 1 : 0;
                    });              
            }
        }
    }
}
