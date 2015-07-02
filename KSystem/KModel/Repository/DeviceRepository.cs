using KModel.Entity;
using KModel.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private KBaseEntities _context;

        public DeviceRepository(KBaseEntities context)
        {
            _context = context;
        }

        public int GetLastNetworkStatusByDeviceId(int deviceId)
        {
            IEnumerable<NetworkStatus> result = _context.NetworkStatus.Where(p => p.Device == deviceId);
            if (result.Any())
                return result.OrderByDescending(p => p.Id).Take(1).Single().Status;
            else
                return 0;
        }

        public IEnumerable<NetworkStatus> GetNetworkStatusForPeriodByUserId(string userId, DateTime startDate)
        {
            IEnumerable<NetworkStatus> query = from hu in _context.HouseUser
                                               join dh in _context.DeviceHouse on hu.House equals dh.House
                                               join ns in _context.NetworkStatus on dh.Device equals ns.Device
                                               where hu.UserProfile == userId && ns.Date > startDate
                                               select ns;
            return query;
        }

        public IEnumerable<Device> GetDeviceListByUserName(string name)
        {
            IEnumerable<Device> query = from up in _context.AspNetUsers
                                        where up.UserName == name
                                        join hu in _context.HouseUser on up.Id equals hu.UserProfile
                                        join dh in _context.DeviceHouse on hu.House equals dh.House
                                        select dh.Device1;
            return query;
        }
    }
}
