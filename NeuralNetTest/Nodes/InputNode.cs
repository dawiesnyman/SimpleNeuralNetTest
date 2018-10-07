using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public class InputNode : BaseNode, IInputNode
    {
        private readonly List<HiddenNode> _hiddenNodes;
        private readonly Dictionary<string, double> _weights;
        private double _input;

        public InputNode(int address) : base(new Address(0, address))
        {

            _weights = new Dictionary<string, double>();
            _hiddenNodes = new List<HiddenNode>();
        }
        public InputNode(Address address) : base(address)
        {
            _weights = new Dictionary<string, double>();
            _hiddenNodes = new List<HiddenNode>();
        }
        public double InValue { get; set; }
        public IDictionary<string, double> Weights => _weights;
        public double Output { get => _input; }
        public double Input { get { return _input; } set { _input = value; } }
        public IEnumerable<HiddenNode> HiddenNodes => _hiddenNodes;
    }
}
