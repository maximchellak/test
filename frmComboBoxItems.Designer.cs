
namespace StarNet.Forms.UI
{
    partial class frmComboBoxItems
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
            Gizmox.WebGUI.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new Gizmox.WebGUI.Forms.DataGridViewCellStyle();
            Gizmox.WebGUI.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new Gizmox.WebGUI.Forms.DataGridViewCellStyle();
            this.grdComboData = new Gizmox.WebGUI.Forms.DataGridView();
            this.objCol1 = new Gizmox.WebGUI.Forms.DataGridViewTextBoxColumn();
            this.objCol2 = new Gizmox.WebGUI.Forms.DataGridViewTextBoxColumn();
            this.pnlTexts = new Gizmox.WebGUI.Forms.Panel();
            this.txtCol2 = new StarNet.Controls.UI.TextBox();
            this.txtCol1 = new StarNet.Controls.UI.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdComboData)).BeginInit();
            this.pnlTexts.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdComboData
            // 
            this.grdComboData.AllowUserToAddRows = false;
            this.grdComboData.AllowUserToDeleteRows = false;
            this.grdComboData.AllowUserToResizeRows = false;
            this.grdComboData.AutoGenerateColumns = false;
            this.grdComboData.Columns.AddRange(new Gizmox.WebGUI.Forms.DataGridViewColumn[] {
            this.objCol1,
            this.objCol2});
            this.grdComboData.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.grdComboData.Location = new System.Drawing.Point(2, 30);
            this.grdComboData.Name = "grdComboData";
            this.grdComboData.RowHeadersVisible = false;
            this.grdComboData.ScrollBars = Gizmox.WebGUI.Forms.ScrollBars.Vertical;
            this.grdComboData.SelectionMode = Gizmox.WebGUI.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdComboData.Size = new System.Drawing.Size(651, 434);
            this.grdComboData.TabIndex = 0;
            this.grdComboData.CellContentDoubleClick += new Gizmox.WebGUI.Forms.DataGridViewCellEventHandler(this.grdComboData_CellContentDoubleClick);
            this.grdComboData.CellDoubleClick += new Gizmox.WebGUI.Forms.DataGridViewCellEventHandler(this.grdComboData_CellDoubleClick);
            this.grdComboData.ColumnWidthChanged += new Gizmox.WebGUI.Forms.DataGridViewColumnEventHandler(this.grdComboData_ColumnWidthChanged);
            // 
            // objCol1
            // 
            this.objCol1.DataPropertyName = "id";
            this.objCol1.DefaultCellStyle = dataGridViewCellStyle1;
            this.objCol1.DefaultHeaderCellType = typeof(Gizmox.WebGUI.Forms.DataGridViewColumnHeaderCell);
            this.objCol1.HeaderText = "קוד";
            this.objCol1.Name = "objCol1";
            this.objCol1.ReadOnly = true;
            this.objCol1.Resizable = Gizmox.WebGUI.Forms.DataGridViewTriState.True;
            this.objCol1.Width = 150;
            // 
            // objCol2
            // 
            this.objCol2.AutoSizeMode = Gizmox.WebGUI.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.objCol2.DataPropertyName = "text";
            this.objCol2.DefaultCellStyle = dataGridViewCellStyle2;
            this.objCol2.DefaultHeaderCellType = typeof(Gizmox.WebGUI.Forms.DataGridViewColumnHeaderCell);
            this.objCol2.HeaderText = "תצוגה";
            this.objCol2.Name = "objCol2";
            this.objCol2.ReadOnly = true;
            this.objCol2.Resizable = Gizmox.WebGUI.Forms.DataGridViewTriState.True;
            // 
            // pnlTexts
            // 
            this.pnlTexts.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Clear;
            this.pnlTexts.Controls.Add(this.txtCol2);
            this.pnlTexts.Controls.Add(this.txtCol1);
            this.pnlTexts.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.pnlTexts.Location = new System.Drawing.Point(2, 2);
            this.pnlTexts.Name = "pnlTexts";
            this.pnlTexts.Size = new System.Drawing.Size(651, 28);
            this.pnlTexts.TabIndex = 1;
            // 
            // txtCol2
            // 
            this.txtCol2.IsAutoComplete = false;
            this.txtCol2.Location = new System.Drawing.Point(172, 1);
            this.txtCol2.Name = "txtCol2";
            this.txtCol2.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.Yes;
            this.txtCol2.Size = new System.Drawing.Size(166, 26);
            this.txtCol2.TabIndex = 2;
            this.txtCol2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtCol1
            // 
            this.txtCol1.IsAutoComplete = false;
            this.txtCol1.Location = new System.Drawing.Point(0, 1);
            this.txtCol1.Name = "txtCol1";
            this.txtCol1.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.Yes;
            this.txtCol1.Size = new System.Drawing.Size(166, 26);
            this.txtCol1.TabIndex = 2;
            this.txtCol1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // frmComboBoxItems
            // 
            this.Controls.Add(this.grdComboData);
            this.Controls.Add(this.pnlTexts);
            this.DockPadding.All = 2;
            this.FormStyle = Gizmox.WebGUI.Forms.FormStyle.Application;
            this.Padding = new Gizmox.WebGUI.Forms.Padding(2);
            this.Size = new System.Drawing.Size(655, 466);
            this.StartPosition = Gizmox.WebGUI.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmComboBoxItems_Load);
            this.Resize += new System.EventHandler(this.frmComboBoxItems_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grdComboData)).EndInit();
            this.pnlTexts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.DataGridView grdComboData;
        private Gizmox.WebGUI.Forms.Panel pnlTexts;
        private Gizmox.WebGUI.Forms.DataGridViewTextBoxColumn objCol1;
        private Gizmox.WebGUI.Forms.DataGridViewTextBoxColumn objCol2;
        private Controls.UI.TextBox txtCol2;
        private Controls.UI.TextBox txtCol1;
    }
}
