using StarNet.Bl;
using StarNet.Common;
using StarNet.Model.Enums;
using StarNet.Model.Enums.Nesicha;
using StarNet.Model.Nesicha;
using StarNet.Model.Nesicha.Havad;
using StarNet.ResourceFiles;
using StarNet.Services.Contracts;
using StarNet.Services.Contracts.DataContracts.BatchService;
using StarNet.Services.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StarNet.Services
{
    public partial class BatchService
    {
        public DownloadFilesBatchStatus CreateDownloadFilesTask()
        {
            var taskId = Guid.NewGuid().ToString();

            _logger.LogDebug($"New task with ID [{taskId}]");

            IBatchStatus status = new DownloadFilesBatchStatus();
            InitializeBatchStatus(status, taskId);
            UpdateStatus(taskId, status);

            return status as DownloadFilesBatchStatus;
        }

        public DownloadFilesBatchStatus GetDownloadFilesStatus(string clientTaskId)
        {
            var status = GetStatus(clientTaskId);

            if (status != null)
            {
                return status as DownloadFilesBatchStatus;
            }

            return null;
        }

        public void StartDownloadFilesTask(DownloadFilesBatchTaskRequest request)
        {
            if (request == null)
            {
                _logger.LogDebug("ERROR - Request is null");
                return;
            }

            var dbLogger = new IntegrationLogWriter(
                request.UserId,
                IntegrationServiceTypeEnum.DownloadFilesBatchTask,
                request.InvoiceId,
                null,
                null);

            try
            {
                _logger.LogDebug($"Start task with ID [{request.ClientTaskId}]");

                //get status object
                var status = GetDownloadFilesStatus(request.ClientTaskId);

                try
                {
                    //_logger.LogDebug($"Starting task with ID [{request.ClientTaskId}] by user [{request.UserId}]");

                    dbLogger.RequestLog.AppendLine(MethodBase.GetCurrentMethod().Name);
                    dbLogger.RequestLog.AppendLine(Common.Utils.GetUnspecifiedDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss"));
                    dbLogger.RequestLog.AppendLine();
                    dbLogger.RequestLog.AppendJSONObject(request);

                    //set status to Started
                    status.UserId = request.UserId;
                    status.InvoiceId = request.InvoiceId ?? 0;
                    status.IsStarted = true;
                    status.StartTime = Common.Utils.GetUnspecifiedDateTimeNow();
                    UpdateStatus(request.ClientTaskId, status);

                    if (request.DocumentIds == null)
                    {
                        throw new Exception("DocumentIds is null");
                    }

                    var documents = new List<Document>(0);

                    using (var unitOfWork = new UnitOfWork())
                    {
                        documents = unitOfWork.NesichaBl
                            .GetDocuments(
                                x => request.DocumentIds.Contains(x.Id)
                                    && !x.DeleteDate.HasValue,
                                null,
                                x => x.DocType)
                            .ToList();
                    }

                    status.TotalItems = documents.Count();
                    dbLogger.ResponseLog.AppendLine($"Dowloading {status.TotalItems} files");
                    dbLogger.ResponseLog.AppendLine(Common.Utils.GetUnspecifiedDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss"));

                    status.FileName = $"files_{DateTime.Today.ToString("ddMMyyyy")}.zip";
                    status.FilePath = Path.Combine(Config.DocumentCachedRoot, System.Guid.NewGuid().ToString() + ".zip");

                    var fileServices = new FileServices();
                    var downloadFilesRequest = new DownloadFilesRequest();
                    downloadFilesRequest.InvoiceId = request.InvoiceId;
                    downloadFilesRequest.InvoiceNumber = request.InvoiceNumber;
                    downloadFilesRequest.InvoiceType = request.InvoiceType;
                    downloadFilesRequest.UserId = request.UserId;

                    foreach (var document in documents)
                    {
                        if (IsTaskCanceled(request.ClientTaskId))
                        {
                            break;
                        }

                        status.CurrentItemProcessing++;
                        UpdateStatus(request.ClientTaskId, status);

                        var filePath = fileServices.GetDocumentFilePath(downloadFilesRequest, document);

                        if (filePath == null)
                        {
                            dbLogger.ResponseLog.AppendLine($"Download failed for document ID {document.Id} ({status.CurrentItemProcessing}/{status.TotalItems})");
                            dbLogger.ResponseLog.AppendLine(Common.Utils.GetUnspecifiedDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss"));
                            continue;
                        }

                        var fileName = document.FileName;

                        if (document.DocType != null)
                        {
                            fileName = $"{Core.Utils.RemoveInvalidFileNameChars(document.DocType.Description)}_{fileName}";
                        }

                        ZipUtils.ZipUtils.AddFileToZipArchive(status.FilePath, filePath, fileName);

                        dbLogger.ResponseLog.AppendLine($"Downloaded file {status.CurrentItemProcessing}/{status.TotalItems}");
                        dbLogger.ResponseLog.AppendLine(Common.Utils.GetUnspecifiedDateTimeNow().ToString("dd/MM/yyyy HH:mm:ss"));
                    }

                    if (IsTaskCanceled(request.ClientTaskId))
                    {
                        status.IsCanceled = true;
                        ClearCanceledTask(request.ClientTaskId);
                        dbLogger.ResponseLog.AppendLine("Task is canceled");

                        ClearFiles(status?.FilePath);
                    }

                    status.IsFinished = true;
                    status.FinishedTime = Common.Utils.GetUnspecifiedDateTimeNow();
                    UpdateStatus(request.ClientTaskId, status);
                }
                catch (Exception ex)
                {
                    status.HasErrors = true;
                    status.IsFinished = true;
                    status.FinishedTime = Common.Utils.GetUnspecifiedDateTimeNow();
                    status.ErrorMessage = ex.Message;
                    UpdateStatus(request.ClientTaskId, status);
                    ClearFiles(status?.FilePath);

                    throw;
                }
            }
            catch (Exception ex)
            {
                dbLogger.SetStatus(IntegrationLogStatusEnum.RunTimeError);
                dbLogger.ErrorLog.AppendLine(ex.ToString());

                _logger.LogDebug($"{MethodBase.GetCurrentMethod().Name}. Run-Time error");
                _logger.LogError(ex);
            }
            finally
            {
                dbLogger.Flush();
            }
        }

        public RemoteFileInfo CompleteDownloadFilesTask(CompleteDownloadFilesRequest request)
        {
            try
            {
                var result = new RemoteFileInfo();
                var status = GetDownloadFilesStatus(request.ClientTaskId);

                if (status == null)
                {
                    throw new Exception("Status not found");
                }

                if (status.IsCanceled)
                {
                    throw new Exception("Task was cancelled");
                }

                if (!status.IsFinished)
                {
                    throw new Exception("Task hasn't finished yet");
                }

                result.FileName = status.FileName;

                var fileInfo = new FileInfo(status.FilePath);
                result.Length = fileInfo.Length;

                var stream = new FileStream(
                    status.FilePath,
                    FileMode.Open,
                    FileAccess.Read);

                result.FileByteStream = stream;

                FinishTask(request.ClientTaskId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Run-Time error. ", ex);
                throw;
            }
        }

        private void ClearFiles(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch { }
        }

    }
}
