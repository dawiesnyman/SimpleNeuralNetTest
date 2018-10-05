using NeuralNetTest.Nodes;
using System;
using System.Collections.Generic;

namespace NeuralNetTest
{
    public class NetworkFactory
    {
        private readonly int _inputNodeCount;
        private readonly int _hiddenNodeCount;
        private readonly int _outPutNodeCount;
        private readonly Func<double, double> _activationFunction;
        private readonly Func<double, double, double, double> _weightAdjustFunction;

        private List<InputNode> _inputNodes;
        private List<HiddenNode> _hiddenNodes;
        private List<BaseOutputNode> _outputNodes;

        public NetworkFactory(int inputNodeCount, int hiddenNodeCount, int outPutNodeCount, 
            Func<double, double> activationFunction, Func<double, double, double, double> weightAdjustmentFunction)
        {
            _inputNodeCount = inputNodeCount;
            _hiddenNodeCount = hiddenNodeCount;
            _outPutNodeCount = outPutNodeCount;
            _activationFunction = activationFunction;
            _weightAdjustFunction = weightAdjustmentFunction;

            _inputNodes = new List<InputNode>();
            _hiddenNodes = new List<HiddenNode>();
            _outputNodes = new List<BaseOutputNode>();
        }

        public Network BuildNetwork()
        {
            var network = new Network();
            
            //todo: min max to be set from input min/max?
            Random r = new Random();

            for (int i = 0; i < _inputNodeCount; i++)
            {
                var inputNode = new InputNode(i, r.NextDouble());
                _inputNodes.Add(inputNode);
            }
            
            for (int i = 0; i < _outPutNodeCount; i++)
            {
                var outputNode = new OutputNode(i, _activationFunction, _weightAdjustFunction);
                foreach(var input in InputNodes)
                {
                    outputNode.AddInputNode(input);
                }

                _outputNodes.Add(outputNode);
            }

            foreach(var input in _inputNodes)
            {
                foreach(var output in _outputNodes)
                {
                    output.AddInputNode(input);
                }
            }

            return network;
        }
        public IEnumerable<InputNode> InputNodes => _inputNodes;
        public IEnumerable<BaseOutputNode> OutputNodes => _outputNodes;
    }
}
