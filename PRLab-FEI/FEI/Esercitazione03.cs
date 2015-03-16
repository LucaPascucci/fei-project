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

  [AlgorithmInfo("Stretch del contrasto", Category = "FEI")]
  public class ContrastStretch : ImageOperation<Image<byte>, Image<byte>>
  {
    private int stretchDiscardPerc;

    [AlgorithmParameter]
    [DefaultValue(0)]
    public int StretchDiscardPercentage
    {
      get { return stretchDiscardPerc; }
      set
      {
        if (value < 0 || value > 100)
          throw new ArgumentOutOfRangeException("Inserire un valore fra 0 and 100.");
        stretchDiscardPerc = value;
      }
    }

    public ContrastStretch()
    {
    }

    public ContrastStretch(Image<byte> inputImage)
      : base(inputImage)
    {
      StretchDiscardPercentage = 0;
    }

    public ContrastStretch(Image<byte> inputImage, int stretchDiscard)
      : base(inputImage)
    {
      StretchDiscardPercentage = stretchDiscard;
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }


  [AlgorithmInfo("Equalizzazione istogramma", Category = "FEI")]
  public class HistogramEqualization : ImageOperation<Image<byte>, Image<byte>>
  {
    public HistogramEqualization()
    {
    }

    public HistogramEqualization(Image<byte> inputImage)
      : base(inputImage)
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }

  [AlgorithmInfo("Operazione aritmetica", Category = "FEI")]
  public class ImageArithmetic : ImageOperation<Image<byte>, Image<byte>, Image<byte>>
  {
    [AlgorithmParameter]
    [DefaultValue(defaultOperation)]
    public ImageArithmeticOperation Operation { get; set; }
    const ImageArithmeticOperation defaultOperation = ImageArithmeticOperation.Difference;

    public ImageArithmetic()
    {
    }

    public ImageArithmetic(Image<byte> image1, Image<byte> image2, ImageArithmeticOperation operation)
      : base(image1, image2)
    {
      Operation = operation;
    }

    public ImageArithmetic(Image<byte> image1, Image<byte> image2)
      : this(image1, image2, defaultOperation)
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }
}
