using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioLab.Common;
using BioLab.ImageProcessing;
using System.Drawing;
using BioLab.GUI.Forms;
using System.Windows.Forms;
using BioLab.ImageProcessing.Topology;
using BioLab.DataStructures;
using System.ComponentModel;

namespace PRLab.FEI
{

    [AlgorithmInfo("Calcolo Istogramma", Category = "FEI")]
    public class HistogramBuilder : ImageOperation<Image<byte>, Histogram>
    {
      [AlgorithmParameter]
      [DefaultValue(false)]
      public bool Sqrt { get; set; }

      [AlgorithmParameter]
      [DefaultValue(0)]
      public int SmoothWindowSize { get; set; }

      public HistogramBuilder(Image<byte> inputImage)
      {
        InputImage = inputImage;
      }

      public HistogramBuilder(Image<byte> inputImage, bool sqrt, int smoothWindowSize)
      {
        InputImage = inputImage;
        Sqrt = sqrt;
        SmoothWindowSize = smoothWindowSize;
      }

      public override void Run()
      {
        throw new NotImplementedException();
      }
    }
    
}
