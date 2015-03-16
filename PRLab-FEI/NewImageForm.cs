using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioLab.GUI.Forms
{
    internal partial class NewImageForm : Form
    {
        public NewImageForm()
        {
            InitializeComponent();
        }

        private void InputImageSizeForm_Load(object sender, EventArgs e)
        {

        }

        private void width_Enter(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, 10);
        }
    }
}
