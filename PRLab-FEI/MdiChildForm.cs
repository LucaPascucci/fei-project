using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioLab.GUI.DataViewers;
using System.Globalization;
using System.IO;

namespace PRLab
{
    public partial class MdiChildForm : Form
    {
        public MdiChildForm(Form mdiParent, DataViewer dataViewer, string name)
            : this()
        {
            MdiParent = mdiParent;
            dataViewer.Dock = DockStyle.Fill;
            Controls.Add(Viewer=dataViewer);
            fileName = name;

            ViewerAsZoomable = dataViewer as IZoomable;
            if (ViewerAsZoomable != null)
            {
                ViewerAsZoomable.ZoomLevelChanged += new EventHandler(zoomableViewer_ZoomLevelChanged);
            }
            ViewerAsEditable = dataViewer as IEditable;
            
            var vd = dataViewer  as DataViewer;
            if (vd!=null)
            {
                vd.EnableEditModeInDefaultContextMenu = true;
                vd.EnablePasteInDefaultContextMenu = true;
                var zvd = vd as ZoomableDataViewer;
                if (zvd != null)
                {
                    zvd.MaxZoomLevel = 100;
                }
            }

            UpdateTitle();
        }

        void zoomableViewer_ZoomLevelChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (ViewerAsZoomable != null)
            {
                Text = string.Format(CultureInfo.InvariantCulture, "{0} @ {1:p0}", FileName, ViewerAsZoomable.ZoomLevel);
            }
            else Text = FileName;
        }

        public MdiChildForm()
        {
            InitializeComponent();
        }


        public DataViewer Viewer { get; private set; }
        public IZoomable ViewerAsZoomable { get; private set; }
        public IEditable ViewerAsEditable { get; private set; }

        public string FileName 
        {
            get
            {
                return fileName;
            }
        }

        private string fileName;
        private string lastSavedPath;

        public string LastSavedPath 
        { 
            get
            {
                return lastSavedPath == null ? fileName : lastSavedPath;
            }
            set
            {
                lastSavedPath = value;
                fileName = Path.GetFileNameWithoutExtension(lastSavedPath);
                UpdateTitle();
            }
        }

        public bool NeverSaved { get { return lastSavedPath == null; } }
    }
}
