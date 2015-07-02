using KModel.Entity;
using KModel.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private KBaseEntities _context;

        public HouseRepository(KBaseEntities context)
        {
            _context = context;
        }

        public House GetByDeviceId(int deviceId)
        {
            return _context.DeviceHouse.Where(p => p.Device == deviceId).Select(p => p.House1).SingleOrDefault();
        }

        public IEnumerable<House> GetListByUserId(string id)
        {
            return _context.HouseUser
                .Where(p => p.UserProfile == id)
                .Select(p => p.House1);
        }

        public House GetBySensorId(int id)
        {
            return (from sd in _context.SensorDry
                    where sd.Id == id
                    join dh in _context.DeviceHouse on sd.Device equals dh.Device
                    select dh.House1).SingleOrDefault();
        }
    }
}
