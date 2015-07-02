using KModel.Entity;
using KSystem.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSystem.Function
{
    public interface IEmailNotification
    {
        bool Send(IEnumerable<string> mailto, string subject, string message);
        string CreateMessage(InputData data, SensorDry sensor, House house);
    }
}
