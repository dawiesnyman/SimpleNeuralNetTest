using System;
using System.Collections.Generic;

namespace NeuralNetTest.Nodes
{
    public abstract class BaseOutputNode : BaseNode
    {
        private readonly Random _random = new Random();
        private readonly List<IInputNode> _inputNodes;
        private readonly List<BaseOutputNode> _outNodes;
        private readonly Func<double, double> _activationFunction;
        private Func<double, double, double, double> _weightAdjustmentFunction;
        private Func<double> _getRandom;

        public BaseOutputNode(int address, Func<double, double> activationFunction, 
            Func<double, double, double, double> weightAdjustmentFunction,
            Func<double> getRandom = null) : base(address)
        {
            if (getRandom == null) { _getRandom = (() => { return _random.NextDouble(); }); }
            else { _getRandom = getRandom; }

            _activationFunction = activationFunction;
            _weightAdjustmentFunction = weightAdjustmentFunction;
            _inputNodes = new List<IInputNode>();
            _outNodes = new List<BaseOutputNode>();
        }
        public void CalculateOutput()
        {
            double total = 0;
            foreach (var i in _inputNodes)
            {
                total += (i.Weights[Address] * i.Output);
            }
            total += Bias;

            Output = _activationFunction(total);
        }
        public void AdjustWeights(double expected)
        {
            double error = expected - Output;
            _inputNodes.ForEach((i) =>
            {
                i.Weights[Address] += _weightAdjustmentFunction(i.Output, Output, error);
            });
        }
        public IEnumerable<IInputNode> InputNodes => _inputNodes;
        public double Bias { get; set; }
        public double Output { get; protected set; }
        public void SetInput(double[] input)
        {
            if(input.Length != _inputNodes.Count)
            {
                throw new Exception("Input array lengt does not equal the input nodes");
            }

            for(int i = 0; i < input.Length; i++)
            {
                _inputNodes[i].Input = input[i];
            }
        }
        public void AddInputNode(IInputNode input)
        {
            _inputNodes.Add(input);
        }

        public void AddInputNodes(IEnumerable<IInputNode> inputnodes)
        {
            Random r = new Random();
            new List<IInputNode>(inputnodes).ForEach((i) =>
            {               
                i.Weights.Add(Address, r.NextDouble());
            });

            _inputNodes.AddRange(inputnodes);
        }
    }
}
