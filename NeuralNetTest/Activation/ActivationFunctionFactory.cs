using System;

namespace NeuralNetTest.Activation
{
    public class ActivationFunctionFactory
    {
        public static Func<double, double> GetFunction(eActivationFunc act)
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
