using Gizmox.WebGUI.Common.Gateways;
using Gizmox.WebGUI.Forms;
using StarNet.Controls.UI;
using StarNet.Forms.Core;
using StarNet.ResourceFiles;
using StarNet.Services.Client;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace StarNet.Core.Forms.Actions
{
    public partial class frmDownloadFilesAsync : frmBaseActionForm
    {
        private List<int> _documentIds;
        private Dictionary<string, int> _workingIndicatorCounter = null;
        private byte[] _downloadBuffer = null;
        private string _downloadFileName = "";

        public string TaskId { get; private set; }

        public frmDownloadFilesAsync(int invoiceId, List<int> documentIds)
            : base(invoiceId)
        {
            InitializeComponent();

            _documentIds = documentIds;
        }

        private void EnableControls()
        {
            txtTaskId.ReadOnly = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IntegrationManager.CancelTask(TaskId);

            tmrProgress.Stop();
            tmrProgress.Enabled = false;
        }

        private void frmDownloadFilesAsync_Load(object sender, EventArgs e)
        {
            EnableControls();

            lblFinished.Visible = false;

            lblDownloadingFile.SetFontStyle(FontStyle.Bold);
            lblFinished.SetFontStyle(FontStyle.Bold);

            DownloadFiles();
        }

        private void DownloadFiles()
        {
            _workingIndicatorCounter = new Dictionary<string, int>(10);

            tmrProgress.Start();
            tmrProgress.Enabled = true;

            var task = IntegrationManager.CreateDownloadFilesTask();
            TaskId = task.ClientTaskId;
            txtTaskId.Text = TaskId;
            txtExecutionDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            IntegrationManager.StartDownloadFilesTask(
                TaskId,
                SessionCache.CurrentUser.Id,
                InvoiceId,
                _documentIds);
        }

        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            try
            {
                var status = IntegrationManager.GetDownloadFilesStatus(TaskId);

                if (status.IsFinished)
                {
                    tmrProgress.Stop();
                    tmrProgress.Enabled = false;

                    var response = IntegrationManager.CompleteDownloadFilesTask(TaskId);

                    _downloadBuffer = response.DownloadBuffer;
                    _downloadFileName = response.DownloadFileName;

                    objHtmlBox.Url = (new GatewayReference(this, "DownloadSingleFile")).ToString();

                    lblFinished.Visible = true;

                    if (status.HasErrors)
                    {
                        lblFinished.ForeColor = Color.OrangeRed;
                        lblFinished.Text = General.DownloadFiles_Failed;

                        if (!string.IsNullOrEmpty(status.ErrorMessage))
                        {
                            lblFinished.Text += $" - {status.ErrorMessage}";
                        }
                    }
                    else
                    {
                        lblFinished.ForeColor = Color.Green;
                        lblFinished.Text = General.DownloadFiles_Finished;
                    }

                    this.DialogResult = DialogResult.OK;
                    Focus();
                }

                lblDownloadingFile.Text = $"{General.DownloadingFile} ({status.TotalItems} / {status.CurrentItemProcessing}) {AddWorkingIndicator("DownloadingFile")}";
            }
            catch
            {
                tmrProgress.Stop();
                tmrProgress.Enabled = false;

                throw;
            }
        }

        private string AddWorkingIndicator(string key)
        {
            if (!_workingIndicatorCounter.ContainsKey(key))
            {
                _workingIndicatorCounter.Add(key, 1);
            }

            if (_workingIndicatorCounter[key] == 4)
            {
                _workingIndicatorCounter[key] = 1;
            }

            return new string('.', _workingIndicatorCounter[key]++);
        }

        protected override Gizmox.WebGUI.Common.Interfaces.IGatewayHandler ProcessGatewayRequest(
            Gizmox.WebGUI.Hosting.HostContext objHostContext,
            string strAction)
        {
            if (strAction == "DownloadSingleFile")
            {
                // make sure no caching takes place.
                objHostContext.Response.Expires = -1;
                objHostContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                objHostContext.Response.CacheControl = "no-cache";
                objHostContext.Response.AddHeader("Pragma", "no-cache");

                var urlFileName = System.Web.HttpUtility.UrlPathEncode(_downloadFileName);
                urlFileName = urlFileName.Replace(',', '.');

                objHostContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + urlFileName);

                // build your output and set content type
                objHostContext.Response.ContentType = System.Web.MimeMapping.GetMimeMapping(_downloadFileName);
                objHostContext.Response.BinaryWrite(_downloadBuffer);
                objHostContext.Response.Flush(); // Sends all currently buffered output to the client.                  
                System.Web.HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                return null;
            }
            else
            {
                return base.ProcessGatewayRequest(objHostContext, strAction);
            }
        }

    }
}