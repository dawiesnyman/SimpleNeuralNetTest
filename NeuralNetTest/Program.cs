using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sigmoidActivationFunction = new Func<double, double>((z) =>
            {
                if (z < -20.0) return 0.0;
                else if (z > 20.0) return 1.0;
                else return 1.0 / (1.0 + Math.Exp(-z));
            });

            var stepActivationFunction = new Func<double, double>((z) =>
            {
                return  z > 0 ? 1 : 0;
            });

            var waFunction = new Func<double, double, double, double>((i, o, e) =>
            {
                return e * i * o * (1 - o);
            });

            var factory = new Network(4, 0, 2, stepActivationFunction, waFunction);

            factory.BuildNetwork();

            //set input


            Console.ReadKey();


        }
    }
}
