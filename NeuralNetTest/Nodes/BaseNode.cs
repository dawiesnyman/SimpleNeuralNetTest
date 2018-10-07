using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Nodes
{
    public abstract class BaseNode : IBaseNode
    {
        private readonly int _address;
        private readonly int _layerNo;

        public BaseNode()
        {

        }
        public BaseNode(int address)
        {
            _address = address;
        }
        public int Address => _address;
        public int LayerNo => _layerNo;
    }
}
