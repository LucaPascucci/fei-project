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
using BioLab.PatternRecognition.Localization;
using System.Diagnostics;
namespace PRLab.FEI
{

    [AlgorithmInfo("Correlazione", Category = "FEI")]
    [CustomAlgorithmPreviewOutput(typeof(OuputViewerCorrelazione))]
    public class Correlazione : ImageOperation<Image<byte>, Image<int>>
    {

        [AlgorithmInput]
        public Image<byte> Template { get; set; }

        [AlgorithmParameter]
        [DefaultValue(CorrelationMeasure.Zncc)]
        public CorrelationMeasure CorrelationMeasure { get; set; }

        public Correlazione(Image<byte> inputImage, Image<byte> template, CorrelationMeasure correlationMeasure)
            : base(inputImage)
        {
            this.Template = template;
            CorrelationMeasure = correlationMeasure;
        }

        public Correlazione()
        {
            CorrelationMeasure = CorrelationMeasure.Zncc;
        }

        public override void Run()
        {
            if ((this.Template.Width * this.Template.Height) > (int.MaxValue / (255 * 255)))
            {
                throw new Exception("Template troppo grande per calcolo intero a 32bit");
            }
            Result = new Image<int>(this.InputImage.Width, this.InputImage.Height);
            ImageCursor cursor = new ImageCursor(this.Template.Width / 2, this.Template.Height / 2, this.InputImage.Width - 1 - this.Template.Width / 2,
                this.InputImage.Height - 1 - this.Template.Height / 2, this.InputImage);
            ImageCursor templateCursor = new ImageCursor(this.Template);
            switch (this.CorrelationMeasure)
            {
                case CorrelationMeasure.CC:
                    do
                    {
                        int val = 0;
                        do
                        {
                            val += (int)this.InputImage[cursor.Y + templateCursor.Y - Template.Height / 2, cursor.X + templateCursor.X - Template.Width / 2] * this.Template[templateCursor];
                        } while (templateCursor.MoveNext());
                        templateCursor.Restart();
                        Result[cursor] = val;
                    } while (cursor.MoveNext());
                    break;
                case CorrelationMeasure.Ncc:

                    double normaTemplate = 0, normaImg = 0;
                    do
                    {
                        normaTemplate += this.Template[templateCursor] * this.Template[templateCursor];
                    } while (templateCursor.MoveNext());
                    templateCursor.Restart();
                    normaTemplate = Math.Sqrt(normaTemplate);

                    do
                    {
                        double val = 0;
                        do
                        {
                            val += this.InputImage[cursor.Y + templateCursor.Y - Template.Height / 2, cursor.X + templateCursor.X - Template.Width / 2] * this.Template[templateCursor];
                            normaImg += this.InputImage[cursor.Y + templateCursor.Y - Template.Height / 2, cursor.X + templateCursor.X - Template.Width / 2]
                                * this.InputImage[cursor.Y + templateCursor.Y - Template.Height / 2, cursor.X + templateCursor.X - Template.Width / 2];
                        } while (templateCursor.MoveNext());
                        templateCursor.Restart();
                        if (normaImg == 0 || val == 0)
                        {
                            val = 0;
                        }
                        else
                        {
                            normaImg = Math.Sqrt(normaImg);
                            val = ((val / (normaImg * normaTemplate)) * 64);
                        }
                        normaImg = 0;
                        Result[cursor] = (int)val;
                    } while (cursor.MoveNext());
                    break;
                case CorrelationMeasure.Nssd:
                    break;
                case CorrelationMeasure.Ssd:
                    break;
                case CorrelationMeasure.Zncc:

                    break;
                case CorrelationMeasure.Znssd:
                    break;
                default:
                    break;
            }
        }
    }
}
