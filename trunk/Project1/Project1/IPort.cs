using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Project1
{   [ServiceContract]
    public interface IPort
    {
    [OperationContract]
    void Recieve(int idPortu, string message);


    }
}
