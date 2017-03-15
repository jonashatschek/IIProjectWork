using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PagedList;

namespace IIProjectClient.Models
{
    public class Sökning
    {
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
    <FordonPassage>
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