using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anna.Iris
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"C:\Users\dxsnym\Desktop\IrisData\iris.data";

            var datalines = File.ReadAllLines(filename).ToList();

            var models = new List<IrisModel>();
            int count = 30; 
            datalines.ForEach((i) =>
            {
                if (!string.IsNullOrEmpty(i))
                {
                    models.Add(new IrisModel(i));
                }
            });

            var network = new Network(4, 3, 0, 0);
            int id = 0;
            network.BuildNetwork()
                .SetBias(4)
                .SetInputs(models.Select((s) => s.AsInput(id++)))
                .Train(5000);

            var model = new IrisModel("6.3,2.5,5.0,1.9,Iris-virginica");

            network.Calculate(model.AsInput(id++));
            //Console.Write("Value: {0}, \n", network.OutputNodes.ToList()[0].Output);
            Console.Write("Value: {0}, {1}, {2} Expected: {3}\n", network.OutputNodes.ToList()[0].Output,
                network.OutputNodes.ToList()[1].Output,
                network.OutputNodes.ToList()[2].Output, model.ClassID);
            model = new IrisModel("6.1,2.9,4.7,1.4,Iris-versicolor");

            network.Calculate(model.AsInput(id++));
            //Console.Write("Value: {0}, \n", network.OutputNodes.ToList()[0].Output);
            Console.Write("Value: {0}, {1}, {2} Expected: {3}\n", network.OutputNodes.ToList()[0].Output,
                network.OutputNodes.ToList()[1].Output,
                network.OutputNodes.ToList()[2].Output, model.ClassID);

            model = new IrisModel("5.1,3.5,1.4,0.3,Iris-setosa");

            network.Calculate(model.AsInput(id++));
            //Console.Write("Value: {0}, \n", network.OutputNodes.ToList()[0].Output);
            Console.Write("Value: {0}, {1}, {2} Expected: {3}\n", network.OutputNodes.ToList()[0].Output,
                network.OutputNodes.ToList()[1].Output,
                network.OutputNodes.ToList()[2].Output, model.ClassID);


            Console.ReadLine();
        }
    }
}
