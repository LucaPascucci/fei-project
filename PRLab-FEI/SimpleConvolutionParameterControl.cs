using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioLab.GUI.Forms;
using BioLab.ImageProcessing;
using PRLab.FEI;

namespace BioLab.GUI.UserControls
{
  internal partial class SimpleConvolutionParameterControl : UserControl, IAlgorithmPreviewParameters
  {
    public SimpleConvolutionParameterControl()
    {
      InitializeComponent();
    }

    private void dataGridViewFilter_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
      int val;
      String str = e.FormattedValue.ToString();
      if (str != "")  // la stringa nulla è ammessa: va trattata come "0"
      {
        try
        {
          val = int.Parse(str);
        }
        catch
        {
          e.Cancel = true;
          errorProvider.SetError(dataGridViewFilter, "Enter a number");
          return;
        }
      }
    }

    private void dataGridViewFilter_Validated(object sender, EventArgs e)
    {
      errorProvider.SetError(dataGridViewFilter, "");
    }

    private void numericUpDownFilterSize_ValueChanged(object sender, EventArgs e)
    {
      dataGridViewFilter.ColumnCount = (int)numericUpDownFilterSize.Value;
      dataGridViewFilter.RowCount = (int)numericUpDownFilterSize.Value;
      OnParametersChanged();
    }

    private void numericUpDownDenominator_ValueChanged(object sender, EventArgs e)
    {
      OnParametersChanged();
    }

    private void numericUpDownAdditionalBorder_ValueChanged(object sender, EventArgs e)
    {
      OnParametersChanged();
    }

    public void ExchangeValues(BioLab.Common.IAlgorithm algorithm, bool toControl)
    {
      var conv = (ConvoluzioneByteInt)algorithm;
      if (toControl)
      {
        if (conv.Filter == null)
        {
          dataGridViewFilter.ColumnCount = 3;
          dataGridViewFilter.RowCount = 3;
          numericUpDownFilterSize.Value = 3;
          numericUpDownDenominator.Value = 1;
        }
        else
        {
          dataGridViewFilter.ColumnCount = conv.Filter.Size;
          dataGridViewFilter.RowCount = conv.Filter.Size;
          numericUpDownFilterSize.Value = conv.Filter.Size;
          for (int i = 0; i < conv.Filter.Size; i++)
          {
            for (int j = 0; j < conv.Filter.Size; j++)
            {
              dataGridViewFilter.Rows[i].Cells[j].Value = conv.Filter[i, j];
            }
          }
          numericUpDownDenominator.Value = conv.Filter.Denominator;
        }
      }
      else
      {
        int newSize = (int)numericUpDownFilterSize.Value;
        var f = new int[newSize*newSize];
        for (int i = 0; i < newSize; i++)
        {
          for (int j = 0; j < newSize; j++)
          {
            try
            {
              var str = dataGridViewFilter.Rows[i].Cells[j].Value.ToString();
              if (str == string.Empty)
                str = "0";
              f[i*newSize+ j] = int.Parse(str);
            }
            catch
            {
              f[i*newSize+ j] = 0;
            }
          }
        }
        conv.Filter = new ConvolutionFilter<int>(f, (int)numericUpDownDenominator.Value);        
      }
    }

    protected virtual void OnParametersChanged()
    {
      EventHandler tmp = ParametersChanged;
      if (tmp != null)
        tmp(this, EventArgs.Empty);
    }

    public event EventHandler ParametersChanged;

    private void dataGridViewFilter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      OnParametersChanged();
    }
  }
}
