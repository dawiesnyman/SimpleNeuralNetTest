using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Nodes
{
    public class Address
    {
        public Address()
        {
        }

        public Address(int layer, int nodeNumber)
        {
            Layer = layer;
            NodeNumber = nodeNumber;
        }
        public int Layer { get; private set; }
        public int NodeNumber { get; private set; }

        public override string ToString()
        {
            return $"{Layer}, {NodeNumber}";
        }

        
    }
}
