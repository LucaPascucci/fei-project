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
          Result = new Histogram();
          foreach (byte b in InputImage)
          {
              Result[b]++;
          }
          if (Sqrt)
          {
              for (int i = 0; i <= 255; i++){
                  Result[i] = (int)Math.Round(Math.Sqrt(Result[i]));
              }
          }
          if (SmoothWindowSize > 0)
          {
              for (int i = 0; i <= 255; i++){
                  int elemSum = 0;
                  int start = i - SmoothWindowSize;
                  int end = i + SmoothWindowSize;
                  if (start < 0)
                  {
                      start = 0;
                  }
                  if (end > 255)
                  {
                      end = 255;
                  }
                  for (int neighbour = start; neighbour <= end; neighbour++)
                  {
                      elemSum += Result[neighbour];
                  }
                  Result[i] = elemSum / (end - start + 1);
                 
              }
          }
      }
    }

    [AlgorithmInfo("Image Binarization", Category = "FEI")]
    public class MyImageBinarization : ImageOperation<Image<byte>, Image<byte>>
    {
        [AlgorithmParameter]
        [DefaultValue(0)]
        public int Treeshold { get; set; }

        public override void Run()
        {
            Result = new Image<byte>(InputImage.Width, InputImage.Height);
            for (int i = 0; i < Result.PixelCount; i++)
            {
                if (InputImage[i] <= Treeshold)
                {
                    Result[i] = 0;
                }
                else
                {
                    Result[i] = 255;
                }
            }
        }
    }

    [AlgorithmInfo("RGB to Channels Grayscale", Category = "FEI")]
    public class RGBtoChannelsGrayscale : Algorithm
    {
        [AlgorithmInput]
        public RgbImage<byte> InputImage { get; set; }

        [AlgorithmOutput]
        public Image<byte> RedChannelGrayscale { get; set; }

        [AlgorithmOutput]
        public Image<byte> BlueChannelGrayscale { get; set; }

        [AlgorithmOutput]
        public Image<byte> GreenChannelGrayscale { get; set; }

        public RGBtoChannelsGrayscale(RgbImage<byte> inputImage)
        {
            InputImage = inputImage;
        }

        public override void Run() {
            RedChannelGrayscale = InputImage.RedChannel;
            BlueChannelGrayscale = InputImage.BlueChannel;
            GreenChannelGrayscale = InputImage.GreenChannel;

        }

    }

    [AlgorithmInfo("Grayscale Channels to RGB", Category = "FEI")]
    public class GrayscaletoRGB : Algorithm
    {
        [AlgorithmOutput]
        public RgbImage<byte> Result { get; set; }

        [AlgorithmInput]
        public Image<byte> RedChannelGrayscale { get; set; }

        [AlgorithmInput]
        public Image<byte> BlueChannelGrayscale { get; set; }

        [AlgorithmInput]
        public Image<byte> GreenChannelGrayscale { get; set; }

        public GrayscaletoRGB(Image<byte> redChannelGrayscale, Image<byte> blueChannelGrayscale, Image<byte> greenChannelGrayscale)
        {
            RedChannelGrayscale = redChannelGrayscale;
            BlueChannelGrayscale = blueChannelGrayscale;
            GreenChannelGrayscale = greenChannelGrayscale;
        }

        public override void Run()
        {
            Result = new RgbImage<byte>(RedChannelGrayscale.Width, RedChannelGrayscale.Height);
            for (int i = 0; i < Result.PixelCount ; i++ )
            {
                Result.RedChannel[i] = RedChannelGrayscale[i];
                Result.BlueChannel[i] = BlueChannelGrayscale[i];
                Result.GreenChannel[i] = GreenChannelGrayscale[i];
            }
        }
    }

    [AlgorithmInfo("RGB Change Lightness", Category = "FEI")]
    public class RGBChangeLightness : ImageOperation<RgbImage<byte>, RgbImage<byte>>
    {
        [AlgorithmParameter]
        [DefaultValue(0)]
        public int RedVariation { get; set; }

        [AlgorithmParameter]
        [DefaultValue(0)]
        public int GreenVariation { get; set; }

        [AlgorithmParameter]
        [DefaultValue(0)]
        public int BlueVariation { get; set; }

        public override void Run()
        {
            Result = new RgbImage<byte>(InputImage.Width, InputImage.Height);

            PixelMapping<byte, byte> red = p => (p + RedVariation * 255 / 100).ClipToByte();
            Image<byte> redChannel = new LookupTableTransform<byte>(InputImage.RedChannel, red).Execute();

            PixelMapping<byte, byte> green = p => (p + GreenVariation * 255 / 100).ClipToByte();
            Image<byte> greenChannel = new LookupTableTransform<byte>(InputImage.GreenChannel, green).Execute();

            PixelMapping<byte, byte> blue = p => (p + BlueVariation * 255 / 100).ClipToByte();
            Image<byte> blueChannel = new LookupTableTransform<byte>(InputImage.BlueChannel, blue).Execute();

            for (int i = 0; i < InputImage.PixelCount; i++)
            {
                Result.RedChannel[i] = redChannel[i];
                Result.GreenChannel[i] = greenChannel[i];
                Result.BlueChannel[i] = blueChannel[i];
            }

            //per cambiare la luminosità di tutti i colori in un colpo solo
            //HslImage<byte> workingImage = InputImage.ToByteHslImage();
            //for (int i = 0; i < workingImage.PixelCount; i++)
            //{
            //    if (workingImage.LightnessChannel[i] + RedVariation > 255)
            //    {
            //        workingImage.LightnessChannel[i] = 255;
            //    }
            //    else if (workingImage.LightnessChannel[i] + RedVariation < 0)
            //    {
            //        workingImage.LightnessChannel[i] = 0;
            //    }
            //    else
            //    {
            //        workingImage.LightnessChannel[i] += (byte)RedVariation;
            //    }
            //}
            //Result = workingImage.ToByteRgbImage();
        }

    }

    [AlgorithmInfo("RGB to HSL Channels", Category = "FEI")]
    public class RGBtoHSLChannels : Algorithm
    {
        [AlgorithmInput]
        public RgbImage<byte> InputImage { get; set; }

        [AlgorithmOutput]
        public Image<byte> HueChannel { get; set; }

        [AlgorithmOutput]
        public Image<byte> SaturationChannel { get; set; }

        [AlgorithmOutput]
        public Image<byte> LightnessChannel { get; set; }

        public RGBtoHSLChannels(RgbImage<byte> inputImage)
        {
            InputImage = inputImage;
        }

        public override void Run()
        {
            HslImage<byte> workingImage = InputImage.ToByteHslImage();

            HueChannel = workingImage.HueChannel;
            SaturationChannel = workingImage.SaturationChannel;
            LightnessChannel = workingImage.LightnessChannel;

        }

    }

     [AlgorithmInfo("HSL Channels To RGB", Category = "FEI")]
    public class HSLChannelsToRGB : Algorithm
    {

        [AlgorithmOutput]
        public RgbImage<byte> Result { get; set; }

        [AlgorithmInput]
        public Image<byte> HueChannel { get; set; }

        [AlgorithmInput]
        public Image<byte> SaturationChannel { get; set; }

        [AlgorithmInput]
        public Image<byte> LightnessChannel { get; set; }

        public HSLChannelsToRGB(Image<byte> hue, Image<byte> saturation, Image<byte> lightness)
        {
            HueChannel = hue;
            SaturationChannel = saturation;
            LightnessChannel = lightness;
        }

        public override void Run()
        {
            HslImage<byte> workingImage = new HslImage<byte>(HueChannel.Width, HueChannel.Height);
            for (int i = 0; i < workingImage.PixelCount; i++)
            {
                workingImage.HueChannel[i] = HueChannel[i];
                workingImage.SaturationChannel[i] = SaturationChannel[i];
                workingImage.LightnessChannel[i] = LightnessChannel[i];
            }

            Result = workingImage.ToByteRgbImage();
        }
    }

     [AlgorithmInfo("Grayscale To RGB (singol color shades)", Category = "FEI")]
     public class GrayscaleToRGBSingleColor : ImageOperation<Image<byte>, RgbImage<byte>>
     {
         [AlgorithmParameter]
         [DefaultValue(0)]
         public int Hue { get; set; }

         [AlgorithmParameter]
         [DefaultValue(0)]
         public int Saturation { get; set; }

         public override void Run()
         {
             if (Hue < 0)
             {
                 Hue = 0;
             }
             else if (Hue > 255)
             {
                 Hue = 255;
             }

             if (Saturation < 0)
             {
                 Saturation = 0;
             }
             else if (Saturation > 255)
             {
                 Saturation = 255;
             }

             HslImage<byte> workingImage = new HslImage<byte>(InputImage.Width, InputImage.Height);
             for (int i = 0; i < workingImage.PixelCount; i++ )
             {
                 workingImage.LightnessChannel[i] = InputImage[i];
                 workingImage.HueChannel[i] = (byte)Hue;
                 workingImage.SaturationChannel[i] = (byte)Saturation;
             }

             Result = workingImage.ToByteRgbImage();
        }

     }
    
}
