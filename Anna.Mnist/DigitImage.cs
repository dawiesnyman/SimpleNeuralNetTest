using Anna.Layers;

namespace Anna.Mnist
{
    public class DigitImage
    {
        public byte[][] pixels;
        public byte label;

        public DigitImage(byte[][] pixels,
          byte label)
        {
            this.pixels = new byte[28][];
            for (int i = 0; i < this.pixels.Length; ++i)
                this.pixels[i] = new byte[28];

            for (int i = 0; i < 28; ++i)
                for (int j = 0; j < 28; ++j)
                    this.pixels[i][j] = pixels[i][j];

            this.label = label;
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    if (this.pixels[i][j] == 0)
                        s += " "; // white
                    else if (this.pixels[i][j] == 255)
                        s += "O"; // black
                    else
                        s += "."; // gray
                }
                s += "\n";
            }
            s += this.label.ToString();
            return s;
        } // ToString
        public Input AsInput(int id)
        {
            var inputValues = ToDoubleArray();
            var ouputValues = GetOutputArray();
            return new Input() { ID = id,  InputValues = inputValues, OutputValues = ouputValues };
        }
        public double[] GetOutputArray()
        {
            var outvalues = new double[10];

            for (int i = 0; i < 10; i++)
            {

                outvalues[i] = label == i ? 1 : 0;
            }

            return outvalues;
        }

        private double[] ToDoubleArray()
        {
            var pic = new double[784];
            int x = 0;
            for (int i = 0; i < 28; ++i)
            {
                for (int j = 0; j < 28; ++j)
                {
                    pic[x++] = pixels[i][j];
                }
            }
            return pic;
        }
    }
}
