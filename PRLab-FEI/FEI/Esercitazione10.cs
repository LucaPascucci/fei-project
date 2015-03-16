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
using BioLab.PatternRecognition.Localization;
using System.Diagnostics;
namespace PRLab.FEI
{

  [AlgorithmInfo("Correlazione", Category = "FEI")]
  [CustomAlgorithmPreviewOutput(typeof(OuputViewerCorrelazione))]
  public class Correlazione : ImageOperation<Image<byte>, Image<int>>
  {
    
    [AlgorithmInput]
    public Image<byte> Template { get; set; }

    [AlgorithmParameter]
    [DefaultValue(CorrelationMeasure.Zncc)]
    public CorrelationMeasure CorrelationMeasure { get; set; }

    public Correlazione(Image<byte> inputImage, Image<byte> template, CorrelationMeasure correlationMeasure)
      : base(inputImage)
    {
      this.Template = template;
      CorrelationMeasure = correlationMeasure;
    }

    public Correlazione()
    {
      CorrelationMeasure = CorrelationMeasure.Zncc;
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }
}
