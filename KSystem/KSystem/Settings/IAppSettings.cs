using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSystem.Settings
{
    public interface IAppSettings
    {
        T GetSettings<T>(string key) where T : IConvertible;
    }
}
