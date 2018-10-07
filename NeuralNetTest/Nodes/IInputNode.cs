using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public interface IInputNode : IBaseNode
    {
        IDictionary<string, double> Weights { get; }
        double Output { get; }
        double Input { get; set; }

    }
}
