using KModel.Entity;
using KModel.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository
{
    public class ReportRepository : IReportRepository
    {
        private KBaseEntities _context;

        public ReportRepository(KBaseEntities context)
        {
            _context = context;
        }

        public IEnumerable<Report> GetReports()
        {
            return _context.Report;
        }

        public IEnumerable<ReasonForDisarming> GetReasonForDisarmingReport(DateTime StartDate, DateTime EndDate, List<int> housesId)
        {
            IEnumerable<ReasonForDisarming> query = from rfd in _context.ReasonForDisarming
                                                   join sd in _context.SensorDry on rfd.SensorDry equals sd.Id
                                                   join dh in _context.DeviceHouse on sd.Device equals dh.Device
                                                   join houses in housesId on dh.House equals houses
                                                   where rfd.Date >= StartDate && rfd.Date <= EndDate
                                                   select rfd;
            return query;
        }
    }
}
