using System.Collections.Generic;

namespace NeuralNet.Nodes
{
    public interface IInputNode : IBaseNode
    {
        IDictionary<int, double> Weights { get; }
        double Input { get; set; }

    }
}
