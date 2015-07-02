using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository.Interface
{
    public interface IReportRepository
    {
        IEnumerable<Report> GetReports();
        IEnumerable<ReasonForDisarming> GetReasonForDisarmingReport(DateTime StartDate, DateTime EndDate, List<int> housesId);
    }
}
