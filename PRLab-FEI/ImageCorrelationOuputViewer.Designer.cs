namespace PRLab
{
  partial class OuputViewerCorrelazione
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
      this.label1 = new System.Windows.Forms.Label();
      this.numericUpDownMaxima = new System.Windows.Forms.NumericUpDown();
      this.checkBoxShowCorrelationValues = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxima)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 417);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(155, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Numero di massimi da mostrare:";
      // 
      // numericUpDownMaxima
      // 
      this.numericUpDownMaxima.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.numericUpDownMaxima.Location = new System.Drawing.Point(164, 415);
      this.numericUpDownMaxima.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
      this.numericUpDownMaxima.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownMaxima.Name = "numericUpDownMaxima";
      this.numericUpDownMaxima.Size = new System.Drawing.Size(47, 20);
      this.numericUpDownMaxima.TabIndex = 2;
      this.numericUpDownMaxima.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
      this.numericUpDownMaxima.ValueChanged += new System.EventHandler(this.numericUpDownMaxima_ValueChanged);
      // 
      // checkBoxShowCorrelationValues
      // 
      this.checkBoxShowCorrelationValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBoxShowCorrelationValues.AutoSize = true;
      this.checkBoxShowCorrelationValues.Location = new System.Drawing.Point(227, 417);
      this.checkBoxShowCorrelationValues.Name = "checkBoxShowCorrelationValues";
      this.checkBoxShowCorrelationValues.Size = new System.Drawing.Size(146, 17);
      this.checkBoxShowCorrelationValues.TabIndex = 3;
      this.checkBoxShowCorrelationValues.Text = "Mostra valori correlazione";
      this.checkBoxShowCorrelationValues.UseVisualStyleBackColor = true;
      this.checkBoxShowCorrelationValues.CheckedChanged += new System.EventHandler(this.checkBoxShowCorrelationValues_CheckedChanged);
      // 
      // OuputViewerCorrelazione
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.checkBoxShowCorrelationValues);
      this.Controls.Add(this.numericUpDownMaxima);
      this.Controls.Add(this.label1);
      this.Name = "OuputViewerCorrelazione";
      this.Size = new System.Drawing.Size(646, 437);
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxima)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown numericUpDownMaxima;
    private System.Windows.Forms.CheckBox checkBoxShowCorrelationValues;
  }
}
