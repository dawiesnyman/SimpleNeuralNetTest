using Anna.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anna.Iris
{
    class IrisModel
    {
        public IrisModel()
        { }
        public IrisModel(string dataLine)
        {
            var items = dataLine.Split(',');

            SepLength = double.Parse(items[0]);
            SepWidth  = double.Parse(items[1]);
            PetLength = double.Parse(items[2]);
            PetWidth  = double.Parse(items[3]);
            ClassName = items[4];
        }
        public double SepLength { get; set; }
        public double SepWidth { get; set; }
        public double PetLength { get; set; }
        public double PetWidth { get; set; }
        public string ClassName { get; set; }
        public double ClassID
        {
            get
            {
                switch (ClassName)
                {
                    case "Iris-setosa":
                        return 0;
                    case "Iris-versicolor":
                        return 1;
                    case "Iris-virginica":
                        return 2;
                    default:
                        return -1;
                }
            }
        }
        public Input AsInput(int id)
        {
            var input = new Input() { ID = id };
            var output = new double[3];

            switch(ClassName)
            {
                case "Iris-setosa":
                    output = new double[] { 1.0, 0.0, 0.0 };
                    break;
                case "Iris-versicolor":
                    output = new double[] { 0.0, 1.0, 0.0 };
                    break;
                case "Iris-virginica":
                    output = new double[] { 0.0, 0.0, 1.0 };
                    break;
                default:
                    output = null;
                    break;
            }
            input.InputValues = new double[] { SepLength, SepWidth, PetLength, PetWidth };
            input.OutputValues = output;

            return input;
        }
    }
}
