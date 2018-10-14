using System.Collections.Generic;

namespace Anna.Nodes
{
    public interface IInputNode : IBaseNode
    {
        IDictionary<int, double> Weights { get; }
        double Input { get; set; }

    }
}
