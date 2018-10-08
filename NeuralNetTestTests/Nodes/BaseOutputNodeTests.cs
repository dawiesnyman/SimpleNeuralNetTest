using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetTest.Functions;
using NeuralNetTest.Layers;
using NeuralNetTest.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Nodes.Tests
{
    [TestClass()]
    public class BaseOutputNodeTests
    {
        [TestMethod()]
        public void CalculateOutputStepTest()
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

            var inputNodes1 = new InputLayer()
            {
                new InputNode(0),// { InValue = 0 });
                new InputNode(1),// { InValue = 0 });
                new InputNode(2)// { InValue = 1 });
            };


            var outnode = new OutputNode(1, FunctionFactory.GetActivationFunction(eActivationFunc.Step), weightAdjust);
            outnode.AddInputNodes(inputNodes1);
            outnode.Bias = 0.0;

            for (int x = 0; x < 100000; x++)
            {

                inputNodes1.SetInput(new double[] { 0.0, 0.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(0);
                inputNodes1.SetInput(new double[] { 1.0, 1.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(1);
                inputNodes1.SetInput(new double[] { 1.0, 0.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(1);
                inputNodes1.SetInput(new double[] { 0.0, 1.0, 1.0 });
                outnode.CalculateOutput();
                outnode.AdjustWeights(0);
            }

            outnode.SetInput(new double[] { 1.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            var outp = outnode.Output;
            Assert.IsTrue(outp == 1);
            outnode.SetInput(new double[] { 0.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            outp = outnode.Output;

            Assert.IsTrue(outp == 0.0);
        }

        [TestMethod()]
        public void CalculateOutputSigmoidTest()
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
            inputNodes1.Add(new InputNode(0));// { InValue = 0 });
            inputNodes1.Add(new InputNode(1));// { InValue = 0 });
            inputNodes1.Add(new InputNode(2));// { InValue = 1 });

            var outnode = new OutputNode(1, FunctionFactory.GetActivationFunction(eActivationFunc.Sigmoid), weightAdjust);
            outnode.AddInputNodes(inputNodes1);
            outnode.Bias = -2.0;

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
            Assert.IsTrue(outp > 0.9);
            outnode.SetInput(new double[] { 0.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            outp = outnode.Output;

            Assert.IsTrue(outp < 0.9);
        }
        [TestMethod()]
        public void CalculateOutputSigmoidMultiLayerTest()
        {
            // 0 0 1 => 0
            // 1 1 1 => 1
            // 1 0 1 => 1
            // 0 1 1 => 0
            // test
            // 1 0 0 => ?
            Random r = new Random();
            Func<double> getRandom = (() => { return r.NextDouble(); });


            var weightAdjust = new Func<double, double, double, double>((i, o, e) =>
                {
                    return e * i * o * (1 - o);
                });
            var sigFunction = FunctionFactory.GetActivationFunction(eActivationFunc.Sigmoid);
            var step =  new Func<double, double>((z) =>
            {
                return z > 0.0 ? 1 : 0;
            });

            var inputNodes1 = new InputLayer();
            inputNodes1.Add(new InputNode(0));// { InValue = 0 });
            inputNodes1.Add(new InputNode(1));// { InValue = 0 });
            inputNodes1.Add(new InputNode(2));// { InValue = 1 });
            var bias = -2.0;
            var hiddenNodes = new List<HiddenNode>()
            {
                new HiddenNode(0, sigFunction, weightAdjust, getRandom) { Bias = bias },
                new HiddenNode(1, sigFunction, weightAdjust, getRandom){ Bias = bias },
                new HiddenNode(2, sigFunction, weightAdjust, getRandom){ Bias = bias }
            };

            foreach (var hidden in hiddenNodes)
            {
                hidden.AddInputNodes(inputNodes1);
            }

            var outnode = new OutputNode(0, step, weightAdjust, getRandom);
            outnode.AddInputNodes(hiddenNodes);
            //outnode.Bias = -2.0;

            Debug.WriteLine("Node {0} weight:{1}", 0, inputNodes1[0].Weights[0]);
            Debug.WriteLine("Node {0} weight:{1}", 1, inputNodes1[0].Weights[1]);

            Debug.WriteLine("Node {0} weight:{1}", 0, hiddenNodes[0].Weights[0]);
            Debug.WriteLine("Node {0} weight:{1}", 0, hiddenNodes[1].Weights[0]);


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

            Debug.WriteLine("Node {0} weight:{1}", 0, inputNodes1[0].Weights[0]);
            Debug.WriteLine("Node {0} weight:{1}", 1, inputNodes1[0].Weights[1]);


            Debug.WriteLine("Node {0} weight:{1}", 0, hiddenNodes[0].Weights[0]);
            Debug.WriteLine("Node {0} weight:{1}", 0, hiddenNodes[1].Weights[0]);

            outnode.SetInput(new double[] { 1.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            var outp = outnode.Output;
            Assert.IsTrue(outp == 1);
            outnode.SetInput(new double[] { 0.0, 0.0, 0.0 });
            outnode.CalculateOutput();

            outp = outnode.Output;

            Assert.IsTrue(outp == 0);
        }
    }
}