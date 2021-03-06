﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace IIProjectClient.Models
{
    public class FordonPassage
    {
        public string fordonsEPC { get; set; }
        public Plats plats { get; set; }
        public string tidpunkt { get; set; }
        public string EVN { get; set; }
        public string fordonsinnehavare { get; set; }
        public string uaForetag { get; set; }
        public string fordonstyp { get; set; }
        public string giltigtGodkannande { get; set; }
        public Tjänstemeddelande tjanstemeddelande { get; set; }

        public static FordonPassage fromXML(XElement xml)
        {
            FordonPassage fordonPassage = new FordonPassage()
            {
                fordonsEPC = xml.Element("fordonsEPC").Value,
                plats = new Plats()
                {
                    platsEPC = xml.Element("Plats").Element("platsEPC").Value,
                    platsNamn = xml.Element("Plats").Element("platsNamn").Value
                },
                tidpunkt = xml.Element("tidpunkt").Value,
                EVN = xml.Element("FordonsInfo").Element("EVN").Value,
                fordonsinnehavare = xml.Element("FordonsInfo").Element("fordonsinnehavare").Value,
                uaForetag = xml.Element("FordonsInfo").Element("uaForetag").Value,
                fordonstyp = xml.Element("FordonsInfo").Element("fordonstyp").Value,
                giltigtGodkannande = xml.Element("FordonsInfo").Element("giltigtGodkannande").Value
            };
            return fordonPassage;
        }

        public XElement toXML()
        {
            var xml =
                new XElement("FordonPassage",
                    new XElement("fordonsEPC", this.fordonsEPC),
                    plats.toXML(),
                    new XElement("tidpunkt", this.tidpunkt.ToString()),
                    new XElement("FordonsInfo",
                        new XElement("EVN", this.EVN),
                        new XElement("fordonsinnehavare", this.fordonsinnehavare),
                        new XElement("uaForetag", this.uaForetag),
                        new XElement("fordonstyp", this.fordonstyp),
                        new XElement("giltigtGodkannande", this.giltigtGodkannande))
                    );
            return xml;

        }

    }

}
/* Såhär ska hela XML-svaret se ut i slutändan. 
<sökning>
   <FordonPassage>
       <fordonsEPC></fordonsEPC>
       <Plats>
           <platsEPC></platsEPC>
           <platsNamn></platsnamn>
       </Plats>
       <tidpunkt></tidpunkt>
       <FordonsInfo>
            <EVN></EVN>
            <fordonsinnehavare></fordonsinnehavare>
            <uaForetag></uaForetag>
            <fordonstyp></fordonstyp>
            <giltigtGodkannande></giltigtGodkannande>
       </FordonsInfo>
   </FordonPassage>
   <Tjänstemeddelande>
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