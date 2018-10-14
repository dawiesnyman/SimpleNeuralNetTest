using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.Models
{
    public class TrainingCalcModel
    {
        public double Weight { get; set; }
        public double Input { get; set; }
        public double ActualOutput { get; set; }
        public double ExpectedOutput { get; set; }
        public double Error { get; set; }
        public double TrainingRate { get; set; }
    }
}
