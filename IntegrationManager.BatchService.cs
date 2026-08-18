using System;
using System.Collections.Generic;
using System.Linq;
using StarNet.Logging;
using StarNet.Services.Client.StarNetAPI.WCF.BatchService;

namespace StarNet.Services.Client
{
    public static partial class IntegrationManager
    {
        public static void FinishTask(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    proxy.FinishTask(clientTaskId);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static void CancelTask(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    proxy.CancelTask(clientTaskId);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }


        #region Invoice10Create

        public static Invoice10CreateBatchStatus CreateNewInvoice10CreateBatchTask(
            Logger logger = null)
        {
            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.CreateNewInvoice10CreateBatchTask();

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static Invoice10CreateBatchStatus GetInvoice10CreateStatus(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: BatchTaskType = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.GetInvoice10CreateStatus(clientTaskId);

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static void StartInvoice10CreateTask(
            string clientTaskId,
            int userId,
            Model.Nesicha.Havad.HavadCentralInvoice havadCentralInvoiceInstance,
            List<Model.Nesicha.Havad.HavadCentralInvoiceItem> items,
            Logger logger = null)
        {
            try
            {
                //using "using" causes wait for operation compelition

                logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

                var request = new Invoice10CreateBatchTaskRequest();
                request.ClientTaskId = clientTaskId;
                request.UserId = userId;
                request.VendorId = havadCentralInvoiceInstance.Invoice.VendorId.Value;
                request.InvoiceNumber = havadCentralInvoiceInstance.Invoice.InvoiceNumber.Value;
                request.InvoiceCreatedDate = havadCentralInvoiceInstance.Invoice.InvoiceCreatedDate.Value;
                request.CentralInvoiceCalculationTypeId = havadCentralInvoiceInstance.CentralInvoiceCalculationTypeId.Value;
                request.NumberOfHours = havadCentralInvoiceInstance.NumberOfHours;
                request.NumberOfCases = havadCentralInvoiceInstance.NumberOfCases;
                request.AmountBeforeVat = havadCentralInvoiceInstance.AmountBeforeVat.Value;
                request.VatAmount = havadCentralInvoiceInstance.VatAmount.Value;
                request.TotalAmount = havadCentralInvoiceInstance.TotalAmount.Value;
                request.VendorComments = havadCentralInvoiceInstance.VendorComments;

                request.Items = items
                    .Select(x =>
                        new Invoice10CreateBatchTaskInvoiceItem
                        {
                            InvoiceId = x.InvoiceId,
                            NumberOfHours = x.NumberOfHours,
                            AmountBeforeVat = x.AmountBeforeVat
                        })
                    .ToArray();

                var proxy = new BatchServiceClient("BatchService");
                proxy.StartInvoice10CreateTaskAsync(request);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        #endregion Invoice10Create

        #region Invoice10Cancel

        public static Invoice10CancelBatchStatus CreateNewInvoice10CancelBatchTask(
            Logger logger = null)
        {
            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.CreateNewInvoice10CancelBatchTask();

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static Invoice10CancelBatchStatus GetInvoice10CancelStatus(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: BatchTaskType = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.GetInvoice10CancelStatus(clientTaskId);

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static void StartInvoice10CancelTask(
            string clientTaskId,
            int userId,
            int invoiceId,
            Logger logger = null)
        {
            try
            {
                //using "using" causes wait for operation compelition

                logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

                var request = new Invoice10CancelBatchTaskRequest();
                request.ClientTaskId = clientTaskId;
                request.UserId = userId;
                request.InvoiceId = invoiceId;

                var proxy = new BatchServiceClient("BatchService");
                proxy.StartInvoice10CancelTaskAsync(request);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        #endregion Invoice10Cancel

        #region Invoice10SendToCompany

        public static Invoice10SendToCompanyBatchStatus CreateNewInvoice10SendToCompanyBatchTask(
            Logger logger = null)
        {
            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.CreateNewInvoice10SendToCompanyBatchTask();

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static Invoice10SendToCompanyBatchStatus GetInvoice10SendToCompanyStatus(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: BatchTaskType = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.GetInvoice10SendToCompanyStatus(clientTaskId);

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static void StartInvoice10SendToCompanyTask(
            string clientTaskId,
            int userId,
            int invoiceId,
            Logger logger = null)
        {
            try
            {
                //using "using" causes wait for operation compelition

                logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

                var request = new Invoice10SendToCompanyBatchTaskRequest();
                request.ClientTaskId = clientTaskId;
                request.UserId = userId;
                request.InvoiceId = invoiceId;

                var proxy = new BatchServiceClient("BatchService");
                proxy.StartInvoice10SendToCompanyTaskAsync(request);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        #endregion Invoice10SendToCompany

        #region Invoice10Confirm

        public static Invoice10ConfirmBatchStatus CreateNewInvoice10ConfirmBatchTask(
            Logger logger = null)
        {
            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.CreateNewInvoice10ConfirmBatchTask();

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static Invoice10ConfirmBatchStatus GetInvoice10ConfirmStatus(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: BatchTaskType = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.GetInvoice10ConfirmStatus(clientTaskId);

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static void StartInvoice10ConfirmTask(
            string clientTaskId,
            int userId,
            int invoiceId,
            Logger logger = null)
        {
            try
            {
                //using "using" causes wait for operation compelition

                logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

                var request = new Invoice10ConfirmBatchTaskRequest();
                request.ClientTaskId = clientTaskId;
                request.UserId = userId;
                request.InvoiceId = invoiceId;

                var proxy = new BatchServiceClient("BatchService");
                proxy.StartInvoice10ConfirmTaskAsync(request);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        #endregion Invoice10Confirm

        #region DownloadFiles

        public static DownloadFilesBatchStatus CreateDownloadFilesTask(
            Logger logger = null)
        {
            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.CreateDownloadFilesTask();

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static DownloadFilesBatchStatus GetDownloadFilesStatus(
            string clientTaskId,
            Logger logger = null)
        {
            logger?.LogDebug($"request parameters: BatchTaskType = {clientTaskId}");

            try
            {
                using (var proxy = new BatchServiceClient("BatchService"))
                {
                    var response = proxy.GetDownloadFilesStatus(clientTaskId);

                    logger?.LogDebug($"response parameters: ClientTaskId = {response?.ClientTaskId}");

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static void StartDownloadFilesTask(
            string clientTaskId,
            int userId,
            int invoiceId,
            List<int> documentIds,
            Logger logger = null)
        {
            try
            {
                //using "using" causes wait for operation compelition

                logger?.LogDebug($"request parameters: ClientTaskId = {clientTaskId}");

                var request = new DownloadFilesBatchTaskRequest();
                request.ClientTaskId = clientTaskId;
                request.UserId = userId;
                request.InvoiceId = invoiceId;
                request.DocumentIds = documentIds.ToArray();

                var proxy = new BatchServiceClient("BatchService");
                proxy.StartDownloadFilesTask(request);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex);
                throw;
            }
        }

        public static RetreiveDocumentsByIdsResponse CompleteDownloadFilesTask(
            string clientTaskId,
            Logger logger = null)
        {
            var retval = new RetreiveDocumentsByIdsResponse();

            using (var proxy = new BatchServiceClient("StreamedBatchService"))
            {
                var iProxy = proxy as IBatchService;
                var request = new CompleteDownloadFilesRequest();
                request.ClientTaskId = clientTaskId;

                var response = iProxy.CompleteDownloadFilesTask(request);

                retval.DownloadBuffer = GetFileByteArray(response.FileByteStream);
                retval.DownloadFileName = response.FileName;

                return retval;
            }
        }

        #endregion DownloadFiles
    }
}
