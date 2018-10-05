using System;

namespace NeuralNetTest.Nodes
{
    public class OutputNode : BaseOutputNode
    {
        public OutputNode(int address, Func<double, double> activationFunction, Func<double, double, double, double> weightAdjustmentFunction) 
            : base(address, activationFunction, weightAdjustmentFunction)
        {

        }
    }
}
