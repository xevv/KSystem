using KModel.Entity;
using KModel.Repository;
using KModel.Repository.Interface;
using KSystem.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KSystem.Controllers
{
    public class SensorController : AuthenticateController
    {
        private ISensorDryRepository SensorDryRepository;
        private IDeviceRepository DeviceRepository;
        private IHouseRepository HouseRepository;

        public SensorController(ISensorDryRepository sensorDryRepository, IDeviceRepository deviceRepository, IHouseRepository houseRepository)
        {
            SensorDryRepository = sensorDryRepository;
            DeviceRepository = deviceRepository;
            HouseRepository = houseRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSensorViewListByHouseId(int id)
        {
            IEnumerable<SensorDry> model = SensorDryRepository.GetListByHouseId(id);
            if (model != null)
            {
                List<SensorDryRoomViewModel> viewModel = new List<SensorDryRoomViewModel>();
                foreach (SensorDry item in model)
                {
                    SensorDryViewModel SensorDryViewModel = new SensorDryViewModel();
                    SensorDryViewModel.Id = item.Id;
                    SensorDryViewModel.Door = item.Door1.Name;
                    SensorDryViewModel.Status = item.Status;
                    SensorDryViewModel.Device = item.Device;

                    SensorDryData data = new SensorDryData();
                    data = SensorDryRepository.GetLastDataBySensorId(item.Id);
                    SensorDryViewModel.Online = DeviceRepository.GetLastNetworkStatusByDeviceId(item.Device);
                    if (data != null)
                    {
                        SensorDryViewModel.Value = data.Data;
                        SensorDryViewModel.Time = data.Date;
                        if (data.Data == 0)
                        {
                            SensorDryViewModel.DisplayValue = item.SensorDryType.SensorDryValueType.Value0;
                        }
                        else if (data.Data == 1)
                        {
                            SensorDryViewModel.DisplayValue = item.SensorDryType.SensorDryValueType.Value1;
                        }
                    }
                    else
                    {
                        SensorDryViewModel.Value = 2;
                        SensorDryViewModel.DisplayValue = "Нет данных";
                        SensorDryViewModel.Time = DateTime.Today;
                    }

                    SensorDryRoomViewModel SensorListByRoom = viewModel
                        .Where(p => p.Room == item.Premises1.Name)
                        .FirstOrDefault();

                    if (SensorListByRoom != null)
                    {
                        SensorListByRoom.Sensor.Add(SensorDryViewModel);
                    }
                    else
                    {
                        SensorDryRoomViewModel SensorDryListViewModel = new SensorDryRoomViewModel(item.Premises1.Name, SensorDryViewModel);
                        viewModel.Add(SensorDryListViewModel);
                    }
                }
                foreach (SensorDryRoomViewModel item in viewModel)
                {
                    item.Sensor = item.Sensor.OrderBy(p => p.Value).ToList();
                }
                return PartialView(viewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        public PartialViewResult ReasonForDisarming(int sensorId)
        {
            SensorDry SensorDry = SensorDryRepository.GetById(sensorId);
            House house = HouseRepository.GetBySensorId(sensorId);
            ReasonForDisarmingViewModel model = new ReasonForDisarmingViewModel();
            model.UserId = UserId;
            model.HouseId = house.Id;
            model.Door = SensorDry.Door1.Name;
            model.SensorDryId = SensorDry.Id;
            return PartialView(model);
        }

        public ActionResult AddReasonForDisarming(ReasonForDisarmingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (SensorDryRepository.SetReasonForDisarming(model.SensorDryId, model.Reason, UserId)
                && SensorDryRepository.SensorDryUpdateStatus(model.SensorDryId, 0))
                    return Content("OK");
                else
                    ModelState.AddModelError("", "Не удалось сохранить в базу данных!");
                return PartialView("ReasonForDisarming", model);
            }
            else
            {
                return PartialView("ReasonForDisarming", model);
            }
        }

        public void UpdateSensorDryStatus(int SensorDry, int Status)
        {
            SensorDryRepository.SensorDryUpdateStatus(SensorDry, Status);
        }

        public ActionResult SensorDryDataForPeriod(string period)
        {
            DateTime startDate = DateTime.Now.AddDays(-1);
            switch (period)
            {
                case "week":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "twenty-four":
                    startDate = DateTime.Now.AddDays(-1);
                    break;
            }
            IEnumerable<SensorDryData> dataModel = SensorDryRepository.GetDataListForPeriodByUserId(UserId, startDate);
            IEnumerable<NetworkStatus> networkStatusModel = DeviceRepository.GetNetworkStatusForPeriodByUserId(UserId, startDate);
            List<SummaryLastDataViewModel> viewModel = new List<SummaryLastDataViewModel>();

            foreach (NetworkStatus item in networkStatusModel)
            {
                SummaryLastDataViewModel SummaryLastDataViewModel = new SummaryLastDataViewModel();
                SummaryLastDataViewModel.Date = item.Date;
                SummaryLastDataViewModel.Value = 2;
                House house = HouseRepository.GetByDeviceId(item.Device);
                SummaryLastDataViewModel.Message = String.Concat(house.Street, " ", house.Number, " Оборудование ");
                if (item.Status == 0)
                {
                    SummaryLastDataViewModel.StringValue += "Не в сети";
                }
                else
                {
                    SummaryLastDataViewModel.StringValue += "Снова в сети";
                }
                viewModel.Add(SummaryLastDataViewModel);
            }
            foreach (SensorDryData item in dataModel)
            {
                SummaryLastDataViewModel SummaryLastDataViewModel = new SummaryLastDataViewModel();
                SummaryLastDataViewModel.Date = item.Date;
                SensorDry SensorDry = SensorDryRepository.GetById(item.SensorId);
                House house = HouseRepository.GetBySensorId(item.SensorId);
                SummaryLastDataViewModel.Message = String.Concat(house.Street, " ", house.Number, " ", SensorDry.Premises1.Name, " ", SensorDry.Door1.Name, " ", SensorDry.SensorDryType.Name, " ", " изменил свое состояние на", " ");
                SummaryLastDataViewModel.Value = item.Data;
                if (item.Data == 0)
                {
                    SummaryLastDataViewModel.StringValue = SensorDry.SensorDryType.SensorDryValueType.Value0;
                }
                else
                {
                    SummaryLastDataViewModel.StringValue = SensorDry.SensorDryType.SensorDryValueType.Value1;
                }
                viewModel.Add(SummaryLastDataViewModel); 
            }
            return PartialView(viewModel.OrderByDescending(x => x.Date).ToList());
        }
    }
}
