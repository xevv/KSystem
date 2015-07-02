using KModel.Entity;
using KModel.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository
{
    public class UserRepository : IUserRepository
    {
        private KBaseEntities _context;

        public UserRepository(KBaseEntities context)
        {
            _context = context;
        }

        public AspNetUsers GetById(string id)
        {
            return _context.AspNetUsers
                .Where(p => p.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<AspNetUsers> GetListByHouseId(int houseId)
        {
            IEnumerable<AspNetUsers> query = from hu in _context.HouseUser
                                             join up in _context.AspNetUsers on hu.UserProfile equals up.Id
                                             where hu.House == houseId
                                             select up;
            return query;
        }

        public bool Update(AspNetUsers model)
        {
            AspNetUsers profile = GetById(model.Id);
            if (model.FIO != null)
                profile.FIO = model.FIO;
            if (model.Org != 0)
                profile.Org = model.Org;
            if (model.UserName != null)
                profile.UserName = model.UserName;
            if (model.Email != null)
                profile.Email = model.Email;
            if (model.PhoneNumber != null)
                profile.PhoneNumber = model.PhoneNumber;
            if (model.NotificationEmail != null)
                profile.NotificationEmail = model.NotificationEmail;
            if (model.NotificationPhone != null)
                profile.NotificationPhone = model.NotificationPhone;
            if (model.CallTimeStart != null)
                profile.CallTimeStart = model.CallTimeStart;
            if (model.CallTimeEnd != null)
                profile.CallTimeEnd = model.CallTimeEnd;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch { return false; };
        }
    }
}
