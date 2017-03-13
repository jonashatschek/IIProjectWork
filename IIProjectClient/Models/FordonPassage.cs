using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IIProjectClient.Models
{
    public class FordonPassage
    {
        public int ID { get; set; }
        public string fordonsEPC { get; set; }
        public Plats plats { get; set; }
        public string tidpunkt { get; set; }
        public string EVN { get; set; }
        public string fordonsinnehavare { get; set; }
        public string uaForetag { get; set; }
        public string fordonstyp { get; set; }
        public string giltigtGodkannande { get; set; }

        public static FordonPassage fromXML(XElement xml)
        {
            FordonPassage fordonPassage = new FordonPassage()
            {
                //ID = (int)xml.Element("ID"),
                fordonsEPC = xml.Element("fordonsEPC").Value,
                plats = new Plats()
                {
                    platsEPC = xml.Element("Plats").Element("platsEPC").Value,
                    platsNamn = xml.Element("Plats").Element("platsNamn").Value
                },
                tidpunkt = xml.Element("tidpunkt").Value,
                EVN = xml.Element("EVN").Value,
                fordonsinnehavare = xml.Element("fordonsinnehavare").Value,
                uaForetag = xml.Element("uaForetag").Value,
                fordonstyp = xml.Element("fordonstyp").Value,
                giltigtGodkannande = xml.Element("giltigtGodkannande").Value
            };
            return fordonPassage;
        }

        public XElement toXML()
        {
            var xml =
                new XElement("FordonPassage",
                    new XElement("ID", this.ID),
                    new XElement("fordonsEPC", this.fordonsEPC),
                    plats.toXML(),
                    new XElement("tidpunkt", this.tidpunkt.ToString()),
                    new XElement("EVN", this.EVN),
                    new XElement("fordonsinnehavare", this.fordonsinnehavare),
                    new XElement("uaForetag", this.uaForetag),
                    new XElement("fordonstyp", this.fordonstyp),
                    new XElement("giltigtGodkannande", this.giltigtGodkannande)
                    );
            return xml;

        }

    }

}
/*
 <sökning>
    <id></id>
    <FordonPassage>
        <ID></ID>
        <fordonsEPC></fordonsEPC>
        <Plats>
            <platsEPC></platsEPC>
            <platsNamn></platsnamn>
        </Plats>
        <tidpunkt></tidpunkt>
        <EVN></EVN>
        <fordonsinnehavare></fordonsinnehavare>
        <uaForetag></uaForetag>
        <fordonstyp></fordonstyp>
        <giltigtGodkannande></giltigtGodkannande>
    </FordonPassage>
    <Tjänstemeddelande>
        <ID></ID>
        <svarskod></svarskod>
        <meddelande></meddelande>
        <tjänsteansvarig></tjänsteansvarig>
        <appNamnVer></appNamnVer>
        <tidpunkt></tidpunkt>
        <anropsansvarig></anropsansvarig>
        <argument></argument>
    </Tjänstemeddelande>
 </sökning>
 
 */