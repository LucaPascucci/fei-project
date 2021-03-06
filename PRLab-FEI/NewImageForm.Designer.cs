namespace BioLab.GUI.Forms
{
    partial class NewImageForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.ImageWidth = new System.Windows.Forms.NumericUpDown();
            this.ImageHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(94, 87);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(13, 87);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // ImageWidth
            // 
            this.ImageWidth.Location = new System.Drawing.Point(57, 21);
            this.ImageWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ImageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ImageWidth.Name = "ImageWidth";
            this.ImageWidth.Size = new System.Drawing.Size(68, 20);
            this.ImageWidth.TabIndex = 1;
            this.ImageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ImageWidth.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ImageWidth.Enter += new System.EventHandler(this.width_Enter);
            // 
            // ImageHeight
            // 
            this.ImageHeight.Location = new System.Drawing.Point(57, 47);
            this.ImageHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ImageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ImageHeight.Name = "ImageHeight";
            this.ImageHeight.Size = new System.Drawing.Size(68, 20);
            this.ImageHeight.TabIndex = 4;
            this.ImageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ImageHeight.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ImageHeight.Enter += new System.EventHandler(this.width_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "pixel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "pixel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "&Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "&Height";
            // 
            // NewImageForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(181, 122);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ImageHeight);
            this.Controls.Add(this.ImageWidth);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewImageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New image";
            this.Load += new System.EventHandler(this.InputImageSizeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown ImageWidth;
        public System.Windows.Forms.NumericUpDown ImageHeight;
    }
}