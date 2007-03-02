namespace LoneChat
{
    partial class LoneChat
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "SoulSeek Network",
            "sk6.slsknet.org",
            "2240"}, -1);
            this.tbTabs = new System.Windows.Forms.TabControl();
            this.tbServers = new System.Windows.Forms.TabPage();
            this.lsServers = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.tbTabs.SuspendLayout();
            this.tbServers.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTabs
            // 
            this.tbTabs.Controls.Add(this.tbServers);
            this.tbTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTabs.Location = new System.Drawing.Point(0, 24);
            this.tbTabs.Multiline = true;
            this.tbTabs.Name = "tbTabs";
            this.tbTabs.SelectedIndex = 0;
            this.tbTabs.Size = new System.Drawing.Size(583, 399);
            this.tbTabs.TabIndex = 0;
            // 
            // tbServers
            // 
            this.tbServers.Controls.Add(this.lsServers);
            this.tbServers.Location = new System.Drawing.Point(4, 22);
            this.tbServers.Name = "tbServers";
            this.tbServers.Padding = new System.Windows.Forms.Padding(3);
            this.tbServers.Size = new System.Drawing.Size(575, 373);
            this.tbServers.TabIndex = 0;
            this.tbServers.Text = "Servers";
            this.tbServers.UseVisualStyleBackColor = true;
            // 
            // lsServers
            // 
            this.lsServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.lsServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsServers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lsServers.Location = new System.Drawing.Point(3, 3);
            this.lsServers.Name = "lsServers";
            this.lsServers.Size = new System.Drawing.Size(569, 367);
            this.lsServers.TabIndex = 0;
            this.lsServers.UseCompatibleStateImageBehavior = false;
            this.lsServers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 138;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Host/IP Adress";
            this.columnHeader2.Width = 135;
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(583, 24);
            this.msMenu.TabIndex = 1;
            this.msMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewConnection,
            this.toolStripMenuItem1,
            this.mnuQuit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuNewConnection
            // 
            this.mnuNewConnection.Name = "mnuNewConnection";
            this.mnuNewConnection.Size = new System.Drawing.Size(173, 22);
            this.mnuNewConnection.Text = "New connection...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 6);
            // 
            // mnuQuit
            // 
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(173, 22);
            this.mnuQuit.Text = "Quit";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Port";
            this.columnHeader4.Width = 79;
            // 
            // LoneChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 423);
            this.Controls.Add(this.tbTabs);
            this.Controls.Add(this.msMenu);
            this.MainMenuStrip = this.msMenu;
            this.Name = "LoneChat";
            this.Text = "LoneChat";
            this.tbTabs.ResumeLayout(false);
            this.tbServers.ResumeLayout(false);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbTabs;
        private System.Windows.Forms.TabPage tbServers;
        private System.Windows.Forms.ListView lsServers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuQuit;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

