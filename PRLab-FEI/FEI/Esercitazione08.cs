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


    [AlgorithmInfo("Etichettatura delle componenti connesse", Category = "FEI")]
    public class EtichettaturaComponentiConnesse : TopologyOperation<ConnectedComponentImage>
    {
        public EtichettaturaComponentiConnesse(Image<byte> inputImage, byte foreground, MetricType metric): base(inputImage, foreground)
        {
            Metric = metric;
        }

        public EtichettaturaComponentiConnesse(Image<byte> inputImage): this(inputImage, 255, MetricType.Chessboard)
        {
        }

        public EtichettaturaComponentiConnesse()
        {
            Metric = MetricType.Chessboard;
        }

        public override void Run()
        {
            Result = new ConnectedComponentImage(InputImage.Width, InputImage.Height, -1);
            var cursor = new ImageCursor(InputImage, 1); // per semplicità ignora i bordi (1 pixel)            
            int[] neighborLabels = new int[Metric == MetricType.CityBlock ? 2 : 4];
            int nextLabel = 0;
            var equivalences = new DisjointSets(InputImage.PixelCount);
            do
            { // prima scansione
                if (InputImage[cursor] == Foreground)
                {
                    int labelCount = 0;
                    if (Result[cursor.West] >= 0) 
                    { 
                        neighborLabels[labelCount++] = Result[cursor.West];
                    }
                    if (Result[cursor.North] >= 0)
                    {
                        neighborLabels[labelCount++] = Result[cursor.North];
                    }
                    if (Metric == MetricType.Chessboard)
                    {   // anche le diagonali
                        if (Result[cursor.Northwest] >= 0) neighborLabels[labelCount++] = Result[cursor.Northwest];
                        if (Result[cursor.Northeast] >= 0) neighborLabels[labelCount++] = Result[cursor.Northeast];
                    }
                    if (labelCount == 0)
                    {
                        equivalences.MakeSet(nextLabel); // crea un nuovo set
                        Result[cursor] = nextLabel++; // le etichette iniziano da 0
                    }
                    else
                    {
                        int l = Result[cursor] = neighborLabels[0]; // seleziona la prima
                        for (int i = 1; i < labelCount; i++) // equivalenze
                            if (neighborLabels[i] != l)
                                equivalences.MakeUnion(neighborLabels[i], l); // le rende equivalenti
                    }
                }
            } while (cursor.MoveNext());
            int totalLabels;
            int[] corresp = equivalences.Renumber(nextLabel, out totalLabels); // seconda e ultima scansione 
            cursor.Restart(); 
            do { 
                int l = Result[cursor]; 
                if (l >= 0) { 
                    Result[cursor] = corresp[l]; 
                } 
            } while (cursor.MoveNext()); 
            Result.ComponentCount = totalLabels;

        }

        [AlgorithmParameter]
        [DefaultValue(MetricType.Chessboard)]
        public MetricType Metric { get; set; }
    }

    [AlgorithmInfo("Informazioni delle componenti connesse", Category = "FEI")]
    public class InformazioniComponentiConnesse : Algorithm
    {
        [AlgorithmOutput]
        public int ComponentiConnesse { get; set; }

        [AlgorithmOutput]
        public int AreaMinima { get; set; }

        [AlgorithmOutput]
        public double AreaMedia { get; set; }

        [AlgorithmOutput]
        public int AreaMassima { get; set; }

        //da fare questo
        [AlgorithmOutput]
        public double PerimetroMedio { get; set; }

        [AlgorithmParameter]
        [DefaultValue(255)]
        public byte ForegroundValue { get; set; }

        [AlgorithmInput]
        public Image<byte> InputImage { get; set; }

        public InformazioniComponentiConnesse(Image<byte> inputImage, byte foreground)
        {
            ForegroundValue = foreground;
            InputImage = inputImage;
        }

        public override void Run()
        {
            EtichettaturaComponentiConnesse elaboratore = new EtichettaturaComponentiConnesse(InputImage, ForegroundValue, MetricType.Chessboard);
            ConnectedComponentImage risultatoElborazione = elaboratore.Execute();

            ComponentiConnesse = risultatoElborazione.ComponentCount;

            int[] componenti = new int[risultatoElborazione.ComponentCount];

            for (int i = 0; i < risultatoElborazione.PixelCount; i++)
            {
                if (!risultatoElborazione[i].Equals(-1))
                {
                    componenti[risultatoElborazione[i]]++;
                }
            }
            int areamin = componenti[0];
            int areamax = componenti[0];
            int areatotale = 0;
            for (int i = 0; i < risultatoElborazione.ComponentCount; i++)
            {
                areatotale += componenti[i];

                if (areamin > componenti[i])
                {
                    areamin = componenti[i];
                }
                if (areamax < componenti[i])
                {
                    areamax = componenti[i];
                }
            }

            AreaMinima = areamin;
            AreaMassima = areamax;
            AreaMedia = (double)(areatotale) / (double)(risultatoElborazione.ComponentCount);
        }

    }
}
