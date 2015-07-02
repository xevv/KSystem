using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository.Interface
{
    public interface IDeviceRepository
    {
        IEnumerable<NetworkStatus> GetNetworkStatusForPeriodByUserId(string userId, DateTime startDate);
        int GetLastNetworkStatusByDeviceId(int deviceId);
        IEnumerable<Device> GetDeviceListByUserName(string name);
    }
}
