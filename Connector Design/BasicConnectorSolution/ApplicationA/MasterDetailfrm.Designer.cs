using System.Windows.Forms;
using ApplicationA.Views;

namespace ApplicationA
{
    partial class MasterDetailfrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Views.MasterGrid masterGrid;
        private Views.DetailOverview detailOverview;

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
            this.masterGrid = new ApplicationA.Views.MasterGrid();
            this.detailOverview = new ApplicationA.Views.DetailOverview();
            this.SuspendLayout();
            // 
            // masterGrid
            // 
            this.masterGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.masterGrid.Location = new System.Drawing.Point(0, 225);
            this.masterGrid.Name = "masterGrid";
            this.masterGrid.Size = new System.Drawing.Size(1078, 510);
            this.masterGrid.TabIndex = 0;
            this.masterGrid.Customersdgv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomersdgvRowEnter);
            // 
            // detailOverview
            // 
            this.detailOverview.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailOverview.Location = new System.Drawing.Point(0, 0);
            this.detailOverview.Name = "detailOverview";
            this.detailOverview.Size = new System.Drawing.Size(1078, 272);
            this.detailOverview.TabIndex = 1;
            // 
            // MasterDetailfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 735);
            this.Controls.Add(this.masterGrid);
            this.Controls.Add(this.detailOverview);
            this.Name = "MasterDetailfrm";
            this.Text = "Application A: Windows Client";
            this.ResumeLayout(false);

        }

        #endregion

    }
}

