using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace StarNet.Core.Forms.Actions
{
    partial class frmDownloadFilesAsync
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

        #region Visual WebGui Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrProgress = new Gizmox.WebGUI.Forms.Timer(this.components);
            this.lblTaskId = new StarNet.Controls.UI.Label();
            this.txtTaskId = new StarNet.Controls.UI.TextBox();
            this.txtExecutionDate = new StarNet.Controls.UI.TextBox();
            this.lblExecutionDate = new StarNet.Controls.UI.Label();
            this.pnlTaskDetails = new Gizmox.WebGUI.Forms.Panel();
            this.lblDownloadingFile = new StarNet.Controls.UI.Label();
            this.pnlLabels = new Gizmox.WebGUI.Forms.Panel();
            this.lblFinished = new StarNet.Controls.UI.Label();
            this.objHtmlBox = new Gizmox.WebGUI.Forms.HtmlBox();
            this.pnlWorkspace.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlTaskDetails.SuspendLayout();
            this.pnlLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWorkspace
            // 
            this.pnlWorkspace.Controls.Add(this.pnlLabels);
            this.pnlWorkspace.Controls.Add(this.pnlTaskDetails);
            this.pnlWorkspace.Size = new System.Drawing.Size(741, 402);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(637, 8);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 402);
            this.pnlButtons.Size = new System.Drawing.Size(741, 57);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(537, 8);
            // 
            // tmrProgress
            // 
            this.tmrProgress.Interval = 1000;
            this.tmrProgress.Tick += new System.EventHandler(this.tmrProgress_Tick);
            // 
            // lblTaskId
            // 
            this.lblTaskId.ForeColor = System.Drawing.Color.Black;
            this.lblTaskId.Input = null;
            this.lblTaskId.IsMandatory = false;
            this.lblTaskId.Location = new System.Drawing.Point(9, 3);
            this.lblTaskId.MandatoryColor = System.Drawing.Color.Red;
            this.lblTaskId.Name = "lblTaskId";
            this.lblTaskId.Size = new System.Drawing.Size(87, 26);
            this.lblTaskId.TabIndex = 3;
            this.lblTaskId.Text = "מזהה ריצה:";
            this.lblTaskId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTaskId
            // 
            this.txtTaskId.IsAutoComplete = false;
            this.txtTaskId.Location = new System.Drawing.Point(99, 3);
            this.txtTaskId.Name = "txtTaskId";
            this.txtTaskId.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.Yes;
            this.txtTaskId.Size = new System.Drawing.Size(476, 26);
            this.txtTaskId.TabIndex = 0;
            // 
            // txtExecutionDate
            // 
            this.txtExecutionDate.IsAutoComplete = false;
            this.txtExecutionDate.Location = new System.Drawing.Point(99, 35);
            this.txtExecutionDate.Name = "txtExecutionDate";
            this.txtExecutionDate.RightToLeft = Gizmox.WebGUI.Forms.RightToLeft.Yes;
            this.txtExecutionDate.Size = new System.Drawing.Size(476, 26);
            this.txtExecutionDate.TabIndex = 1;
            // 
            // lblExecutionDate
            // 
            this.lblExecutionDate.ForeColor = System.Drawing.Color.Black;
            this.lblExecutionDate.Input = null;
            this.lblExecutionDate.IsMandatory = false;
            this.lblExecutionDate.Location = new System.Drawing.Point(9, 35);
            this.lblExecutionDate.MandatoryColor = System.Drawing.Color.Red;
            this.lblExecutionDate.Name = "lblExecutionDate";
            this.lblExecutionDate.Size = new System.Drawing.Size(87, 26);
            this.lblExecutionDate.TabIndex = 3;
            this.lblExecutionDate.Text = "תאריך ריצה:";
            this.lblExecutionDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTaskDetails
            // 
            this.pnlTaskDetails.Controls.Add(this.lblTaskId);
            this.pnlTaskDetails.Controls.Add(this.txtTaskId);
            this.pnlTaskDetails.Controls.Add(this.txtExecutionDate);
            this.pnlTaskDetails.Controls.Add(this.lblExecutionDate);
            this.pnlTaskDetails.Dock = Gizmox.WebGUI.Forms.DockStyle.Bottom;
            this.pnlTaskDetails.Location = new System.Drawing.Point(0, 337);
            this.pnlTaskDetails.Name = "pnlTaskDetails";
            this.pnlTaskDetails.Size = new System.Drawing.Size(741, 65);
            this.pnlTaskDetails.TabIndex = 1;
            // 
            // lblDownloadingFile
            // 
            this.lblDownloadingFile.AutoSize = true;
            this.lblDownloadingFile.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.lblDownloadingFile.ForeColor = System.Drawing.Color.Black;
            this.lblDownloadingFile.Input = null;
            this.lblDownloadingFile.IsMandatory = false;
            this.lblDownloadingFile.Location = new System.Drawing.Point(20, 10);
            this.lblDownloadingFile.MandatoryColor = System.Drawing.Color.Red;
            this.lblDownloadingFile.Name = "lblDownloadingFile";
            this.lblDownloadingFile.Size = new System.Drawing.Size(71, 13);
            this.lblDownloadingFile.TabIndex = 3;
            this.lblDownloadingFile.Text = "מוריד מסמך";
            this.lblDownloadingFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLabels
            // 
            this.pnlLabels.Controls.Add(this.lblFinished);
            this.pnlLabels.Controls.Add(this.lblDownloadingFile);
            this.pnlLabels.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.pnlLabels.DockPadding.Bottom = 10;
            this.pnlLabels.DockPadding.Left = 20;
            this.pnlLabels.DockPadding.Right = 20;
            this.pnlLabels.DockPadding.Top = 10;
            this.pnlLabels.Location = new System.Drawing.Point(0, 0);
            this.pnlLabels.Name = "pnlLabels";
            this.pnlLabels.Padding = new Gizmox.WebGUI.Forms.Padding(20, 10, 20, 10);
            this.pnlLabels.Size = new System.Drawing.Size(741, 337);
            this.pnlLabels.TabIndex = 0;
            // 
            // lblFinished
            // 
            this.lblFinished.AutoSize = true;
            this.lblFinished.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            this.lblFinished.ForeColor = System.Drawing.Color.Black;
            this.lblFinished.Input = null;
            this.lblFinished.IsMandatory = false;
            this.lblFinished.Location = new System.Drawing.Point(20, 23);
            this.lblFinished.MandatoryColor = System.Drawing.Color.Red;
            this.lblFinished.Name = "lblFinished";
            this.lblFinished.Size = new System.Drawing.Size(84, 13);
            this.lblFinished.TabIndex = 3;
            this.lblFinished.Text = "תהליך הסתיים";
            this.lblFinished.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // objHtmlBox
            // 
            this.objHtmlBox.ContentType = "text/html";
            this.objHtmlBox.Html = "<HTML>No content.</HTML>";
            this.objHtmlBox.Location = new System.Drawing.Point(-1000, -1000);
            this.objHtmlBox.Name = "objHtmlBox";
            this.objHtmlBox.Size = new System.Drawing.Size(10, 10);
            this.objHtmlBox.TabIndex = 4;
            // 
            // frmDownloadFilesAsync
            // 
            this.Controls.Add(this.objHtmlBox);
            this.Size = new System.Drawing.Size(741, 459);
            this.Text = "הורדת מסמכים";
            this.Load += new System.EventHandler(this.frmDownloadFilesAsync_Load);
            this.RegisteredTimers = new Gizmox.WebGUI.Forms.Timer[] {
        this.tmrProgress};
            this.Controls.SetChildIndex(this.pnlButtons, 0);
            this.Controls.SetChildIndex(this.pnlWorkspace, 0);
            this.Controls.SetChildIndex(this.objHtmlBox, 0);
            this.pnlWorkspace.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlTaskDetails.ResumeLayout(false);
            this.pnlLabels.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        private Timer tmrProgress;
        private Panel pnlTaskDetails;
        private StarNet.Controls.UI.Label lblTaskId;
        internal StarNet.Controls.UI.TextBox txtTaskId;
        internal StarNet.Controls.UI.TextBox txtExecutionDate;
        private StarNet.Controls.UI.Label lblExecutionDate;
        private Panel pnlLabels;
        private StarNet.Controls.UI.Label lblDownloadingFile;
        private StarNet.Controls.UI.Label lblFinished;
        protected HtmlBox objHtmlBox;
    }
}