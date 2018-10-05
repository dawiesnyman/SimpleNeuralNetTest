using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetTest.Activation;
using NeuralNetTest.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Nodes.Tests
{
    [TestClass()]
    public class BaseOutputNodeTests
    {
        private double doe(double input, double weight, double output, double error)
        {
            return 0.0;
        }
        [TestMethod()]
        public void CalculateOutputTest()
        {
            // 0 0 1 => 0
            // 1 1 1 => 1
            // 1 0 1 => 1
            // 0 1 1 => 0
            // test
            // 1 0 0 => ?
            var weightAdjust = new Func<double, double, double, double>((i, o, e) =>
            {
                return e * i * o * (1 - o);
            });

            var inputNodes1 = new List<InputNode>();
            inputNodes1.Add(new InputNode(0, 0.5));// { InValue = 0 });
            inputNodes1.Add(new InputNode(1, 0.5));// { InValue = 0 });
            inputNodes1.Add(new InputNode(2, 0.5));// { InValue = 1 });

            var outnode = new OutputNode(1, ActivationFunctionFactory.GetFunction(eActivationFunc.Step), weightAdjust);
            outnode.AddInputNodes(inputNodes1);
            outnode.Bias = 0.0;// -2.0;

            for (int x = 0; x < 100000; x++)
            {
                outnode.SetInput(new double[] { 0.0, 0.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(0);
                outnode.SetInput(new double[] { 1.0, 1.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(1);
                outnode.SetInput(new double[] { 1.0, 0.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(1);
                outnode.SetInput(new double[] { 0.0, 1.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(0);
            }

            outnode.SetInput(new double[] { 1.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            var outp = outnode.Output;

            outnode.SetInput(new double[] { 0.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            outp = outnode.Output;

            Assert.Fail();
        }
    }
}