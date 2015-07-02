using KModel.Entity;
using KModel.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository
{
    public class SensorDryRepository : ISensorDryRepository
    {
        private KBaseEntities _context;

        public SensorDryRepository(KBaseEntities context)
        {
            _context = context;
        }

        public SensorDry GetById(int id)
        {
            return _context.SensorDry.Where(p => p.Id == id).SingleOrDefault();
        }

        public IEnumerable<SensorDry> GetListByHouseId(int id)
        {
            IEnumerable<SensorDry> query = from dh in _context.DeviceHouse
                                          where dh.House == id
                                          join sd in _context.SensorDry on dh.Device equals sd.Device
                                          select sd;
            return query;
        }

        public bool SetReasonForDisarming(int SensorDryId, string Text, string userId)
        {
            ReasonForDisarming model = new ReasonForDisarming();
            model.SensorDry = SensorDryId;
            model.Reason = Text;
            model.Date = DateTime.Now;
            model.UserProfile = userId;
            _context.ReasonForDisarming.Add(model);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch { return false; };
        }

        public bool SensorDryUpdateStatus(int SensorDryId, int status)
        {
            SensorDry model = _context.SensorDry.Where(p => p.Id == SensorDryId).SingleOrDefault();
            try
            {
                model.Status = status;
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public SensorDryData GetLastDataBySensorId(int id)
        {
            return _context.SensorDryData.Where(p => p.SensorId == id)
                .DefaultIfEmpty()
                .OrderByDescending(p => p.Id)
                .First();
        }

        public int GetWarningSensorByHouseId(int id)
        {
            IEnumerable<SensorDryData> query = from h in _context.House
                                              where h.Id == id
                                              join dh in _context.DeviceHouse on h.Id equals dh.House
                                              join sd in _context.SensorDry on dh.Device equals sd.Device
                                              join sdd in _context.SensorDryData on sd.Id equals sdd.SensorId
                                              select sdd;
            if (query.Any())
                return query.OrderByDescending(p => p.Id).FirstOrDefault().Data;
            else
                return 0;
        }


        public IEnumerable<SensorDryData> GetDataListForPeriodByUserId(string id, DateTime startDate)
        {
            IEnumerable<SensorDryData> query = from h in _context.HouseUser
                                              where h.UserProfile == id
                                              join dh in _context.DeviceHouse on h.House equals dh.House
                                              join sd in _context.SensorDry on dh.Device equals sd.Device
                                              join sdd in _context.SensorDryData on sd.Id equals sdd.SensorId
                                              where sdd.Date > startDate
                                              orderby sdd.Date descending
                                              select sdd;
            return query;
        }
    }
}
