using KModel.Entity;
using System;
using System.Collections.Generic;
namespace KModel.Repository.Interface
{
    public interface ISensorDryRepository
    {
        SensorDry GetById(int id);
        IEnumerable<SensorDryData> GetDataListForPeriodByUserId(string id, DateTime startDate);
        SensorDryData GetLastDataBySensorId(int id);
        IEnumerable<SensorDry> GetListByHouseId(int id);
        int GetWarningSensorByHouseId(int id);
        bool SensorDryUpdateStatus(int SensorDryId, int status);
        bool SetReasonForDisarming(int SensorDryId, string Text, string userId);
    }
}
