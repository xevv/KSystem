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
    public class UserController : AuthenticateController
    {
        private IUserRepository UserRepository;

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public PartialViewResult Settings()
        {
            AspNetUsers model = UserRepository.GetById(UserId);
            SettingsViewModel WindowModel = new SettingsViewModel(model);
            return PartialView(WindowModel);
        }

        public ActionResult SetSettings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                AspNetUsers profile = new AspNetUsers
                {
                    Id = model.UserId,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    NotificationEmail = model.NotificationEmail,
                    NotificationPhone = model.NotificationPhone,
                    CallTimeStart = TimeSpan.Parse(model.CallTimeStart),
                    CallTimeEnd = TimeSpan.Parse(model.CallTimeEnd)
                };
                if (UserRepository.Update(profile))
                    return Json(new { result = "OK" });
                else
                {
                    ModelState.AddModelError("", "Не удалось сохранить данные");
                    return PartialView("Settings", model);
                }
            }
            else
            {
                return PartialView("Settings", model);
            }
        }
    }
}
