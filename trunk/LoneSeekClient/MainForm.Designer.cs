namespace LoneSeekGUI
{
    partial class MainForm
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
            this.bnConnect = new System.Windows.Forms.Button();
            this.bnDisconnect = new System.Windows.Forms.Button();
            this.txText = new System.Windows.Forms.TextBox();
            this.lvRooms = new System.Windows.Forms.ListView();
            this.Room = new System.Windows.Forms.ColumnHeader();
            this.Users = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // bnConnect
            // 
            this.bnConnect.Location = new System.Drawing.Point(54, 30);
            this.bnConnect.Name = "bnConnect";
            this.bnConnect.Size = new System.Drawing.Size(91, 24);
            this.bnConnect.TabIndex = 0;
            this.bnConnect.Text = "connect";
            this.bnConnect.UseVisualStyleBackColor = true;
            this.bnConnect.Click += new System.EventHandler(this.bnConnect_Click);
            // 
            // bnDisconnect
            // 
            this.bnDisconnect.Location = new System.Drawing.Point(54, 69);
            this.bnDisconnect.Name = "bnDisconnect";
            this.bnDisconnect.Size = new System.Drawing.Size(91, 23);
            this.bnDisconnect.TabIndex = 1;
            this.bnDisconnect.Text = "disconnect";
            this.bnDisconnect.UseVisualStyleBackColor = true;
            this.bnDisconnect.Click += new System.EventHandler(this.bnDisconnect_Click);
            // 
            // txText
            // 
            this.txText.Location = new System.Drawing.Point(12, 124);
            this.txText.Multiline = true;
            this.txText.Name = "txText";
            this.txText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txText.Size = new System.Drawing.Size(414, 234);
            this.txText.TabIndex = 2;
            // 
            // lvRooms
            // 
            this.lvRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Room,
            this.Users});
            this.lvRooms.FullRowSelect = true;
            this.lvRooms.Location = new System.Drawing.Point(432, 30);
            this.lvRooms.Name = "lvRooms";
            this.lvRooms.Size = new System.Drawing.Size(209, 328);
            this.lvRooms.TabIndex = 3;
            this.lvRooms.UseCompatibleStateImageBehavior = false;
            this.lvRooms.View = System.Windows.Forms.View.Details;
            // 
            // Room
            // 
            this.Room.Text = "Room";
            this.Room.Width = 128;
            // 
            // Users
            // 
            this.Users.Text = "Users";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 370);
            this.Controls.Add(this.lvRooms);
            this.Controls.Add(this.txText);
            this.Controls.Add(this.bnDisconnect);
            this.Controls.Add(this.bnConnect);
            this.Name = "MainForm";
            this.Text = "LoneSeek";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnConnect;
        private System.Windows.Forms.Button bnDisconnect;
        private System.Windows.Forms.TextBox txText;
        private System.Windows.Forms.ListView lvRooms;
        private System.Windows.Forms.ColumnHeader Room;
        private System.Windows.Forms.ColumnHeader Users;
    }
}

