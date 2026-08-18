using StarNet.Logging;
using StarNet.Services.Contracts;
using StarNet.Services.Core;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace StarNet.Services
{
    public partial class BatchService : IBatchService
    {
        private Logger _logger;

        private static readonly ConcurrentDictionary<string, IBatchStatus> _results
            = new ConcurrentDictionary<string, IBatchStatus>();

        private static readonly ConcurrentDictionary<string, string> _calceledTaskIds
            = new ConcurrentDictionary<string, string>();



        public BatchService()
        {
            _logger = this.InitLogger();
        }

        public void FinishTask(string clientTaskId)
        {
            _logger.LogDebug($"Finishing task with ID [{clientTaskId}]");

            IBatchStatus status;
            _results.TryRemove(clientTaskId, out status);

            if (IsTaskCanceled(clientTaskId))
            {
                ClearCanceledTask(clientTaskId);
            }
        }

        public void CancelTask(string clientTaskId)
        {
            _logger.LogDebug($"Canceling task with ID [{clientTaskId}]");

            IBatchStatus status;

            if (_results.TryGetValue(clientTaskId, out status))
            {
                _calceledTaskIds.TryAdd(clientTaskId, null);
            }
        }



        private IBatchStatus InitializeBatchStatus(
            IBatchStatus status,
            string taskId)
        {
            status.ClientTaskId = taskId;
            status.TotalItems = 0;
            status.CurrentItemProcessing = 0;
            status.IsStarted = false;
            status.IsFinished = false;
            status.StatusId = 0;

            return status;
        }

        private IBatchStatus GetStatus(string clientTaskId)
        {
            IBatchStatus status;

            if (_results.TryGetValue(clientTaskId, out status))
            {
                return status;
            }

            return null;
        }

        private List<IBatchStatus> GetAllStatuses()
        {
            return _results?.Values?.ToList();
        }

        private bool IsTaskCanceled(string clientTaskId)
        {
            string value;
            return _calceledTaskIds.TryGetValue(clientTaskId, out value);
        }

        private void ClearCanceledTask(string clientTaskId)
        {
            _logger.LogDebug($"Clearing canceled task with ID [{clientTaskId}]");

            string value;
            _calceledTaskIds.TryRemove(clientTaskId, out value);
        }

        private IBatchStatus UpdateStatus(string clientId, IBatchStatus status)
        {
            return _results.AddOrUpdate(clientId, status, (key, oldValue) => status);
        }

    }
}
