using NeuralNetTest.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Layers
{
    public class InputLayer : IEnumerable<IInputNode>
    {
        private List<IInputNode> _inputNodes;

        public InputLayer()
        {
            _inputNodes = new List<IInputNode>();
        }
        public InputLayer(IEnumerable<IInputNode> inputNodes)
        {
            _inputNodes = new List<IInputNode>(inputNodes);
        }
        public IEnumerator<IInputNode> GetEnumerator()
        {
            return _inputNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inputNodes.GetEnumerator();
        }

        public void Add(IInputNode inputNode)
        {
            _inputNodes.Add(inputNode);
        }

        public void SetInput(double[] input)
        {
            if (input.Length != _inputNodes.Count)
            {
                throw new Exception("Input array lengt does not equal the input nodes");
            }

            for (int i = 0; i < input.Length; i++)
            {
                _inputNodes[i].Input = input[i];
            }
        }
        public IInputNode this[int key]
        {
            get
            {
                return _inputNodes[key];
            }
            set
            {
                _inputNodes[key] = value;
            }
        }
    }
}
