﻿using NeuralNet.Models;
using System;
using System.Collections.Generic;

namespace Anna.Nodes
{
    public abstract class BaseOutputNode : BaseNode
    {
        private readonly Random _random = new Random();
        private readonly List<IInputNode> _inputNodes;
        private readonly List<BaseOutputNode> _outNodes;
        private readonly Func<double, double> _activationFunction;
        private Func<TrainingCalcModel, double> _weightAdjustmentFunction;
        private Func<double> _getRandom;

        public BaseOutputNode(int address, Func<double, double> activationFunction, 
            Func<TrainingCalcModel, double> weightAdjustmentFunction,
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
                //var o = i as OutputNode;

                //if (o != null)
                //{
                //    o.CalculateOutput();
                //}
                if(i.Input != 0.0) 
                    total += (i.Weights[Address] * i.Input);
            }
            total += Bias;

            if(total != 0.0)
                Output = _activationFunction(total);
        }
        public void AdjustWeights(double expected)
        {
            double error = expected - Output;
            _inputNodes.ForEach((i) =>
            {
                i.Weights[Address] += _weightAdjustmentFunction(new TrainingCalcModel() { Input = i.Input, ActualOutput = Output, Error = error, Weight = i.Weights[Address] });


                var o = i as BaseOutputNode;

                if (o != null)
                {
                    o.AdjustWeights(expected);
                }
            });
        }
        public IEnumerable<IInputNode> InputNodes => _inputNodes;
        public double Bias { get; set; }
        public double Output { get; protected set; }
        public void SetInput(double[] input)
        {
            if(input.Length != _inputNodes.Count)
            {
                throw new Exception("Input array lengte does not equal the input nodes");
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
            new List<IInputNode>(inputnodes).ForEach((i) =>
            {               
                i.Weights.Add(Address, _getRandom());
            });

            _inputNodes.AddRange(inputnodes);
        }
    }
}
