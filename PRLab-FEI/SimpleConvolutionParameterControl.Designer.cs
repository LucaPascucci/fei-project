namespace BioLab.GUI.UserControls
{
  partial class SimpleConvolutionParameterControl
  {
    private System.ComponentModel.IContainer components = null;

      protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.dataGridViewFilter = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
      this.label3 = new System.Windows.Forms.Label();
      this.numericUpDownFilterSize = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.numericUpDownDenominator = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterSize)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDenominator)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewFilter
      // 
      this.dataGridViewFilter.AllowUserToAddRows = false;
      this.dataGridViewFilter.AllowUserToDeleteRows = false;
      this.dataGridViewFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridViewFilter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
      this.dataGridViewFilter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
      this.dataGridViewFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewFilter.ColumnHeadersVisible = false;
      this.dataGridViewFilter.Location = new System.Drawing.Point(3, 24);
      this.dataGridViewFilter.MultiSelect = false;
      this.dataGridViewFilter.Name = "dataGridViewFilter";
      this.dataGridViewFilter.RowHeadersVisible = false;
      this.dataGridViewFilter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.dataGridViewFilter.Size = new System.Drawing.Size(229, 189);
      this.dataGridViewFilter.TabIndex = 0;
      this.dataGridViewFilter.Validated += new System.EventHandler(this.dataGridViewFilter_Validated);
      this.dataGridViewFilter.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewFilter_CellValidating);
      this.dataGridViewFilter.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFilter_CellEndEdit);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Filter:";
      // 
      // errorProvider
      // 
      this.errorProvider.ContainerControl = this;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label3.Location = new System.Drawing.Point(4, 221);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(89, 18);
      this.label3.TabIndex = 1;
      this.label3.Text = "Filter size:";
      // 
      // numericUpDownFilterSize
      // 
      this.numericUpDownFilterSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.numericUpDownFilterSize.Location = new System.Drawing.Point(99, 219);
      this.numericUpDownFilterSize.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            0});
      this.numericUpDownFilterSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownFilterSize.Name = "numericUpDownFilterSize";
      this.numericUpDownFilterSize.Size = new System.Drawing.Size(53, 20);
      this.numericUpDownFilterSize.TabIndex = 2;
      this.numericUpDownFilterSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.numericUpDownFilterSize.ValueChanged += new System.EventHandler(this.numericUpDownFilterSize_ValueChanged);
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label4.Location = new System.Drawing.Point(4, 247);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(89, 18);
      this.label4.TabIndex = 1;
      this.label4.Text = "Denominator:";
      // 
      // numericUpDownDenominator
      // 
      this.numericUpDownDenominator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.numericUpDownDenominator.Location = new System.Drawing.Point(99, 245);
      this.numericUpDownDenominator.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
      this.numericUpDownDenominator.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownDenominator.Name = "numericUpDownDenominator";
      this.numericUpDownDenominator.Size = new System.Drawing.Size(53, 20);
      this.numericUpDownDenominator.TabIndex = 2;
      this.numericUpDownDenominator.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownDenominator.ValueChanged += new System.EventHandler(this.numericUpDownDenominator_ValueChanged);
      // 
      // SimpleConvolutionParameterControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.numericUpDownDenominator);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.numericUpDownFilterSize);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridViewFilter);
      this.Name = "SimpleConvolutionParameterControl";
      this.Size = new System.Drawing.Size(235, 272);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilterSize)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDenominator)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewFilter;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ErrorProvider errorProvider;
    private System.Windows.Forms.NumericUpDown numericUpDownDenominator;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown numericUpDownFilterSize;
    private System.Windows.Forms.Label label3;
  }
}
