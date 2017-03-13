using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IIProjectClient.Models
{
    public class Sökning
    {
        //public int ID { get; set; }
        public IEnumerable<FordonPassage> fordonPassage { get; set; }
        public Tjänstemeddelande tjänstemeddelande { get; set; }

        public Sökning()
        {
            fordonPassage = new List<FordonPassage>();
        }
    }
}
/*
 <sökning>
    <id></id> överflödig?
    <FordonPassage>
        <ID></ID>
        <fordonsEPC></fordonsEPC>
        <Plats>
            <platsEPC></platsEPC>
            <platsNamn></platsnamn>
        </Plats>
        <from></from>
        <to></to>
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