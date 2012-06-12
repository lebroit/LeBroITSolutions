namespace ApplicationA.Views
{
    partial class MasterGrid
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
            this.Customersdgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Customersdgv)).BeginInit();
            this.SuspendLayout();
            // 
            // Customersdgv
            // 
            this.Customersdgv.AllowUserToOrderColumns = true;
            this.Customersdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Customersdgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Customersdgv.Location = new System.Drawing.Point(0, 0);
            this.Customersdgv.Name = "Customersdgv";
            this.Customersdgv.Size = new System.Drawing.Size(1080, 510);
            this.Customersdgv.TabIndex = 1;
            // 
            // MasterGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Customersdgv);
            this.Name = "MasterGrid";
            this.Size = new System.Drawing.Size(1080, 510);
            ((System.ComponentModel.ISupportInitialize)(this.Customersdgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Customersdgv;
    }
}
