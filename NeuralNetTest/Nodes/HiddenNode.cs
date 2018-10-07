using System;
using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public class HiddenNode : BaseOutputNode, IInputNode
    {
        public HiddenNode(Address address, Func<double, double> activationFunction, 
            Func<double, double, double, double> weightAdjustmentFunction,
            Func<double> getRandom = null) 
            : base(address, activationFunction, weightAdjustmentFunction, getRandom)
        {
            Weights = new Dictionary<string, double>();
        }

        public IDictionary<string, double> Weights { get; set; }
        public double Input { get { return Output; } set { Output = value; } }
    }
}
