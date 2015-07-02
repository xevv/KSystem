using KModel.Entity;
using KModel.Repository;
using KModel.Repository.Interface;
using KSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KSystem.Controllers
{
    public class HouseController : AuthenticateController
    {
        private IHouseRepository HouseRepository;
        private ISensorDryRepository SensorDryRepository;

        public HouseController(IHouseRepository houseRepository, ISensorDryRepository sensorDryRepository)
        {
            HouseRepository = houseRepository;
            SensorDryRepository = sensorDryRepository;
        }

        public PartialViewResult HouseViewByUserName()
        {
            IEnumerable<House> model = HouseRepository.GetListByUserId(UserId);
            List<HouseViewModel> viewModel = new List<HouseViewModel>();
            foreach (House item in model)
            {
                int warning = SensorDryRepository.GetWarningSensorByHouseId(item.Id);
                viewModel.Add(new HouseViewModel(item, warning));
            }
            return PartialView(viewModel);
        }
    }
}
