namespace PRLab
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;
  using BioLab.ImageProcessing;
  using BioLab.GUI.Forms;
  using BioLab.GUI.DataViewers;
  using System.IO;
  using BioLab.ImageProcessing.ImageFormats;
  using BioLab.GUI.Common;
  using BioLab.Common;
  using BioLab.ImageProcessing.Topology;
  using BioLab.ImageProcessing.Fourier;
  using System.Reflection;
  using System.Globalization;


  public partial class MainForm : Form, IAlgorithmPreviewFormDataProvider
  {
    int newImageCounter = 1;

    public MainForm()
    {
      InitializeComponent();
    }

    private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.ArrangeIcons);
    }

    private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.Cascade);
    }

    private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (Form childForm in MdiChildren)
      {
        childForm.Close();
      }
    }

    private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      IClipboardAware viewer = GetActiveMdiChildClipboardAwareViewer();
      if (viewer != null)
      {
        try
        {
          viewer.Copy();
        }
        catch (Exception ex)
        {
          ExceptionMessageBox.Show(ex, "Error", "Cannot copy to the clipboard");
        }
      }
    }

    private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      IClipboardAware viewer = GetActiveMdiChildClipboardAwareViewer();
      if (viewer != null)
      {
        try
        {
          viewer.Paste();
        }
        catch (Exception ex)
        {
          ExceptionMessageBox.Show(ex, "Error", "Cannot paste from the clipboard");
        }
      }
      UpdateStatusLabel();
    }

    private void menuPasteNew_Click(object sender, EventArgs e)
    {
      ImageBase image;
      try
      {
        image = FormatConverter.ConvertImageFromClipboard();
      }
      catch (Exception ex)
      {
        ExceptionMessageBox.Show(ex, "Error", "Cannot paste from the clipboard");
        return;
      }
      if (image != null)
      {
        CreateChildWindowWithDataViewer("Clipboard image", image);
      }
    }

    private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      Application.Idle += new EventHandler(Application_Idle);
      UpdateStatusLabel();

      // Creazione dinamica menu con algoritmi
      Type[] types = GetTypesWithAlgorithmAttribute();
      AppendOperationsToMenu("Basic operations", basicOperationsToolStripMenuItem.DropDown, types);
      AppendOperationsToMenu("Filtering", filteringToolStripMenuItem.DropDown, types);
      AppendOperationsToMenu("Digital topology", digitalTopologyToolStripMenuItem.DropDown, types);
      AppendOperationsToMenu("Binary morphology", binaryMorphologyToolStripMenuItem.DropDown, types);
      AppendOperationsToMenu("Grayscale morphology", grayscaleMorphologyToolStripMenuItem.DropDown, types);
      AppendOperationsToMenu("Localization", detectionToolStripMenuItem.DropDown, types);

      // Creazione dinamica menu con algoritmi delle esercitazioni FEI
      AppendOperationsToMenu("FEI", esercitazioniToolStripMenuItem.DropDown, types);
    }

    private void AppendOperationsToMenu(string category, ToolStripDropDown menu, Type[] types)
    {
      var itemsToAdd = new List<KeyValuePair<Type, AlgorithmInfoAttribute>>();
      foreach (Type t in types)
      {
        object[] attributes = t.GetCustomAttributes(typeof(AlgorithmInfoAttribute), false);
        if (attributes != null && attributes.Length == 1)
        {
          AlgorithmInfoAttribute attribute = (AlgorithmInfoAttribute)attributes[0];
          if (attribute.Category == category)
          {
            itemsToAdd.Add(new KeyValuePair<Type, AlgorithmInfoAttribute>(t, attribute));
          }
        }
      }

      itemsToAdd.Sort((i1, i2) => Comparer<string>.Default.Compare(i1.Value.Name, i2.Value.Name));
      foreach (var item in itemsToAdd)
      {
        ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Value.Name);
        menuItem.Tag = item.Key;
        menuItem.Click += new EventHandler(automaticMenuItem_Click);
        menu.Items.Add(menuItem);
      }
    }

    private Type[] GetTypesWithAlgorithmAttribute()
    {
      List<Type> typeList = new List<Type>(1000);
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      foreach (var a in assemblies)
      {
        Type[] types = a.GetExportedTypes();
        foreach (var t in types)
        {
          object[] attributes = t.GetCustomAttributes(typeof(AlgorithmInfoAttribute), false);
          if (attributes != null && attributes.Length == 1)
          {
            typeList.Add(t);
          }
        }
      }
      return typeList.ToArray();
    }

    void automaticMenuItem_Click(object sender, EventArgs e)
    {
      OpenAlgorithmPreview((Type)((ToolStripMenuItem)sender).Tag);
    }

    void Application_Idle(object sender, EventArgs e)
    {
      UpdateToolStrips();
    }

    private void OpenFile(object sender, EventArgs e)
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.Filter = "Images (*.jpg;*.png;*.bmp;*.gif;*.tif)|*.jpg;*.png;*.bmp;*.gif;*.tif|All Files (*.*)|*.*";
        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
          var fileName = openFileDialog.FileName;
          OpenFile(fileName);
          return;
        }
      }
    }

    private void OpenFile(string fileName)
    {
      Update(); // cosmetic
      using (new WaitCursor())
      {
        Data data;
        try
        {
          data = ImageBase.LoadFromFile(fileName);
        }
        catch (Exception ex)
        {
          ExceptionMessageBox.Show(ex, "Error", "Cannot load image file");
          return;
        }
        CreateChildWindowWithDataViewer(fileName, data);
        newImageCounter++;
        return;
      }
    }

    private void CreateChildWindowWithDataViewer(string title, object data)
    {
      try
      {
        DataViewer viewer = DataViewerBuilder.CreateDataViewer(data);
        MdiChildForm child = new MdiChildForm(this, viewer, Path.GetFileNameWithoutExtension(title));
        child.Show();
      }
      catch (ArgumentException ex)
      {
        ExceptionMessageBox.Show(ex, "Error", "Cannot create data viewer");
        return;
      }
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DoSaveAs();
    }

    private void DoSaveAs()
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        saveFileDialog.Filter = "PNG (*.png)|*.png|BMP (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|TIFF (*.tif)|*.tif|GIF (*.gif)|*.gif";
        if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        {
          var child = (MdiChildForm)ActiveMdiChild;
          child.LastSavedPath = saveFileDialog.FileName;
          DoSave();
        }
      }
    }

    private void DoSave()
    {
      try
      {
        var child = (MdiChildForm)ActiveMdiChild;
        ImageBase image = child.Viewer.Data as ImageBase;
        image.SaveToFile(child.LastSavedPath);
      }
      catch (Exception ex)
      {
        ExceptionMessageBox.Show(ex, "Cannot save file.");
      }
    }

    private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      statusStrip.Visible = statusBarToolStripMenuItem.Checked;
    }

    private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.TileHorizontal);
    }

    private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LayoutMdi(MdiLayout.TileVertical);
    }

    private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      toolStrip.Visible = toolBarToolStripMenuItem.Checked;
    }


    private void EnableToolStripItems(bool enable, params ToolStripItem[] items)
    {
      foreach (ToolStripItem item in items)
        item.Enabled = enable;
    }

    private void MainForm_MdiChildActivate(object sender, EventArgs e)
    {
      UpdateStatusLabel();
    }

    private void UpdateStatusLabel()
    {
      MdiChildForm child = ActiveMdiChild as MdiChildForm;
      if (child != null)
      {
        toolStripStatusLabel.Text = child.Viewer.Data.ToString();
      }
      else
      {
        toolStripStatusLabel.Text = string.Empty;
      }
    }

    private IZoomable GetActiveMdiChildZoomableViewer()
    {
      MdiChildForm child = ActiveMdiChild as MdiChildForm;
      return child != null ? child.ViewerAsZoomable : null;
    }

    private IClipboardAware GetActiveMdiChildClipboardAwareViewer()
    {
      MdiChildForm child = ActiveMdiChild as MdiChildForm;
      return child != null ? child.Viewer as IClipboardAware : null;
    }

    private Image<byte> GetActiveMdiChildByteImage()
    {
      MdiChildForm child = ActiveMdiChild as MdiChildForm;
      if (child == null)
        return null;
      ImageBase image = child.Viewer.Data as ImageBase;
      if (image == null)
        return null;
      return image.ToByteImage();
    }

    private bool ActiveMdiChildSupportSave()
    {
      MdiChildForm child = ActiveMdiChild as MdiChildForm;
      if (child == null)
        return false;
      ImageBase image = child.Viewer.Data as ImageBase;
      return image != null;
    }

    private void UpdateToolStrips()
    {
        // Zoom
        IZoomable zoomableViewer = GetActiveMdiChildZoomableViewer();
        bool zoomable = zoomableViewer != null;

        EnableToolStripItems(zoomable,
            menuFitToWindow,
            menuZoomIn, menuZoomOut, menuZoomCombo
        );

        if (zoomable)
        {
          menuFitToWindow.Checked = zoomableViewer.FitToScreenSize;
          if (!viewMenu.Pressed)
            menuZoomCombo.Text = string.Format(CultureInfo.InvariantCulture, "{0:p}", zoomableViewer.ZoomLevel);
        }
        else
        {
          menuFitToWindow.Checked = false;
        }

        // Clipboard
        IClipboardAware clipboardAwareViewer = GetActiveMdiChildClipboardAwareViewer();
        menuCopy.Enabled = toolStripButtonCopy.Enabled = clipboardAwareViewer != null && clipboardAwareViewer.CanCopy;
        menuPaste.Enabled = toolStripButtonPaste.Enabled = clipboardAwareViewer != null && clipboardAwareViewer.CanPaste;
        menuPasteNew.Enabled = toolStripButtonPasteNew.Enabled = Clipboard.ContainsImage();

        // Save
        saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = saveAsToolStripMenuItem.Enabled = ActiveMdiChildSupportSave();
    }

    private void menuZoomIn_Click(object sender, EventArgs e)
    {
      IZoomable zoomableViewer = GetActiveMdiChildZoomableViewer();
      if (zoomableViewer != null)
      {
        zoomableViewer.ZoomIn();
      }
    }

    private void menuZoomOut_Click(object sender, EventArgs e)
    {
      IZoomable zoomableViewer = GetActiveMdiChildZoomableViewer();
      if (zoomableViewer != null)
      {
        zoomableViewer.ZoomOut();
      }
    }

    private void menuFitToWindow_Click(object sender, EventArgs e)
    {
      IZoomable zoomableViewer = GetActiveMdiChildZoomableViewer();
      if (zoomableViewer != null)
      {
        zoomableViewer.FitToScreenSize = !zoomableViewer.FitToScreenSize;
      }
    }

    private void menuZoomCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      IZoomable zoomableViewer = GetActiveMdiChildZoomableViewer();
      if (zoomableViewer != null)
      {
        string zoomText = (string)menuZoomCombo.SelectedItem;
        double zoomValue = double.Parse(zoomText.Remove(zoomText.Length - 1)) / 100;
        zoomValue = Math.Max(zoomableViewer.MinZoomLevel, zoomValue);
        zoomValue = Math.Min(zoomableViewer.MaxZoomLevel, zoomValue);
        zoomableViewer.ZoomLevel = zoomValue;
      }
    }

    private void menuZoomCombo_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        e.Handled = true;
        IZoomable zoomableViewer = GetActiveMdiChildZoomableViewer();
        if (zoomableViewer != null)
        {
          string zoomText = (string)menuZoomCombo.Text;
          zoomText.Replace("%", string.Empty);
          double zoomValue;
          try
          {
            zoomValue = double.Parse(zoomText) / 100;
          }
          catch
          {
            return;
          }
          zoomValue = Math.Max(zoomableViewer.MinZoomLevel, zoomValue);
          zoomValue = Math.Min(zoomableViewer.MaxZoomLevel, zoomValue);
          zoomableViewer.ZoomLevel = zoomValue;
        }
      }
    }


    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Idle -= Application_Idle;
    }


    private void OpenAlgorithmPreview(Type algorithmType)
    {
      if (algorithmType.IsGenericTypeDefinition)
      {
        // - Un supporto minimale per algoritmi con parametri generici -
        // Per ora tenta semplicemente di utilizzare byte o Image<byte> come tipo da sostituire
        Type[] genericArguments = algorithmType.GetGenericArguments();
        Type[] types = new Type[genericArguments.Length];
        for (int i = 0; i < types.Length; i++)
        {
          Type[] constraints = genericArguments[i].GetGenericParameterConstraints();
          if (constraints.Contains(typeof(System.ValueType)))
          {
            // Prova a sostituirlo con byte
            types[i] = typeof(byte);
          }
          else
          {
            // Prova a sostituirlo con Image<byte>
            types[i] = typeof(Image<byte>);
          }
        }
        try
        {
          algorithmType = algorithmType.MakeGenericType(types);
        }
        catch (Exception ex)
        {
          ExceptionMessageBox.Show(ex, "Error substituting the generic parameters");
          return;
        }
      }

      try
      {
        using (AlgorithmPreviewForm form = new AlgorithmPreviewForm(algorithmType, this))
        {
          if (form.ShowDialog() == DialogResult.OK)
          {
            foreach (var property in algorithmType.GetProperties())
            {
              if (property.GetCustomAttributes(typeof(AlgorithmOutputAttribute), true).Length > 0)
              {
                // It is an "output" property: get the result if available
                MethodInfo getMethod = property.GetGetMethod();
                if (getMethod != null)
                {
                  object data = getMethod.Invoke(form.Algorithm, null);
                  if (data != null)
                  {
                    CreateChildWindowWithDataViewer(string.Format("{0}-{1}-{2}", algorithmType.Name, property.Name, newImageCounter), data);
                    newImageCounter++;
                  }
                }
              }
            }
          }
        }
      }
      catch (TargetInvocationException ex)
      {
        ExceptionMessageBox.Show(ex.InnerException != null ? ex.InnerException : ex, "An error occurred during the algorithm executon");
      }
      catch (Exception ex)
      {
        ExceptionMessageBox.Show(ex, "An error occurred during the algorithm");
      }
    }

    private void menuBinarization_Click(object sender, EventArgs e)
    {
      OpenAlgorithmPreview(typeof(ImageBinarization));
    }


    private void menuBrightnessAdjustment_Click(object sender, EventArgs e)
    {
      OpenAlgorithmPreview(typeof(BrightnessAdjustment));
    }


    private void menuImageArithmetic_Click(object sender, EventArgs e)
    {
      OpenAlgorithmPreview(typeof(ImageArithmetic));
    }

    public IEnumerable<AlgorithmPreviewFormDataItem> GetAvailableData(Type dataType)
    {
      List<AlgorithmPreviewFormDataItem> data = new List<AlgorithmPreviewFormDataItem>();
      foreach (Form f in MdiChildren)
      {
        MdiChildForm child = f as MdiChildForm;
        if (child != null && child.Viewer.Data != null)
        {
          object d = null;
          string text = child.FileName;
          if (dataType == typeof(Image<byte>) && !(child.Viewer.Data is Image<byte>))
          {
            // In this case it considers also images that can be converted into an Image<byte>
            ImageBase image = child.Viewer.Data as ImageBase;
            if (image != null)
            {
              try
              {
                d = image.ToByteImage();
                text += " (Converted)";
              }
              catch (NotImplementedException)
              {
              }
            }
          }
          else if (dataType == typeof(Image<int>) && !(child.Viewer.Data is Image<int>))
          {
            ImageBase image = child.Viewer.Data as ImageBase;
            if (image != null)
            {

              Image<byte> source = child.Viewer.Data as Image<byte>;
              if (source != null)
              {
                try
                {
                  source = image.ToByteImage();
                  text += " (Converted)";
                  Image<int> dest = new Image<int>(source.Width, source.Height);
                  for (int i = 0; i < dest.PixelCount; i++)
                  {
                    dest[i] = source[i];
                  }
                  d = dest;
                }
                catch (NotImplementedException)
                {
                }
              }
            }
          }
          else
          {
            if (dataType.IsAssignableFrom(child.Viewer.Data.GetType()))
              d = child.Viewer.Data;
          }
          if (d != null)
          {
            if (ActiveMdiChild == child) // mette la finestra attiva come primo elemento dell'elenco
            {
              data.Insert(0, new AlgorithmPreviewFormDataItem(d, text));
            }
            else
            {
              data.Add(new AlgorithmPreviewFormDataItem(d, text));
            }
          }
        }
      }
      return data;
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowAboutBox();
    }

    private void ShowAboutBox()
    {
      using (var f = new AboutBox())
      {
        f.ShowDialog();
      }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MdiChildForm child = ActiveMdiChild as MdiChildForm;
      if (child.NeverSaved)
      {
        DoSaveAs();
      }
      else
      {
        DoSave();
      }
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (var f = new NewImageForm())
      {
        if (f.ShowDialog() == DialogResult.OK)
        {
          CreateChildWindowWithDataViewer(string.Format("Image{0}", newImageCounter), new Image<byte>((int)f.ImageWidth.Value, (int)f.ImageHeight.Value));
          newImageCounter++;
        }
      }
    }

    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
      }
    } 
 


    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
      try
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
        {
          OpenFile(file);
        }
      }
      catch (Exception ex)
      {
        ExceptionMessageBox.Show(ex, "Error during drag and drop operation");
      }
    }

  }

}


