using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetTest;
using NeuralNetTest.Layers;
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
        public void TrainTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CalculateOutputSigmoidTest()
        {
            var network = new Network(3, 1);
            var inputs = new List<Input>()
            {
                new Input() { ID = 0, InputValues = { }, OutputValues = { } },
                new Input() { ID = 1, InputValues = { }, OutputValues = { } },
                new Input() { ID = 2, InputValues = { }, OutputValues = { } },
                new Input() { ID = 3, InputValues = { }, OutputValues = { } }
            };

            network.BuildNetwork()
                .SetBias(2.0)
                .SetInputs(inputs)
                .Train(100000);


        }

        [TestMethod()]
        public void BuildNetworkTest()
        {
            var network = new Network(3, 1, 0);

            network.BuildNetwork();


            Assert.Fail();
        }

        [TestMethod()]
        public void SetBiasTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetInputsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SplitTest()
        {
            Assert.Fail();
        }
    }
}