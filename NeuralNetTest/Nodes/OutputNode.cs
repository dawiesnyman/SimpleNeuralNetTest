﻿using System;

namespace NeuralNetTest.Nodes
{
    public class OutputNode : BaseOutputNode
    {
        public OutputNode(Address address, Func<double, double> activationFunction, 
            Func<double, double, double, double> weightAdjustmentFunction,
            Func<double> getRandom = null) 
            : base(address, activationFunction, weightAdjustmentFunction, getRandom)
        {

        }
    }
}
