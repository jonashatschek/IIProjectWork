﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace IIProjectService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface iProjectService
    {
        // TODO: Add your service operations here
        [OperationContract]
        IEnumerable<XElement> HämtaEvents(DateTime fromInclusive, DateTime toInclusive, String readPointEPC);

        [OperationContract]
        XElement HämtaPlats(String epc);

        [OperationContract]
        XElement HämtaFordon(String epc);

        [OperationContract]
        XElement HämtaAllaPlatser();
    }
}
