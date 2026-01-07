using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common;

namespace StarNet.Forms.Admin
{
    partial class frmRecoverPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecoverPassword));
            this.lblCaption = new StarNet.Controls.UI.Label();
            this.txtUsername = new StarNet.Controls.UI.TextBox();
            this.pnlUsername = new Gizmox.WebGUI.Forms.Panel();
            this.lblMessage = new StarNet.Controls.UI.Label();
            this.pnlBase.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlUsername.SuspendLayout();
            this.SuspendLayout();
            // 
            // errWarning
            // 
            this.errWarning.BlinkStyle = Gizmox.WebGUI.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
            // 
            // errError
            // 
            this.errError.BlinkStyle = Gizmox.WebGUI.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.lblMessage);
            this.pnlBase.Controls.Add(this.pnlUsername);
            this.pnlBase.Controls.Add(this.lblCaption);
            resources.ApplyResources(this.pnlBase, "pnlBase");
            // 
            // pnlActions
            // 
            resources.ApplyResources(this.pnlActions, "pnlActions");
            // 
            // lblCaption
            // 
            this.lblCaption.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            resources.ApplyResources(this.lblCaption, "lblCaption");
            this.lblCaption.Name = "lblCaption";
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
            // 
            // pnlUsername
            // 
            this.pnlUsername.Controls.Add(this.txtUsername);
            this.pnlUsername.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            resources.ApplyResources(this.pnlUsername, "pnlUsername");
            this.pnlUsername.Name = "pnlUsername";
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = Gizmox.WebGUI.Forms.DockStyle.Top;
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Name = "lblMessage";
            // 
            // frmRecoverPassword
            // 
            this.FormBorderStyle = Gizmox.WebGUI.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            resources.ApplyResources(this, "$this");
            this.Controls.SetChildIndex(this.lblLockedMessage, 0);
            this.Controls.SetChildIndex(this.pnlBase, 0);
            this.pnlBase.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlUsername.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        internal Controls.UI.Label lblCaption;
        internal Controls.UI.TextBox txtUsername;
        internal Controls.UI.Label lblMessage;
        private Panel pnlUsername;
    }
}