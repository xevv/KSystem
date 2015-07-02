using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository.Interface
{
    public interface IUserRepository
    {
        AspNetUsers GetById(string id);
        bool Update(AspNetUsers model);
        IEnumerable<AspNetUsers> GetListByHouseId(int houseId);
    }
}
