using System;
using System.Collections.Generic;

namespace NeuralNet.Nodes
{
    public class HiddenNode : BaseOutputNode, IInputNode
    {
        public HiddenNode(int address, Func<double, double> activationFunction, 
            Func<double, double, double, double> weightAdjustmentFunction,
            Func<double> getRandom = null) 
            : base(address, activationFunction, weightAdjustmentFunction, getRandom)
        {
            Weights = new Dictionary<int, double>();
        }

        public IDictionary<int, double> Weights { get; set; }
        public double Input { get { return Output; } set { Output = value; } }
    }
}
