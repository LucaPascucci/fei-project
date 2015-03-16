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

  //TODO: public class CalcolaModuloGradiente ...


  [AlgorithmInfo("Trasformata distanza", Category = "FEI")]
  public class TrasformataDistanza : TopologyOperation<Image<int>>
  {
    public TrasformataDistanza()
    {
    }

    public TrasformataDistanza(Image<byte> inputImage, byte foreground, MetricType metric)
      : base(inputImage, foreground)
    {
      Metric = metric;
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }

    [AlgorithmParameter]
    public MetricType Metric { get; set; }
  }


}
