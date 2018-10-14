using NeuralNet.Models;
using System;
using System.Collections.Generic;

namespace Anna.Nodes
{
    public class HiddenNode : BaseOutputNode, IInputNode
    {
        public HiddenNode(int address, Func<double, double> activationFunction, 
            Func<TrainingCalcModel, double> weightAdjustmentFunction,
            Func<double> getRandom = null) 
            : base(address, activationFunction, weightAdjustmentFunction, getRandom)
        {
            Weights = new Dictionary<int, double>();
        }

        public IDictionary<int, double> Weights { get; set; }
        public double Input { get { return Output; } set { Output = value; } }
    }
}
