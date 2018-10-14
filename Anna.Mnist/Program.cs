using Anna.Layers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anna.Mnist
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var startTime = DateTime.Now;

                List<DigitImage> images = new List<DigitImage>();
                Console.WriteLine("\nBegin\n");
                var labelPath = @"C:\Users\dxsnym\Desktop\ml images\t10k-labels.idx1-ubyte";
                var imagePath = @"C:\Users\dxsnym\Desktop\ml images\t10k-images.idx3-ubyte";

                images = new List<DigitImage>(GetImages(labelPath, imagePath));
                var inputs = new List<Input>();
                int id = 0;
                images.Where((i1)=> i1.label== 1 || i1.label == 2 )
                    .ToList().ForEach((i) =>
                 {
                     //Console.WriteLine(i.ToString());
                     //Console.ReadLine();

                     inputs.Add(i.AsInput(id++));
                 });// each image

                var network = new Network(784, 10);
                
                network.BuildNetwork()
                    .SetBias(0)
                    .SetInputs(inputs)
                    .Train(1);

                var endTime = DateTime.Now;

                Console.WriteLine("Total time to train {0}:{1}:{2}", (endTime - startTime).Minutes, (endTime - startTime).Seconds, (endTime - startTime).Milliseconds);
                Console.ReadLine();

                var testimages = @"C:\Users\dxsnym\Desktop\ml images\train-labels.idx1-ubyte";
                var testLabels = @"C:\Users\dxsnym\Desktop\ml images\train-images.idx3-ubyte";
                images = new List<DigitImage>(GetImages(testLabels, testimages));
                var testinputs = new List<Input>();
                int id1 = id++;
                images.Where((i1) => i1.label == 1 || i1.label == 2 )
                    .ToList().ForEach((i) =>
                    {
                        //Console.WriteLine(i.ToString());
                        //Console.ReadLine();

                        network.Calculate(i.AsInput(id1++));

                        
                    });// each image



                Console.WriteLine("Total time to train {0}:{1}:{2}", (endTime - startTime).Minutes, (endTime - startTime).Seconds, (endTime - startTime).Milliseconds);
                Console.WriteLine("\nEnd\n");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        } // Main

        public static IEnumerable<DigitImage> GetImages(string labelPath, string imagePath)
        {
            var images = new List<DigitImage>();
            FileStream ifsLabels =
                new FileStream(labelPath,
                FileMode.Open); // test labels
            FileStream ifsImages =
             new FileStream(imagePath,
             FileMode.Open); // test images

            BinaryReader brLabels =
             new BinaryReader(ifsLabels);
            BinaryReader brImages =
             new BinaryReader(ifsImages);

            int magic1 = brImages.ReadInt32(); // discard
            int numImages = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            // each test image
            for (int di = 0; di < 10000; ++di)
            {
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        try
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }

                byte lbl = brLabels.ReadByte();

                DigitImage dImage =
                  new DigitImage(pixels, lbl);

                images.Add(dImage);
            } // each image

            ifsImages.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();

            return images;
        }
        public IEnumerable<Input> GetTrainingInput(IEnumerable<DigitImage> images)
        {
            int id = 0;
            var inputs = new List<Input>();
            images.ToList().ForEach((i) =>
            {
                inputs.Add(i.AsInput(id++));
            });
            return inputs;
        }
        //public Input GetTrainingInput(DigitImage image)
        //{
        //    var inputValues = ToDoubleArray(image.pixels);
        //    var ouputValues = GetOutputArray(image.label);
        //    return new Input() { ID = 0, InputValues = inputValues, OutputValues = ouputValues };
        //}

    } // Program

    
}
