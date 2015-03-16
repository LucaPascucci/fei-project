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

  public abstract class TrasformazioneAffine<TImage> : ImageOperation<TImage, TImage>
    where TImage : ImageBase
  {
    protected TrasformazioneAffine()
    {
    }

    protected TrasformazioneAffine(TImage inputImage, double translationX, double translationY, double rotationDegrees, double scaleFactorX, double scaleFactorY, int resultWidth, int resultHeight)
      : base(inputImage)
    {
      TranslationX = translationX;
      TranslationY = translationY;
      RotationDegrees = rotationDegrees;
      ScaleFactorX = scaleFactorX;
      ScaleFactorY = scaleFactorY;
      ResultWidth = resultWidth;
      ResultHeight = resultHeight;
    }

    protected TrasformazioneAffine(TImage inputImage, double translationX, double translationY, double rotationDegrees)
      : this(inputImage, translationX, translationY, rotationDegrees, 1, 1, inputImage.Width, inputImage.Height)
    {
    }

    protected TrasformazioneAffine(TImage inputImage, double scaleFactor)
      : this(inputImage, 0, 0, 0, scaleFactor, scaleFactor, 0, 0)
    {
    }

    private double translationX;
    private double rotationDegrees;
    private double translationY;
    private double cx = 0.5;
    private double cy = 0.5;
    private double scaleFactorX = 1.0;
    private double scaleFactorY = 1.0;

    [AlgorithmParameter]
    [DefaultValue(0)]
    public double TranslationX { get { return translationX; } set { translationX = value; } }

    [AlgorithmParameter]
    [DefaultValue(0)]
    public double TranslationY { get { return translationY; } set { translationY = value; } }

    [AlgorithmParameter]
    [DefaultValue(0)]
    public double RotationDegrees { get { return rotationDegrees; } set { rotationDegrees = value; } }

    [AlgorithmParameter]
    [DefaultValue(0.5)]
    public double RotationCenterX { get { return cx; } set { cx = value; } }

    [AlgorithmParameter]
    [DefaultValue(0.5)]
    public double RotationCenterY { get { return cy; } set { cy = value; } }

    [AlgorithmParameter]
    [DefaultValue(1)]
    public double ScaleFactorX { get { return scaleFactorX; } set { scaleFactorX = value; } }

    [AlgorithmParameter]
    [DefaultValue(1)]
    public double ScaleFactorY { get { return scaleFactorY; } set { scaleFactorY = value; } }

    [AlgorithmParameter]
    [DefaultValue(0)]
    public int ResultWidth { get; set; }

    [AlgorithmParameter]
    [DefaultValue(0)]
    public int ResultHeight { get; set; }
  }

  [AlgorithmInfo("Trasformazione affine (grayscale)", Category = "FEI")]
  public class TrasformazioneAffineGrayscale : TrasformazioneAffine<Image<byte>>
  {
    [AlgorithmParameter]
    [DefaultValue(0)]
    public byte Background { get; set; }

    public TrasformazioneAffineGrayscale(Image<byte> inputImage, double translationX, double translationY, double rotationDegrees, double scaleFactorX, double scaleFactorY, byte background, int resultWidth, int resultHeight)
      : base(inputImage, translationX, translationY, rotationDegrees, scaleFactorX, scaleFactorY, resultWidth, resultHeight)
    {
      Background = background;
    }

    public TrasformazioneAffineGrayscale(Image<byte> inputImage, double translationX, double translationY, double rotationDegrees, byte background)
      : base(inputImage, translationX, translationY, rotationDegrees)
    {
      Background = background;
    }

    public TrasformazioneAffineGrayscale(Image<byte> inputImage, double scaleFactor, byte background)
      : base(inputImage, scaleFactor)
    {
      Background = background;
    }

    public TrasformazioneAffineGrayscale()
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }

  [AlgorithmInfo("Trasformazione affine (rgb)", Category = "FEI")]
  public class TrasformazioneAffineRgb : TrasformazioneAffine<RgbImage<byte>>
  {
    [AlgorithmParameter]
    public RgbPixel<byte> Background { get; set; }

    public TrasformazioneAffineRgb(RgbImage<byte> inputImage, double translationX, double translationY, double rotationDegrees, double scaleFactorX, double scaleFactorY, RgbPixel<byte> background, int resultWidth, int resultHeight)
      : base(inputImage, translationX, translationY, rotationDegrees, scaleFactorX, scaleFactorY, resultWidth, resultHeight)
    {
      Background = background;
    }

    public TrasformazioneAffineRgb(RgbImage<byte> inputImage, double translationX, double translationY, double rotationDegrees, RgbPixel<byte> background)
      : base(inputImage, translationX, translationY, rotationDegrees)
    {
      Background = background;
    }

    public TrasformazioneAffineRgb(RgbImage<byte> inputImage, double scaleFactor, RgbPixel<byte> background)
      : base(inputImage, scaleFactor)
    {
      Background = background;
    }

    public TrasformazioneAffineRgb()
    {
    }

    public override void Run()
    {
      throw new NotImplementedException();
    }
  }

}
