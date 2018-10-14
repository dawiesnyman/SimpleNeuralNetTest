using Anna.Functions;
using Anna.Layers;
using Anna.Nodes;
using NeuralNet.Models;
using System;
using System.Collections.Generic;

namespace Anna
{
    public class Network
    {

        private readonly int _inputNodeInitialCount;
        private readonly int _hiddenNodePerLayerInitialCount;
        private readonly int _hiddenLayerInitialCount = 1;
        private readonly int _outPutNodeInitialCount;

        private readonly Func<double, double> _activationFunction;
        private readonly Func<TrainingCalcModel, double> _weightAdjustFunction;
        private readonly Random _random = new Random();

        private Func<double> _getRandom;
        private InputLayer _inputNodes;
        private List<HiddenNode>[] _hiddenNodes;
        private OutputLayer _outputNodes;

        private List<Input> _inputs;

        public Network(int inputNodeCount, int outPutNodeCount, int hiddenLayerCount = 1, int hiddenNodesPerLayerCount = -1)
        {
            _inputNodeInitialCount = inputNodeCount;

            _hiddenNodePerLayerInitialCount = hiddenNodesPerLayerCount == -1 ? inputNodeCount : hiddenNodesPerLayerCount;
            _outPutNodeInitialCount = outPutNodeCount;
            _activationFunction = FunctionFactory.GetActivationFunction(eActivationFunc.Sigmoid);
            _weightAdjustFunction = FunctionFactory.GetWeightAdjustFunction(eWeightAdjustment.Simple);
            _getRandom = (() =>
            {
                return _random.NextDouble();
            });

            _inputNodes = new InputLayer();
            _hiddenNodes = new List<HiddenNode>[_hiddenLayerInitialCount];
            _outputNodes = new OutputLayer();
        }

        public Network(int inputNodeCount, int hiddenNodesPerLayerCount, int hiddenLayerCount, int outPutNodeCount,
            Func<double, double> activationFunction, Func<TrainingCalcModel, double> weightAdjustmentFunction)
            : this(inputNodeCount, outPutNodeCount)
        {
            _activationFunction = activationFunction;
            _weightAdjustFunction = weightAdjustmentFunction;
        }

        /// <summary>
        /// Used to create a custom payout for the network by creating and connecting nodes 
        /// and passing them to the network
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hidden"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public Network SetNetwork(InputLayer input, List<HiddenNode>[] hidden, OutputLayer output)
        {
            _inputNodes = input;
            _hiddenNodes = hidden;
            _outputNodes = output;

            return this;
        }
        public Network BuildNetwork()
        {
            for (int i = 0; i < _inputNodeInitialCount; i++)
            {
                var inputNode = new InputNode(i);
                _inputNodes.Add(inputNode);
            }

            for (int i = 0; i < _outPutNodeInitialCount; i++)
            {
                var outputNode = new OutputNode(i, _activationFunction, _weightAdjustFunction);
                _outputNodes.Add(outputNode);
            }

            for (int i = 0; i < _hiddenLayerInitialCount; i++)
            {
                _hiddenNodes[i] = new List<HiddenNode>();

                for (int j = 0; j < _hiddenNodePerLayerInitialCount; j++)
                {
                    _hiddenNodes[i].Add(new HiddenNode(j, _activationFunction, _weightAdjustFunction, _getRandom));
                }

                if (i == 0)
                {
                    _hiddenNodes[i].ForEach((o) =>
                    {
                        o.AddInputNodes(_inputNodes);
                    });
                }
                else
                {
                    _hiddenNodes[i].ForEach((o) =>
                    {
                        o.AddInputNodes(_hiddenNodes[i - 1]);
                    });
                }
            }

            _outputNodes.ForEach((o) =>
            {
                if (_hiddenLayerInitialCount == 0 || _hiddenNodePerLayerInitialCount == 0)
                {
                    o.AddInputNodes(_inputNodes);
                }
                else
                {
                    o.AddInputNodes(_hiddenNodes[_hiddenLayerInitialCount - 1]);
                }
            });

            return this;
        }

        public Network SetBias(double bias)
        {
            for (int i = 0; i < _hiddenNodes.Length; i++)
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

                    int outPutIndex = 0;

                    _outputNodes.ForEach((o) =>
                    {
                        for (int y = 0; y < _hiddenNodes.Length; y++)
                        {
                            _hiddenNodes[y].ForEach((h) =>
                            {
                                h.CalculateOutput();
                                h.AdjustWeights(_outputNodes.GetExpectedOutput(outPutIndex));
                            });
                        }

                        o.CalculateOutput();
                        o.AdjustWeights(_outputNodes.GetExpectedOutput(outPutIndex++));
                    });

                    Console.Write("Input: {0}\n", i.ID);
                });
                Console.Write("Cycle: {0}", x);
            }

            return this;
        }
        public void Calculate(Input input)
        {
            _inputNodes.SetInput(input.InputValues);

            for (int i = 0; i < _hiddenNodes.Length; i++)
            {
                _hiddenNodes[i].ForEach((h) =>
                {
                    h.CalculateOutput();
                });
            }
            _outputNodes.ForEach((o) =>
            {
                //o.SetInput(input);
                o.CalculateOutput();
            });
        }
        public IEnumerable<IInputNode> InputNodes => _inputNodes;
        public IEnumerable<BaseOutputNode> OutputNodes => _outputNodes;

        public IEnumerable<Input> InputValues => _inputs;
    }
}
