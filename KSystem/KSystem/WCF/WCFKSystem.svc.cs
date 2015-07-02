using KModel.Entity;
using KModel.Repository.Interface;
using KSystem.Function;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KSystem.WCF
{
    public class WCFKSystem : IWCFKSystem
    {
        private ISensorDryRepository SensorDryRepository;
        private IHouseRepository HouseRepository;
        private IEmailNotification EmailNotification;
        private IUserRepository UserRepository;

        public WCFKSystem(ISensorDryRepository sensorDryRepository,
            IHouseRepository houseRepository,
            IEmailNotification emailNotification,
            IUserRepository userRepository)
        {
            SensorDryRepository = sensorDryRepository;
            HouseRepository = houseRepository;
            EmailNotification = emailNotification;
            UserRepository = userRepository;
        }
        public void DoWork(InputData data)
        {
            House house = HouseRepository.GetBySensorId(data.DeviceId);
            int warning = SensorDryRepository.GetWarningSensorByHouseId(house.Id);
            var context = GlobalHost.ConnectionManager.GetHubContext<SensorHub>();
            context.Clients.Group(data.DeviceId.ToString()).updateSensor(new
            {
                SensorId = data.SensorId,
                Data = data.Data,
                Date = data.Date.ToString(),
                HouseId = house.Id.ToString(),
                Warning = warning.ToString()
            });
            SensorDry sensor = SensorDryRepository.GetById(data.SensorId);
            if (sensor.Status != 0)
            {
                IEnumerable<AspNetUsers> Users = UserRepository.GetListByHouseId(house.Id);
                EmailNotification.Send(Users.Select(x => x.Email).ToList(), "Изменение состояние охраны", EmailNotification.CreateMessage(data, sensor, house));
            }
        }

        public void Offline(int deviceId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SensorHub>();
            House house = HouseRepository.GetByDeviceId(deviceId);
            context.Clients.Group(deviceId.ToString()).offlineDevice(house.Id);
            IEnumerable<AspNetUsers> Users = UserRepository.GetListByHouseId(house.Id);
            EmailNotification.Send(Users.Select(x => x.Email).ToList(), "Изменение состояние охраны", "test");

        }

        public void Online(int deviceId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SensorHub>();
            House house = HouseRepository.GetByDeviceId(deviceId);
            context.Clients.Group(deviceId.ToString()).onlineDevice(house.Id);
        }
    }
}
