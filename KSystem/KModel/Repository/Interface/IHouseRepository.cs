using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository.Interface
{
    public interface IHouseRepository
    {
        House GetByDeviceId(int deviceId);
        IEnumerable<House> GetListByUserId(string id);
        House GetBySensorId(int id);
    }
}
