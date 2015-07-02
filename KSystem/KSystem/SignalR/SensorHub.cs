using System;
using KModel.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using KModel.Repository;
using KModel.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using KModel.Repository.Interface;

namespace KSystem
{
    [HubName("SensorHub")]
    public class SensorHub : Hub
    {
        private DeviceRepository DeviceRepository;

        public SensorHub()
        {
            DeviceRepository = new DeviceRepository(new KBaseEntities());
        }

        public void Register(string user)
        {
            IEnumerable<Device> deviceList = DeviceRepository.GetDeviceListByUserName(user);
            if (deviceList != null)
            {
                foreach (Device item in deviceList)
                {
                    Groups.Add(Context.ConnectionId, item.Id.ToString());
                }
            }
        }
    }
}