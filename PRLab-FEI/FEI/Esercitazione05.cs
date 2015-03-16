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
using BioLab.GUI.UserControls;

namespace PRLab.FEI
{

  [AlgorithmInfo("Convoluzione (da immagine byte a immagine int)", Category = "FEI")]
  [CustomAlgorithmPreviewParameterControl(typeof(SimpleConvolutionParameterControl))]
  public class ConvoluzioneByteInt : Convolution<byte, int, ConvolutionFilter<int>>
  {
    public override void Run()
    {
      throw new NotImplementedException();
    }
  }


}
