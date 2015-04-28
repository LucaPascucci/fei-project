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
    [AlgorithmInfo("Calcola Modulo Gradiente", Category = "FEI")]
    public class CalcolaModuloGradiente : ImageOperation<Image<byte>, Image<int>>
    {

        public CalcolaModuloGradiente(Image<byte> input)
        {
            InputImage = input;
        }
        public override void Run()
        {
            Result = new Image<int>(InputImage.Width, InputImage.Height);
            int[] x_values = new int[] { 1, 0, -1, 1, 0, -1, 1, 0, -1 };
            int[] y_values = new int[] { 1, 1, 1, 0, 0, 0, -1, -1, -1 };
            ConvolutionFilter<int> filter_X = new ConvolutionFilter<int>(x_values, 3);
            ConvolutionFilter<int> filter_Y = new ConvolutionFilter<int>(y_values, 3);
            Image<int> resultConvoluzione_X = new ConvoluzioneByteInt(InputImage, filter_X).Execute();
            Image<int> resultConvoluzione_Y = new ConvoluzioneByteInt(InputImage, filter_Y).Execute();
            OnIntermediateResult(new AlgorithmIntermediateResultEventArgs(resultConvoluzione_X, "Convoluzione con filtro X"));
            OnIntermediateResult(new AlgorithmIntermediateResultEventArgs(resultConvoluzione_Y, "Convoluzione con filtro Y"));
            int somma = 0;
            for (int i = 0; i < Result.PixelCount; i++)
            {

                somma = resultConvoluzione_X[i] * resultConvoluzione_X[i] + resultConvoluzione_Y[i] * resultConvoluzione_Y[i];
                Result[i] = (int)(Math.Sqrt(somma));
            }
        }
    }




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

        private int min3way(int first, int second, int third)
        {
            int min = Math.Min(first,second);
            return Math.Min(min, third);
        }

        private int min4way(int first, int second, int third, int fourth)
        {
            int min = min3way(first, second, third);
            return Math.Min(min, fourth);
        }

        private int min5way(int first, int second, int third, int fourth,int fifth)
        {
            int min = min4way(first, second, third, fourth);
            return Math.Min(min, fifth);
        }

        public override void Run()
        {
            Result = new Image<int>(InputImage.Width, InputImage.Height);
            var r = Result;
            var cursor = new ImageCursor(r, 1);
            if (Metric == MetricType.CityBlock)
            {
                do
                {
                    if (InputImage[cursor] == Foreground)
                    {
                        r[cursor] =Math.Min(r[cursor.West], r[cursor.North]) + 1;
                    }
                } while (cursor.MoveNext());
                do
                {
                    if (InputImage[cursor] == Foreground)
                    {
                        r[cursor] = min3way(r[cursor.East] + 1, r[cursor.South] + 1, r[cursor]);
                    }
                } while (cursor.MovePrevious());
            }
            else
            {
                do
                {
                    if (InputImage[cursor] == Foreground)
                    {
                        r[cursor] = min4way(r[cursor.West], r[cursor.North], r[cursor.Northeast], r[cursor.Northwest]) + 1;
                    }
                } while (cursor.MoveNext());
                do
                {
                    if (InputImage[cursor] == Foreground)
                    {
                        r[cursor] = min5way(r[cursor.East] + 1, r[cursor.South] + 1, r[cursor.Southeast] + 1 , r[cursor.Southwest] + 1 , r[cursor]);
                    }
                } while (cursor.MovePrevious());
            }
        }

        [AlgorithmParameter]
        public MetricType Metric { get; set; }
    }


}
