namespace PRLab
{

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows.Forms;
  using BioLab.ImageProcessing.ImageFormats;

  static class Program
  {

    #region Methods (1)


    // Private Methods (1) 

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      FormatConverter.CheckForGrayScaleImages = true;
      Application.Run(new MainForm());
    }


    #endregion Methods

  }
}
