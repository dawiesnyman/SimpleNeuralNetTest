using System;

namespace NeuralNetTest.Nodes
{
    public class HiddenNode : BaseOutputNode, IInputNode
    {
        public HiddenNode(int address, Func<double, double> activationFunction, Func<double, double, double, double> weightAdjustmentFunction) 
            : base(address, activationFunction, weightAdjustmentFunction)
        {

        }

        public double Weight { get; set; }
        public double Input { get { return Output; } set { Output = value; } }
    }
}
