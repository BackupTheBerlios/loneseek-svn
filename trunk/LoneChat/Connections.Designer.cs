namespace LoneChat
{
    partial class Connections
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bnRem = new System.Windows.Forms.Button();
            this.bnAdd = new System.Windows.Forms.Button();
            this.lsConnections = new ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.bnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bnRem);
            this.groupBox1.Controls.Add(this.bnAdd);
            this.groupBox1.Controls.Add(this.lsConnections);
            this.groupBox1.Location = new System.Drawing.Point(5, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connections:";
            // 
            // bnRem
            // 
            this.bnRem.Location = new System.Drawing.Point(300, 135);
            this.bnRem.Name = "bnRem";
            this.bnRem.Size = new System.Drawing.Size(75, 23);
            this.bnRem.TabIndex = 2;
            this.bnRem.Text = "Remove";
            this.bnRem.UseVisualStyleBackColor = true;
            this.bnRem.Click += new System.EventHandler(this.bnRem_Click);
            // 
            // bnAdd
            // 
            this.bnAdd.Location = new System.Drawing.Point(205, 135);
            this.bnAdd.Name = "bnAdd";
            this.bnAdd.Size = new System.Drawing.Size(75, 23);
            this.bnAdd.TabIndex = 1;
            this.bnAdd.Text = "Add";
            this.bnAdd.UseVisualStyleBackColor = true;
            this.bnAdd.Click += new System.EventHandler(this.bnAdd_Click);
            // 
            // lsConnections
            // 
            this.lsConnections.AllowColumnReorder = true;
            this.lsConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsConnections.DoubleClickActivation = false;
            this.lsConnections.FullRowSelect = true;
            this.lsConnections.LabelEdit = true;
            this.lsConnections.Location = new System.Drawing.Point(6, 19);
            this.lsConnections.MultiSelect = false;
            this.lsConnections.Name = "lsConnections";
            this.lsConnections.Size = new System.Drawing.Size(369, 110);
            this.lsConnections.TabIndex = 0;
            this.lsConnections.UseCompatibleStateImageBehavior = false;
            this.lsConnections.View = System.Windows.Forms.View.Details;
            this.lsConnections.SubItemClicked += new SubItemEventHandler(this.lsConnections_SubItemClicked);
            this.lsConnections.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lsConnections_AfterLabelEdit);
            this.lsConnections.SubItemEndEditing += new SubItemEndEditingEventHandler(this.lsConnections_SubItemEndEditing);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 91;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Host";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Port";
            this.columnHeader3.Width = 43;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Username";
            this.columnHeader4.Width = 103;
            // 
            // bnClose
            // 
            this.bnClose.Location = new System.Drawing.Point(299, 180);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(81, 27);
            this.bnClose.TabIndex = 1;
            this.bnClose.Text = "Close";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // Connections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 213);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Connections";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connections";
            this.Load += new System.EventHandler(this.Connections_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ListViewEx lsConnections;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button bnAdd;
        private System.Windows.Forms.Button bnRem;
        private System.Windows.Forms.Button bnClose;
    }
}