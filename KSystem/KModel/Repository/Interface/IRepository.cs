﻿using KModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository.Interface
{
    public interface IRepository<T>
        where T : class
    {
        T GetById(int id);
    }
}
