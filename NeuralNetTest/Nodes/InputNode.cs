using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public class InputNode : BaseNode, IInputNode
    {
        private readonly List<HiddenNode> _hiddenNodes;
        private double _input;

        public InputNode(int address, double weight) : base(address)
        {
            Weight = weight;
            _hiddenNodes = new List<HiddenNode>();
        }
        public double InValue { get; set; }
        public double Weight { get; set; }
        public double Output { get => _input; }
        public double Input { get { return _input; } set { _input = value; } }
        public IEnumerable<HiddenNode> HiddenNodes => _hiddenNodes;
    }
}
