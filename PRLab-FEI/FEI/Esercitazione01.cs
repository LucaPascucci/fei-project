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

namespace PRLab.FEI
{

    [AlgorithmInfo("Negativo Grayscale", Category = "FEI")]
    public class NegativeImage : ImageOperation<Image<byte>, Image<byte>>
    {
        public override void Run()
        {

            Result = new Image<byte>(InputImage.Width, InputImage.Height);
            for (int i = 0; i < Result.PixelCount; i++)
            {
                Result[i] = (byte)(255 - InputImage[i]);
            }
        }
    }

    [AlgorithmInfo("Negativo RGB", Category = "FEI")]
    public class NegativeRgbImage : ImageOperation<RgbImage<byte>, RgbImage<byte>>
    {
        public override void Run()
        {

            Result = new RgbImage<byte>(InputImage.Width, InputImage.Height);
            for (int i = 0; i < Result.PixelCount; i++)
            {
                Result.BlueChannel[i] = (byte)(255 - InputImage.BlueChannel[i]);
                Result.RedChannel[i] = (byte)(255 - InputImage.RedChannel[i]);
                Result.GreenChannel[i] = (byte)(255 - InputImage.GreenChannel[i]);
            }
        }
    }

    [AlgorithmInfo("Variazione Luminosità", Category = "FEI")]
    public class BrightnessModifier : ImageOperation<Image<byte>, Image<byte>>
    {

        [AlgorithmParameter]
        public int Variazione
        {
            get;
            set;
        }

        public override void Run()
        {
            PixelMapping<byte, byte> f = p => (p + Variazione * 255 / 100).ClipToByte();
            Result = new LookupTableTransform<byte>(InputImage, f).Execute();
        }
    }

    [AlgorithmInfo("Pseudocolori", Category = "FEI")]
    public class Pseudocolori : ImageOperation<Image<byte>, RgbImage<byte>>
    {
        public override void Run()
        {
            LookupTableTransform<RgbPixel<byte>> lookupTable = new LookupTableTransform<RgbPixel<byte>>(InputImage, LookupTables.Spectrum);
            lookupTable.Run();

            RgbLookupTableTransform<byte> rgbLookupTable = new RgbLookupTableTransform<byte>(InputImage, lookupTable.LookupTable);
            rgbLookupTable.Run();
            Result = rgbLookupTable.Result;

        }
    }

}
