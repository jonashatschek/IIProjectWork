using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IIProjectClient.Models
{
    public class Tjänstemeddelande
    {
        public string svarskod { get; set; }
        public string meddelande { get; set; }
        public string tjänsteansvarig { get; set; }
        public string appNamnVer { get; set; }
        public DateTime tidpunkt { get; set; }
        public string anropsansvarig { get; set; }
        public string argument { get; set; }

        public static Tjänstemeddelande fromXML(XElement xml)
        {
            Tjänstemeddelande tjänstemeddelande = new Tjänstemeddelande() 
            { 
                svarskod = xml.Element("tjänstemeddelande").Element("svarskod").Value,
                meddelande = xml.Element("tjänstemeddelande").Element("meddelande").Value,
                tjänsteansvarig = xml.Element("tjänstemeddelande").Element("tjänsteansvarig").Value,
                appNamnVer = xml.Element("tjänstemeddelande").Element("appNamnVer").Value,
                tidpunkt = new DateTime((long)xml.Element("tjänstemeddelande").Element("tidpunkt")),
                anropsansvarig = xml.Element("tjänstemeddelande").Element("anropsansvarig").Value,
                argument = xml.Element("tjänstemeddelande").Element("argument").Value
            };
            return tjänstemeddelande;
        }

        public XElement toXML()
        {
            var xml = 
                new XElement("Tjänstemeddelande",
                    new XElement("svarskod", this.svarskod),
                    new XElement("meddelande", this.meddelande),
                    new XElement("tjänsteansvarig", this.tjänsteansvarig),
                    new XElement("appNamnVer", this.appNamnVer),
                    new XElement("tidpunkt", this.tidpunkt),
                    new XElement("anropsansvarig", this.anropsansvarig),
                    new XElement("argument", this.argument)
                    );
            return xml;
        }

    }
}
