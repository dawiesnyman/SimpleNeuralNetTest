using NeuralNet.Models;
using System;

namespace Anna.Nodes
{
    public class OutputNode : BaseOutputNode
    {
        public OutputNode(int address, Func<double, double> activationFunction, 
            Func<TrainingCalcModel, double> weightAdjustmentFunction,
            Func<double> getRandom = null) 
            : base(address, activationFunction, weightAdjustmentFunction, getRandom)
        {

        }
    }
}
