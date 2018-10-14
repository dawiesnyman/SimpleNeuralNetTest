using NeuralNet.Models;
using System;

namespace Anna.Functions
{
    public class FunctionFactory
    {
        private readonly double _trainingRate;
        private static FunctionFactory _instance;

        private FunctionFactory(double trainingRate = 0.0)
        {
            _trainingRate = trainingRate;
        }

        public static Func<TrainingCalcModel, double> GetWeightAdjustFunction(eWeightAdjustment weightAdjustment)
        {
            switch (weightAdjustment)
            {
                case eWeightAdjustment.SimpleGradient:
                    return new Func<TrainingCalcModel, double>((model) =>
                    {
                        return model.Weight + model.TrainingRate * (model.ExpectedOutput - model.ActualOutput) * model.Input;
                    });
                case eWeightAdjustment.Simple:
                default:
                    return new Func<TrainingCalcModel, double>((m) =>
                    {
                        return m.Error * m.Input * m.ActualOutput * (1 - m.ActualOutput);
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
