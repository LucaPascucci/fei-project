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
        public ConvoluzioneByteInt(Image<byte> inputImage, ConvolutionFilter<int> filter)
        {
            InputImage = inputImage;
            Filter = filter;
        }

        public override void Run()
        {
            Result = new Image<int>(InputImage.Width, InputImage.Height);
            int w = InputImage.Width;
            int h = InputImage.Height;
            int m = Filter.Size; //dimensione filtro
            int m2 = m / 2;
            int y1 = m2;
            int y2 = h - m2 - 1;
            int x1 = m2;
            int x2 = w - m2 - 1;

            int nM = Filter.Size * Filter.Size; //celle della matrice
            int[] FOff = new int[nM]; //array offset
            int[] FVal = new int[nM]; //array valori
            int maskLen = 0;
            for (int y = 0; y < Filter.Size; y++)
            {
                for (int x = 0; x < Filter.Size; x++)
                {
                    if (Filter[y, x] != 0)
                    {
                        FOff[maskLen] = (m2 - y) * w + (m2 - x);
                        FVal[maskLen] = Filter[y, x];
                        maskLen++;
                    }
                }
            }
            //maxLen arriva sempre a Filter.Size * Filter.Size
            int index = m2 * (w + 1); // indice lineare all'interno dell'immagine
            int indexStepRow = m2 * 2; // aggiustamento indice a fine riga (salta bordi)
            for (int y = y1; y <= y2; y++, index += indexStepRow)
            {
                for (int x = x1; x <= x2; x++)
                {
                    {
                        int val = 0;
                        for (int k = 0; k < maskLen; k++)
                        {
                            val += InputImage[index + FOff[k]] * FVal[k];
                        }
                        Result[index++] = val / Filter.Denominator;
                    }
                }
            }

        }
    }

    [AlgorithmInfo("Smoothing", Category = "FEI")]
    public class Smoothing : ImageOperation<Image<byte>, Image<int>>
    {
        private int dimensioneFiltro;

        [AlgorithmParameter]
        [DefaultValue(0)]
        public int DimensioneFiltro
        {
            get
            {
                return dimensioneFiltro;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Inserire un valore maggiore di 0.");
                }
                dimensioneFiltro = value;
            }
        }


        public override void Run()
        {
            int[] values = new int[dimensioneFiltro * dimensioneFiltro];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 1;
            }
            ConvolutionFilter<int> filter = new ConvolutionFilter<int>(values, values.Length);
            Result = new ConvoluzioneByteInt(InputImage, filter).Execute();
        }
    }

    [AlgorithmInfo("Sharpening", Category = "FEI")]
    public class Sharpening : ImageOperation<Image<byte>, Image<byte>>
    {
        private double pesoForza;

        [AlgorithmParameter]
        [DefaultValue(0)]
        public double PesoForza
        {
            get
            {
                return pesoForza;
            }
            set
            {
                if (value < 0.0 || value > 1.0)
                {
                    throw new ArgumentOutOfRangeException("Inserire un valore compreso tra 0.0 e 1.0");
                }
                pesoForza = value;
            }
        }


        public override void Run()
        {
            Result = new Image<byte>(InputImage.Width, InputImage.Height);
            int[] values = new int[]{-1,-1,-1,-1,8,-1,-1,-1,-1};
            ConvolutionFilter<int> filter = new ConvolutionFilter<int>(values, 1);
            Image<int> resultConvoluzione = new ConvoluzioneByteInt(InputImage, filter).Execute();
            for (int i = 0; i < Result.PixelCount; i++)
            {
                Result[i] = (InputImage[i] + (pesoForza * resultConvoluzione[i])).RoundAndClipToByte();
            }
        }
    }
}
