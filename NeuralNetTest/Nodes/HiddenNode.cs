using System;
using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public class HiddenNode : BaseOutputNode, IInputNode
    {
        public HiddenNode(int address, Func<double, double> activationFunction, Func<double, double, double, double> weightAdjustmentFunction) 
            : base(address, activationFunction, weightAdjustmentFunction)
        {
            Weights = new Dictionary<int, double>();
        }

        public IDictionary<int, double> Weights { get; set; }
        public double Input { get { return Output; } set { Output = value; } }
    }
}
