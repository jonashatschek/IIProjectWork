using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IIProjectService.IIService;
using System.Xml.Linq;

namespace IIProjectService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ProjectService : iProjectService
    {
        private EpcisEventServiceClient client = new EpcisEventServiceClient();
        private NamingServiceClient namingClient = new NamingServiceClient();

        public IEnumerable<XElement> HämtaEvents(DateTime fromInclusive, DateTime toInclusive, String readPointEPC)
        {
            IEnumerable<XElement> allaEvent = client.GetEvents(fromInclusive, toInclusive, readPointEPC);
            return allaEvent;
        }


        public XElement HämtaPlats(string epc)
        {
            XElement plats = namingClient.GetLocation(epc);
            return plats;
        }


        public XElement HämtaFordon(string epc)
        {
            XElement fordon = namingClient.GetVehicle(epc);
            return fordon;
        }

        public XElement HämtaAllaPlatser()
        {
            XElement allaPlatser = namingClient.GetAllLocations();
            return allaPlatser;
        }
    }
}
