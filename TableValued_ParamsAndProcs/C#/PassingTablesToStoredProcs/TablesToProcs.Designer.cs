namespace PassingTablesToStoredProcs
{
    partial class TablesToProcs
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
            this.buttonSendSmallSet = new System.Windows.Forms.Button();
            this.buttonSendBig = new System.Windows.Forms.Button();
            this.bigSetLabel = new System.Windows.Forms.Label();
            this.buttonSendRowsOneByOne = new System.Windows.Forms.Button();
            this.rowsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSendSmallSet
            // 
            this.buttonSendSmallSet.Location = new System.Drawing.Point(12, 12);
            this.buttonSendSmallSet.Name = "buttonSendSmallSet";
            this.buttonSendSmallSet.Size = new System.Drawing.Size(174, 23);
            this.buttonSendSmallSet.TabIndex = 0;
            this.buttonSendSmallSet.Text = "Send Small Set";
            this.buttonSendSmallSet.UseVisualStyleBackColor = true;
            this.buttonSendSmallSet.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSendBig
            // 
            this.buttonSendBig.Location = new System.Drawing.Point(12, 41);
            this.buttonSendBig.Name = "buttonSendBig";
            this.buttonSendBig.Size = new System.Drawing.Size(174, 23);
            this.buttonSendBig.TabIndex = 1;
            this.buttonSendBig.Text = "Send Big Set";
            this.buttonSendBig.UseVisualStyleBackColor = true;
            this.buttonSendBig.Click += new System.EventHandler(this.buttonSendBig_Click);
            // 
            // bigSetLabel
            // 
            this.bigSetLabel.AutoSize = true;
            this.bigSetLabel.Location = new System.Drawing.Point(192, 46);
            this.bigSetLabel.Name = "bigSetLabel";
            this.bigSetLabel.Size = new System.Drawing.Size(45, 13);
            this.bigSetLabel.TabIndex = 2;
            this.bigSetLabel.Text = "Elapsed";
            // 
            // buttonSendRowsOneByOne
            // 
            this.buttonSendRowsOneByOne.Location = new System.Drawing.Point(12, 70);
            this.buttonSendRowsOneByOne.Name = "buttonSendRowsOneByOne";
            this.buttonSendRowsOneByOne.Size = new System.Drawing.Size(174, 23);
            this.buttonSendRowsOneByOne.TabIndex = 3;
            this.buttonSendRowsOneByOne.Text = "Send Rows One By One";
            this.buttonSendRowsOneByOne.UseVisualStyleBackColor = true;
            this.buttonSendRowsOneByOne.Click += new System.EventHandler(this.buttonSendRowsOneByOne_Click);
            // 
            // rowsLabel
            // 
            this.rowsLabel.AutoSize = true;
            this.rowsLabel.Location = new System.Drawing.Point(192, 75);
            this.rowsLabel.Name = "rowsLabel";
            this.rowsLabel.Size = new System.Drawing.Size(45, 13);
            this.rowsLabel.TabIndex = 4;
            this.rowsLabel.Text = "Elapsed";
            // 
            // TablesToProcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.rowsLabel);
            this.Controls.Add(this.buttonSendRowsOneByOne);
            this.Controls.Add(this.bigSetLabel);
            this.Controls.Add(this.buttonSendBig);
            this.Controls.Add(this.buttonSendSmallSet);
            this.Name = "TablesToProcs";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSendSmallSet;
        private System.Windows.Forms.Button buttonSendBig;
        private System.Windows.Forms.Label bigSetLabel;
        private System.Windows.Forms.Button buttonSendRowsOneByOne;
        private System.Windows.Forms.Label rowsLabel;
    }
}

