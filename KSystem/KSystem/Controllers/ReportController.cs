using KModel.Entity;
using KModel.Repository;
using KModel.Repository.Interface;
using KSystem.Model;
using KSystem.Model.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KSystem.Controllers
{
    public class ReportController : AuthenticateController
    {
        private IReportRepository ReportRepository;
        private IHouseRepository HouseRepository;
        private ISensorDryRepository SensorDryRepository;

        public ReportController(IReportRepository reportRepository, IHouseRepository houseRepository, ISensorDryRepository sensorDryRepository)
        {
            ReportRepository = reportRepository;
            HouseRepository = houseRepository;
            SensorDryRepository = sensorDryRepository;
        }

        public PartialViewResult GetReports()
        {
            IEnumerable<Report> ViewModel = ReportRepository.GetReports();
            return PartialView(ViewModel);
        }

        public PartialViewResult CreateReasonForDisarming()
        {
            DefaultReportModel model = new DefaultReportModel();
            IEnumerable<House> houses = HouseRepository.GetListByUserId(UserId);
            List<HouseViewModel> housesViewModel = new List<HouseViewModel>();
            foreach (House item in houses)
            {
                housesViewModel.Add(new HouseViewModel(item, 1));
            }
            model.Houses = housesViewModel;
            return PartialView(model);
        }



        public ActionResult ReasonForDisarmingReport(DefaultReportModel model, int numPage = 0)
        {
            if (numPage != 0)
            {
                ReasonForDisarmingReportViewModel ViewModel = new ReasonForDisarmingReportViewModel(GetSession<ReasonForDisarmingReportViewModel>());
                ViewModel.Report = ViewModel.Report.Skip(ViewModel.PageInfo.PageSize * (numPage - 1)).Take(ViewModel.PageInfo.PageSize).ToList();
                ViewModel.PageInfo.PageNumber = numPage;
                return PartialView("ReasonForDisarmingReport", ViewModel);
            }
            if (ModelState.IsValid)
            {
                DateTime StartDateTime = model.Date.StartDateSummary;
                DateTime EndDateTime = model.Date.EndDateSummary;
                List<int> houses = new List<int>();
                foreach (HouseViewModel item in model.Houses)
                {
                    if (item.LabelForValue == 1)
                    {
                        houses.Add(item.Id);
                    }
                }
                IEnumerable<ReasonForDisarming> ReportModel = ReportRepository.GetReasonForDisarmingReport(StartDateTime, EndDateTime, houses);
                ReasonForDisarmingReportViewModel ReportResult = new ReasonForDisarmingReportViewModel();
                foreach (ReasonForDisarming item in ReportModel)
                {
                    ReasonForDisarmingReportResult ItemViewModel = new ReasonForDisarmingReportResult();
                    House house = HouseRepository.GetBySensorId(item.SensorDry);
                    ItemViewModel.User = item.AspNetUsers.FIO;
                    ItemViewModel.Object = String.Concat(house.Street, " ", house.Number, ". ", item.SensorDry1.Premises1.Name, ". ", item.SensorDry1.Door1.Name);
                    ItemViewModel.SensorType = item.SensorDry1.SensorDryType.Name;
                    ItemViewModel.Reason = item.Reason;
                    ItemViewModel.Date = item.Date.ToString();
                    ReportResult.Report.Add(ItemViewModel);
                }
                ReportResult.PageInfo.TotalItems = ReportResult.Report.Count;
                SetSession<ReasonForDisarmingReportViewModel>(ReportResult);
                ReasonForDisarmingReportViewModel ViewModel = new ReasonForDisarmingReportViewModel(ReportResult);
                ViewModel.Report = ReportResult.Report.Skip(0).Take(ReportResult.PageInfo.PageSize).ToList();
                ViewModel.PageInfo.PageNumber = 1;
                return PartialView("ReasonForDisarmingReport", ViewModel);
            }
            else
            {
                string error = ModelState.SelectMany(p => p.Value.Errors).First().ErrorMessage;
                return Json(new { result = "FALSE", message = error });
            }
        }
    }
}
