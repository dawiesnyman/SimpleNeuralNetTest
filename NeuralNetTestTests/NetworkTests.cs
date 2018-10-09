using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetTest;
using NeuralNetTest.Layers;
using NeuralNetTest.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest.Tests
{
    [TestClass()]
    public class NetworkTests
    {
        [TestMethod()]
        public void BuildNetworkTest()
        {
            var network = new Network(3, 1, 0);

            network.BuildNetwork();


            Assert.Fail();
        }

        [TestMethod()]
        public void TrainTest()
        {
            var input = new List<Input>()
            {
                new Input(){ ID = 0, InputValues = new double[] { 0.0, 0.0, 1.0 }, OutputValues = new double[] { 0.0 } },
                new Input(){ ID = 0, InputValues = new double[] { 1.0, 0.0, 1.0 }, OutputValues = new double[] { 1.0 } },
                new Input(){ ID = 0, InputValues = new double[] { 0.0, 1.0, 1.0 }, OutputValues = new double[] { 0.0 } }
            };

            var network = new Network(3, 1, 1, 3)
                .SetBias(-2)
                .SetInputs(input);

            Assert.Fail();
        }

        [TestMethod()]
        public void CalculateOutputSigmoidTest()
        {
            var network = new Network(3, 1);
            var inputs = new List<Input>()
            {
                new Input(){ ID = 0, InputValues = new double[] { 0.0, 0.0, 1.0 }, OutputValues = new double[] { 0.0 } },
                new Input(){ ID = 1, InputValues = new double[] { 1.0, 0.0, 1.0 }, OutputValues = new double[] { 1.0 } },
                new Input(){ ID = 2, InputValues = new double[] { 0.0, 1.0, 1.0 }, OutputValues = new double[] { 0.0 } }
            };


            network.BuildNetwork()
                .SetBias(-2.0)
                .SetInputs(inputs)
                .Train(100000);

            network.Calculate(new Input() { ID = 9, InputValues = new double[] { 0.0, 0.0, 0.0 }, OutputValues = new double[] { 0.0 } });

            Assert.IsTrue(network.OutputNodes.First().Output < 0.2);


            network.Calculate(new Input() { ID = 10, InputValues = new double[] { 1.0, 1.0, 1.0 }, OutputValues = new double[] { 1.0 } });

            Assert.IsTrue(network.OutputNodes.First().Output > 0.8);
        }

        [TestMethod()]
        public void SetBiasTest()
        {
            var network = new Network(2, 2, 2, 2);

            var inputs = new InputLayer()
            {
                new InputNode(0),
                new InputNode(1)
            };

            var hiddens = new List<HiddenNode>[]
            {
                new List<HiddenNode>() { new HiddenNode(0, null, null), new HiddenNode(1, null, null)},
                new List<HiddenNode>() { new HiddenNode(0, null, null), new HiddenNode(1, null, null)}
            };

            var outLayer = new OutputLayer()
            {
                new OutputNode(0, null, null),
                new OutputNode(1, null, null)
            };

            network.SetNetwork(inputs, hiddens, outLayer)
                .SetBias(2.2);

        
            for (int i = 0; i < hiddens.Count(); i++)
            {
                hiddens[i].ForEach((h) =>
                {
                    Assert.AreEqual(2.2, h.Bias);
                });
            }

            outLayer.ForEach((o) =>
            {
                Assert.AreEqual(2.2, o.Bias);
            });
        }

        [TestMethod()]
        public void SplitTest()
        {
            Assert.Fail();
        }
    }
}