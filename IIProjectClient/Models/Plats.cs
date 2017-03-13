using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace IIProjectClient.Models
{
    public class Plats
    {
        public string platsEPC { get; set; }
        public string platsNamn { get; set; }

        public static Plats fromXML(XElement xml)
        {
            Plats plats = new Plats()
            {
                platsEPC = xml.Element("platsEPC").Value,
                platsNamn = xml.Element("platsNamn").Value
            };
            return plats;
        }

        public XElement toXML()
        {
            var plats =
                new XElement("Plats",
                    new XElement("platsEPC", this.platsEPC),
                    new XElement("platsNamn", this.platsNamn)
                    );
            return plats;
        }

    }

}