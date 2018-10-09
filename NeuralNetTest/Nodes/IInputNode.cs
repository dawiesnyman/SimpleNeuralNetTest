using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public interface IInputNode : IBaseNode
    {
        IDictionary<int, double> Weights { get; }
        double Input { get; set; }

    }
}
