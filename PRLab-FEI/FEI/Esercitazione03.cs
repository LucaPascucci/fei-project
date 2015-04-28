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
        if (value < 0 || value > 49)
          throw new ArgumentOutOfRangeException("Inserire un valore fra 0 and 49.");
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
        Histogram histogram = new HistogramBuilder(InputImage).Execute();
        int pixelsToDiscard = (InputImage.PixelCount * StretchDiscardPercentage) / 100;
        int counterPixel = pixelsToDiscard;
        for (int destra = 0; counterPixel > 0 && destra < 256; destra++)
        {
            counterPixel -= histogram[destra];
            histogram[destra] = 0;
        }
        counterPixel = pixelsToDiscard;
        for (int sinistra = 255; counterPixel > 0 && sinistra >= 0; sinistra--)
        {
            counterPixel -= histogram[sinistra];
            histogram[sinistra] = 0;
        }
        int min = 0, max = 0;
        int last;
        for (int i = 0; min == 0 || max == 0; i++)
        {
            last = 255 - i;
            if (histogram[i] != 0 && min == 0)
            {
                min = i;
            }
            if (histogram[last] != 0 && max == 0)
            {
                max = last;
            }
        }
        int diff = max - min;
        if (diff > 0)
        {
            var op = new LookupTableTransform<byte>(InputImage,
            p => (255 * (p - min) / diff).ClipToByte());
            Result = op.Execute();
        }
        else
        {
            Result = InputImage.Clone();
        }


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
        Histogram histogram = new HistogramBuilder(InputImage).Execute();
        // Ricalcola ogni elemento dell'istogramma come somma dei precedenti
        for (int i = 1; i < 256; i++)
        {
            histogram[i] += histogram[i - 1];
        }
        // Definisce la funzione di mapping e applica la LUT
        var op = new LookupTableTransform<byte>(InputImage,p => (byte)(255 * histogram[p] / InputImage.PixelCount));
        Result = op.Execute();
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

    public ImageArithmetic(Image<byte> image1, Image<byte> image2, ImageArithmeticOperation operation) : base(image1, image2)
    {
      Operation = operation;
    }

    public ImageArithmetic(Image<byte> image1, Image<byte> image2)
      : this(image1, image2, defaultOperation)
    {
    }

    public override void Run()
    {
        Result = new Image<byte>(InputImage1.Width,InputImage1.Height);
        switch (Operation)
        {
            case ImageArithmeticOperation.Add:
                for (int i = 0; i < InputImage1.PixelCount; i++){
                    Result[i] = (InputImage1[i] + InputImage2[i]).ClipToByte();
                }
                break;

            case ImageArithmeticOperation.And:
                for (int i = 0; i < InputImage1.PixelCount; i++){
                    Result[i] = (InputImage1[i] & InputImage2[i]).ClipToByte();
                }
                
                break;

            case ImageArithmeticOperation.Average:
                for (int i = 0; i < InputImage1.PixelCount; i++){
                    Result[i] = ((InputImage1[i] + InputImage2[i])/2).ClipToByte();
                }
                break;

            case ImageArithmeticOperation.Darkest:
               for (int i = 0; i < InputImage1.PixelCount; i++){
                   if (InputImage1[i] > InputImage2[i])
                   {
                       Result[i] = InputImage2[i];
                   }
                   else
                   {
                       Result[i] = InputImage1[i];
                   }
                    
                }
                break;
                
            case ImageArithmeticOperation.Difference:
               for (int i = 0; i < InputImage1.PixelCount; i++){
                   Result[i] = (byte)Math.Abs(InputImage1[i] - InputImage2[i]);
                }
                break;

            case ImageArithmeticOperation.Lightest:
                for (int i = 0; i < InputImage1.PixelCount; i++)
                {
                if (InputImage1[i] > InputImage2[i])
                   {
                       Result[i] = InputImage1[i];
                   }
                   else
                   {
                       Result[i] = InputImage2[i];
                   }
                    
                }
                break;

            case ImageArithmeticOperation.Or:
                for (int i = 0; i < InputImage1.PixelCount; i++){
                    Result[i] = (InputImage1[i] | InputImage2[i]).ClipToByte();
                }
                break;

            case ImageArithmeticOperation.Subtract:
                for (int i = 0; i < InputImage1.PixelCount; i++){
                    Result[i] = (InputImage1[i] - InputImage2[i]).ClipToByte();
                }
                break;

            case ImageArithmeticOperation.Xor:
                for (int i = 0; i < InputImage1.PixelCount; i++){
                    Result[i] = (InputImage1[i] ^ InputImage2[i]).ClipToByte();
                }
                break;

        }
    }
  }

  [AlgorithmInfo("Operazione aritmetica RGB", Category = "FEI")]
  public class ImageArithmeticRGB : ImageOperation<RgbImage<byte>, RgbImage<byte>, RgbImage<byte>>
  {
      [AlgorithmParameter]
      [DefaultValue(defaultOperation)]
      public ImageArithmeticOperation Operation { get; set; }
      const ImageArithmeticOperation defaultOperation = ImageArithmeticOperation.Difference;

      public ImageArithmeticRGB()
      {
      }

      public ImageArithmeticRGB(RgbImage<byte> image1, RgbImage<byte> image2, ImageArithmeticOperation operation)
          : base(image1, image2)
      {
          Operation = operation;
      }

      public ImageArithmeticRGB(RgbImage<byte> image1, RgbImage<byte> image2)
          : this(image1, image2, defaultOperation)
      {
      }

      public override void Run()
      {
          Result = new RgbImage<byte>(InputImage1.Width, InputImage1.Height);

          Image<byte> ResultRed = new ImageArithmetic(InputImage1.RedChannel, InputImage2.RedChannel, Operation).Execute();
          Image<byte> ResultGreen = new ImageArithmetic(InputImage1.GreenChannel, InputImage2.GreenChannel, Operation).Execute();
          Image<byte> ResultBlue = new ImageArithmetic(InputImage1.BlueChannel, InputImage2.BlueChannel, Operation).Execute();

          for (int i = 0; i < Result.PixelCount; i++)
          {
              Result.RedChannel[i] = ResultRed[i];
              Result.GreenChannel[i] = ResultGreen[i];
              Result.BlueChannel[i] = ResultBlue[i];
          }

      }
  }
}
