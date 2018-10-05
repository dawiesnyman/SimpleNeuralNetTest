using NeuralNetTest.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest
{
    public class Network
    {
        private List<InputNode> _inputNodes;
        private List<HiddenNode> _hiddenNodes;
        private List<BaseOutputNode> _outputNodes;

        public List<InputNode> InputNodes { get => _inputNodes; set => _inputNodes = value; }
        public List<HiddenNode> HiddenNodes { get => _hiddenNodes; set => _hiddenNodes = value; }
        public List<BaseOutputNode> OutputNodes { get => _outputNodes; set => _outputNodes = value; }

        public Network()
        {

        }
        public void SetInputs(object[][] input)
        {

        }

        public void FeedForward(int count)
        {
            _outputNodes.ForEach((o) =>
            {
                //calc each node in each layer
                for (int i = 0; i < count; i++)
                {

                }
            });
        }
    }
}
