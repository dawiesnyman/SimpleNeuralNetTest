using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Nodes
{
    public interface IBaseNode
    {
        int Address { get; }
        int LayerNo { get; }
    }
}
