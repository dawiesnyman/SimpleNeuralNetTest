using NeuralNetTest.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Layers
{
    public class OutputLayer : IEnumerable<OutputNode>
    {
        private double[] _expectedOutput;
        private readonly List<OutputNode> _outputNodes;

        public OutputLayer()
        {
            _outputNodes = new List<OutputNode>();
        }
        public IEnumerator<OutputNode> GetEnumerator()
        {
            return _outputNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _outputNodes.GetEnumerator();
        }

        public void Add(OutputNode node)
        {
            _outputNodes.Add(node);
        }

        public void ForEach(Action<OutputNode> action)
        {
            _outputNodes.ForEach(action);
        }

        public OutputLayer SetExpectedOutput(double[] output)
        {
            if (output.Length != _outputNodes.Count)
            {
                throw new Exception("Input array length does not equal the input nodes");
            }
            _expectedOutput = output;

            return this;
        }

        public double GetExpectedOutput(int id)
        {
            return _expectedOutput[id];
        }
    }
}
