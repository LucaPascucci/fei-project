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

  [AlgorithmInfo("Morfologia: Dilatazione", Category = "FEI")]
  public class Dilatazione : MorphologyOperation
  {
    public Dilatazione(Image<byte> inputImage, Image<byte> structuringElement, byte foreground)
      : base(inputImage, structuringElement, foreground)
    {
    }

    public Dilatazione(Image<byte> inputImage, Image<byte> structuringElement)
      : base(inputImage, structuringElement)
    {
    }

    public Dilatazione()
    {
    }
    public override void Run()
    {
      throw new NotImplementedException();
    }
  }

  [AlgorithmInfo("Morfologia: Erosione", Category = "FEI")]
  public class Erosione : MorphologyOperation
  {
    public Erosione(Image<byte> inputImage, Image<byte> structuringElement, byte foreground)
      : base(inputImage, structuringElement, foreground)
    {
    }

    public Erosione(Image<byte> inputImage, Image<byte> structuringElement)
      : base(inputImage, structuringElement)
    {
    }

    public Erosione()
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }

  [AlgorithmInfo("Morfologia: Apertura", Category = "FEI")]
  public class Apertura : MorphologyOperation
  {
    public Apertura(Image<byte> inputImage, Image<byte> structuringElement, byte foreground)
      : base(inputImage, structuringElement, foreground)
    {
    }

    public Apertura(Image<byte> inputImage, Image<byte> structuringElement)
      : base(inputImage, structuringElement)
    {
    }

    public Apertura()
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }

  [AlgorithmInfo("Morfologia: Chiusura", Category = "FEI")]
  public class Chiusura : MorphologyOperation
  {
    public Chiusura(Image<byte> inputImage, Image<byte> structuringElement, byte foreground)
      : base(inputImage, structuringElement, foreground)
    {
    }

    public Chiusura(Image<byte> inputImage, Image<byte> structuringElement)
      : base(inputImage, structuringElement)
    {
    }

    public Chiusura()
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }  
}
