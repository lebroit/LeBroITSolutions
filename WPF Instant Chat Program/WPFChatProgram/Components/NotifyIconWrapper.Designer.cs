namespace WPFChatProgram.Components
{
    partial class NotifyIconWrapper
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            this.contextMenuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClose = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdWhosOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdHide = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdConfigSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconWrap = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotify.SuspendLayout();
            // 
            // contextMenuNotify
            // 
            this.contextMenuNotify.BackColor = System.Drawing.Color.AliceBlue;
            this.contextMenuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdOpen,
            this.cmdClose,
            this.cmdWhosOnline,
            this.cmdHide,
            this.cmdConfigSettings});
            this.contextMenuNotify.Name = "contextMenuNotify";
            this.contextMenuNotify.ShowImageMargin = false;
            this.contextMenuNotify.Size = new System.Drawing.Size(236, 114);
            this.contextMenuNotify.Text = "Instant Chat Menu";
            // 
            // cmdOpen
            // 
            this.cmdOpen.BackColor = System.Drawing.Color.AliceBlue;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.O)));
            this.cmdOpen.Size = new System.Drawing.Size(235, 22);
            
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.E)));
            this.cmdClose.Size = new System.Drawing.Size(235, 22);
           
            // 
            // cmdWhosOnline
            // 
            this.cmdWhosOnline.BackColor = System.Drawing.Color.AliceBlue;
            this.cmdWhosOnline.Name = "cmdWhosOnline";
            this.cmdWhosOnline.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.W)));
            this.cmdWhosOnline.Size = new System.Drawing.Size(235, 22);
            
            // 
            // cmdHide
            // 
            this.cmdHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.cmdHide.Name = "cmdHide";
            this.cmdHide.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.H)));
            this.cmdHide.Size = new System.Drawing.Size(235, 22);
            
            // 
            // cmdConfigSettings
            // 
            this.cmdConfigSettings.Name = "cmdConfigSettings";
            this.cmdConfigSettings.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.cmdConfigSettings.Size = new System.Drawing.Size(235, 22);
            
            // 
            // notifyIconWrap
            // 
            this.notifyIconWrap.BalloonTipText = "Klik hier als u het Chat venster wilt openen of wilt weten wie er online zijn!";
            this.notifyIconWrap.BalloonTipTitle = "Instant Chat V0.01 Beta, written by Leo Broos";
            this.notifyIconWrap.ContextMenuStrip = this.contextMenuNotify;
            this.notifyIconWrap.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconWrap.Icon")));
            this.notifyIconWrap.Text = "Instant Chat V0.01 Beta, ©LebroITSolutions 2010";
            this.notifyIconWrap.Visible = true;
            this.contextMenuNotify.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuNotify;
        private System.Windows.Forms.ToolStripMenuItem cmdOpen;
        private System.Windows.Forms.ToolStripMenuItem cmdClose;
        private System.Windows.Forms.ToolStripMenuItem cmdWhosOnline;
        private System.Windows.Forms.NotifyIcon notifyIconWrap;
        private System.Windows.Forms.ToolStripMenuItem cmdHide;
        private System.Windows.Forms.ToolStripMenuItem cmdConfigSettings;
    }
}
