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

        public BaseNode()
        {

        }
        public BaseNode(int address)
        {
            _address = address;
        }
        public int Address => _address;
    }
}
