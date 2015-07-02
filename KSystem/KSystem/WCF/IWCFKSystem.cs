using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace KSystem.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFKSystem" in both code and config file together.
    [ServiceContract]
    public interface IWCFKSystem
    {
        [OperationContract]
        void DoWork(InputData data);

        [OperationContract]
        void Offline(int deviceId);

        [OperationContract]
        void Online(int deviceId);
    }

    [ServiceContract]
    public class InputData
    {
        public int DeviceId { get; set; }
        public DateTime Date { get; set; }
        public int SensorId { get; set; }
        public int Data { get; set; }
    }
}
