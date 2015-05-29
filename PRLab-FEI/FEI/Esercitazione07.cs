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
      [BioLab.GUI.Forms.CustomAlgorithmPreviewOutput(
typeof(BioLab.GUI.Forms.ContourExtractionViewer))]
    [AlgorithmInfo("Estrazioni contorni", Category = "FEI")]
    public class EstrazioniContorni : TopologyOperation<List<CityBlockContour>>
    {

        [AlgorithmParameter]
        public MetricType Metric { get; set; }

        
        ImageCursor pixelStart = null;
        CityBlockDirection direzione = 0;
        bool[] bordoVisitato;
        ImageCursor cursor;
        public EstrazioniContorni()
        {
        }

        public EstrazioniContorni(Image<byte> inputImage, byte foreground, MetricType metric)
            : base(inputImage, foreground)
        {
            Metric = metric;
        }

        public override void Run()
        {
            Result = new List<CityBlockContour>();
            cursor = new ImageCursor(InputImage);
            bordoVisitato = new bool[InputImage.PixelCount];

            if (Metric == MetricType.CityBlock)
            {
                do
                {
                    if (InputImage[cursor] == Foreground && !bordoVisitato[cursor] && (InputImage[cursor.West] != Foreground || InputImage[cursor.North] != Foreground || InputImage[cursor.East] != Foreground || InputImage[cursor.South] != Foreground))
                    {
                        PrimoPixel();
                        EstraiContorno(cursor);
                    }
                } while (cursor.MoveNext());
                
            }
        }

        public void PrimoPixel()
        {
            
            CityBlockContour contour = new CityBlockContour(cursor.X,cursor.Y);
           
            direzione = CityBlockDirection.West;
            contour.Add(CityBlockDirection.West);
            
            pixelStart = new ImageCursor(InputImage);
            pixelStart.MoveTo(cursor);
            Result.Add(contour);

            bordoVisitato[cursor] = true;
    
          
            
        }


        public void EstraiContorno(ImageCursor cursor)
        {
            do
            {
                for (int i = 1; i <= 4; i++)
                {
                    direzione = CityBlockMetric.GetNextDirection(direzione);

                    if (InputImage[cursor.GetAt(direzione)] == Foreground)
                    {
                        break;
                    }
                }

                cursor.MoveTo(direzione);
                if (cursor != pixelStart)
                {
                    CityBlockContour contour = new CityBlockContour(cursor.X, cursor.Y);
                    contour.Add(direzione);

                    Result.Add(contour);
                    bordoVisitato[cursor] = true;
                    direzione = CityBlockMetric.GetOppositeDirection(direzione);
                }


            } while (cursor != pixelStart);
        }
        
    }

}
