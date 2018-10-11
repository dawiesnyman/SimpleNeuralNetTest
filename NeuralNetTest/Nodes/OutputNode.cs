using System;

namespace NeuralNet.Nodes
{
    public class OutputNode : BaseOutputNode
    {
        public OutputNode(int address, Func<double, double> activationFunction, 
            Func<double, double, double, double> weightAdjustmentFunction,
            Func<double> getRandom = null) 
            : base(address, activationFunction, weightAdjustmentFunction, getRandom)
        {

        }
    }
}
