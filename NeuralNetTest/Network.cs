using NeuralNetTest.Functions;
using NeuralNetTest.Layers;
using NeuralNetTest.Nodes;
using System;
using System.Collections.Generic;

namespace NeuralNetTest
{
    public class Network
    {
        private readonly int _inputNodeCount;
        private readonly int _hiddenNodePerLayerCount;
        private readonly int _hiddenLayerCount = 1;
        private readonly int _outPutNodeCount;
        private readonly Func<double, double> _activationFunction;
        private readonly Func<double, double, double, double> _weightAdjustFunction;
        private readonly Random _random = new Random();

        private Func<double> _getRandom;
        private InputLayer _inputNodes;
        private List<HiddenNode>[] _hiddenNodes;
        private OutputLayer _outputNodes;

        private List<Input> _inputs;
        private Dictionary<int, double[]> _outputs;

        public Network(int inputNodeCount, int outPutNodeCount, int hiddenLayerCount = 1, int hiddenNodesPerLayerCount = -1)
        {
            _inputNodeCount = inputNodeCount;

            _hiddenNodePerLayerCount = hiddenNodesPerLayerCount == -1 ? inputNodeCount : hiddenNodesPerLayerCount;
            _outPutNodeCount = outPutNodeCount;
            _activationFunction = FunctionFactory.GetActivationFunction(eActivationFunc.Sigmoid);
            _weightAdjustFunction = FunctionFactory.GetWeightAdjustFunction(eWeightAdjustment.Simple);
            _getRandom = (() =>
            {
                return _random.NextDouble();
            });

            _inputNodes = new InputLayer();
            _hiddenNodes = new List<HiddenNode>[_hiddenLayerCount];            
            _outputNodes = new OutputLayer();
        }

        public Network(int inputNodeCount, int hiddenNodesPerLayerCount, int hiddenLayerCount, int outPutNodeCount,
            Func<double, double> activationFunction, Func<double, double, double, double> weightAdjustmentFunction)
            : this(inputNodeCount, outPutNodeCount)
        {
            _activationFunction = activationFunction;
            _weightAdjustFunction = weightAdjustmentFunction;
        }

        public Network BuildNetwork()
        {
            for (int i = 0; i < _inputNodeCount; i++)
            {
                var inputNode = new InputNode(i);
                _inputNodes.Add(inputNode);
            }

            for (int i = 0; i < _outPutNodeCount; i++)
            {
                var outputNode = new OutputNode(i, _activationFunction, _weightAdjustFunction);
                _outputNodes.Add(outputNode);
            }

            for (int i = 0; i < _hiddenLayerCount; i++)
            {
                _hiddenNodes[i] = new List<HiddenNode>();

                for (int j = 0; j < _hiddenNodePerLayerCount; j++)
                {
                    _hiddenNodes[i].Add(new HiddenNode(j, _activationFunction, _weightAdjustFunction, _getRandom));
                }

                if(i > 0)
                {
                    _hiddenNodes[i].ForEach((o) =>
                    {
                        o.AddInputNodes(_hiddenNodes[i - 1]);
                    });
                }
            }

            _outputNodes.ForEach((o) =>
            {
                //if hiddenlayercount is 0 or hiddenNode count is 0 connect input to output layer
                if (_hiddenLayerCount == 0 || _hiddenNodePerLayerCount == 0)
                {
                    o.AddInputNodes(_inputNodes);
                }
                else
                {
                    o.AddInputNodes(_hiddenNodes[_hiddenLayerCount - 1]);
                }
            });



            return this;
        }

        public Network SetBias(double bias)
        {
            for(int i =0; i < _hiddenLayerCount; i++)
            {
                _hiddenNodes[i].ForEach((h) =>
                {
                    h.Bias = bias;
                });
            }

            _outputNodes.ForEach((o) =>
            {
                o.Bias = bias;
            });

            return this;
        }
        public Network SetInputs(List<Input> input)
        {
            _inputs = input;

            return this;
        }
        public Network Split(int trainPercentage)
        {
            return this;
        }
        public Network Train(int cycles)
        {
            for (int x = 0; x < cycles; x++)
            {
                _inputs.ForEach((i) =>
                {
                    _inputNodes.SetInput(_inputs[i.ID].InputValues);
                    _outputNodes.SetExpectedOutput(_inputs[i.ID].OutputValues);

                    int index = 0;

                    _outputNodes.ForEach((o) =>
                    {
                        o.CalculateOutput();
                        o.AdjustWeights(_outputNodes.GetExpectedOutput(index++));
                    });

                });
            }

            return this;
        }
        public IEnumerable<IInputNode> InputNodes => _inputNodes;
        public IEnumerable<BaseOutputNode> OutputNodes => _outputNodes;
    }
}
