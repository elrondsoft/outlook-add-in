using System.Collections.Generic;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;

namespace Helios.Api.Utils.Sync.Comparer.Data
{
    public class TasksComparerResult
    {
        public IList<HeliosTask> HeliosTasksToCreate { get; set; }
        public IList<HeliosTask> HeliosTasksToUpdate { get; set; }
        public IList<HeliosTask> HeliosTasksToDelete { get; set; }

        public IList<OutlookTask> OutlookTasksToCreate { get; set; }
        public IList<OutlookTask> OutlookTasksToUpdate { get; set; }
        public IList<OutlookTask> OutlookTasksToDelete { get; set; }

        public TasksComparerResult()
        {
            HeliosTasksToCreate = new List<HeliosTask>();
            HeliosTasksToUpdate = new List<HeliosTask>();
            HeliosTasksToDelete = new List<HeliosTask>();

            OutlookTasksToCreate = new List<OutlookTask>();
            OutlookTasksToUpdate = new List<OutlookTask>();
            OutlookTasksToDelete = new List<OutlookTask>();
        }
    }
}
