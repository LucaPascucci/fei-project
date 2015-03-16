using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioLab.GUI.Forms;
using BioLab.Common;
using BioLab.ImageProcessing;
using BioLab.PatternRecognition.Localization;
using BioLab.DataStructures;
using BioLab.Math.Geometry;
using BioLab.ImageProcessing.Topology;
using BioLab.GUI.DataViewers;
using PRLab.FEI;

namespace PRLab
{
  internal partial class OuputViewerCorrelazione : UserControl, IAlgorithmPreviewOutput
  {
    public OuputViewerCorrelazione()
    {
      InitializeComponent();
      viewer.Location = Point.Empty;
      viewer.Width = ClientSize.Width;
      viewer.Height = numericUpDownMaxima.Top - 5;
      viewer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
      Controls.Add(viewer);
      viewer.Paint+=new PaintEventHandler(viewer_Paint);
    }

    ImageViewer viewer = new ImageViewer();

    public void UpdateOutputViewer(IAlgorithm algorithm)
    {
      ic = (Correlazione)algorithm;
      Recalculate();
    }

    Correlazione ic;

    private void Recalculate()
    {
      result = ic.Result;

      var values = result.CopyPixels();
      bool isCC = ic.CorrelationMeasure == CorrelationMeasure.CC || ic.CorrelationMeasure == CorrelationMeasure.Ncc || ic.CorrelationMeasure == CorrelationMeasure.Zncc;
      var sortedIndices = ArrayUtilities.GetSortedIndexes(values, !isCC);

      maxima.Clear();
      var cursor = new ImageCursor(result, 1);
      for (int i = 0; i < sortedIndices.Length; i++)
      {
        cursor.MoveTo(sortedIndices[i]);
        bool isMaximum = true;
        var v = result[cursor];
        for (var d = ChessboardDirection.East; d <= ChessboardDirection.Southeast; d++)
        {
          if (isCC && result[cursor.GetAt(d)] > v ||
              !isCC && result[cursor.GetAt(d)] < v)
          {
            isMaximum = false;
            break;
          }
        }
        if (isMaximum)
        {
          maxima.Add((IntPoint2D)cursor);
          if (maxima.Count >= numericUpDownMaxima.Value)
          {
            break;
          }
        }
      }

      UpdateImage();
    }

    private void UpdateImage()
    {
      if (checkBoxShowCorrelationValues.Checked)
      {
        viewer.Image = ic.Result.ToByteImage(PixelConversionMethod.Stretch);
      }
      else
      {
        viewer.Image = ic.InputImage;
      }      
      viewer.Invalidate();
    }

    Image<int> result;
    List<IntPoint2D> maxima = new List<IntPoint2D>();

    private void viewer_Paint(object sender, PaintEventArgs e)
    {
      if (result != null)
      {
        viewer.AdjustGraphicsToWorldUnits(e.Graphics);
        foreach (var p in maxima)
        {
          const int size = 3;
          var center = p.ToPoint();
          e.Graphics.DrawLine(Pens.Red, center.X - size, center.Y, center.X + size, center.Y);
          e.Graphics.DrawLine(Pens.Red, center.X, center.Y-size, center.X , center.Y+size);
        }
      }
    }

    private void numericUpDownMaxima_ValueChanged(object sender, EventArgs e)
    {
      Recalculate();
    }

    private void checkBoxShowCorrelationValues_CheckedChanged(object sender, EventArgs e)
    {
      UpdateImage();
    }


  }
}
