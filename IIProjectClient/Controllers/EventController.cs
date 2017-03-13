using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Hosting;
using IIProjectClient.ProjectService;
using IIProjectClient.Models;

namespace IIProjectClient.Controllers
{
    public class EventController : Controller
    {
        iProjectServiceClient client = new iProjectServiceClient();
        
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }
        /* Såhär ska hela XML-svaret se ut i slutändan. 
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
               <FordonsInfo>
                    <EVN></EVN>
                    <fordonsinnehavare></fordonsinnehavare>
                    <uaForetag></uaForetag>
                    <fordonstyp></fordonstyp>
                    <giltigtGodkannande></giltigtGodkannande>
               </FordonsInfo>
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

        [HttpPost]
        public ActionResult Index(DateTime from, DateTime tom, string plats)
        {
            //Hämtar alla platser.
            XElement allaPlatser = client.HämtaAllaPlatser();
            //Hittar platsens epc.
            var specplatsXML = from p in allaPlatser.Descendants("Location")
                               where p.Element("Name").Value == plats
                               select
                               new XElement("Plats",
                                   new XElement("epc", p.Element("Epc").Value));
            string specplats = specplatsXML.Elements("epc").FirstOrDefault().Value;


            //Hämtar alla event inom ett tidsspann.
            IEnumerable<XElement> events = client.HämtaEvents(from, tom, specplats);
            //Hämtar info om platsen som skrevs in i vyn.
            XElement platsen = client.HämtaPlats(specplats);

            Plats söktPlats = new Plats()
            {
                platsNamn = platsen.Element("Location").Element("Name").Value,
                platsEPC = platsen.Element("Location").Element("Epc").Value
            };
            var fordon = client.HämtaFordon("urn:epc:id:giai:123456.1847447213244");
            var fordonpassage = from fp in events.Descendants("ObjectEvent") //events kommer bytas här antar jag.
                                let id = "" //Dessa rader ska fyllas i vart man hittar infon.
                                let fordonsEPC = fp.Element("epcList").Element("epc").Value
                                let tidpunkt = fp.Element("eventTime").Value
                                select 
                                    new XElement("FordonPassage",
                                        new XElement("ID", id),
                                        new XElement("fordonsEPC",fordonsEPC),
                                        söktPlats.toXML(),
                                        new XElement("tidpunkt", tidpunkt),
                                            from e in client.HämtaFordon(fordonsEPC).Descendants("Fordonsindivider")
                                            select
                                            new XElement("FordonsInfo",
                                                    new XElement("EVN", e.Element("FordonsIndivid").Element("Fordonsnummer").Value),
                                                    new XElement("fordonsinnehavare", e.Element("FordonsIndivid").Element("Fordonsinnehavare").Element("Foretag").Value),
                                                    new XElement("uaForetag", e.Element("FordonsIndivid").Element("UnderhallsansvarigtForetag").Element("Foretag").Value),
                                                    new XElement("fordonstyp", (from f in client.HämtaFordon(fordonsEPC).Descendants("FordonsTyp")
                                                                                select f.Element("FordonskategoriKodFullVardeSE").Value)),
                                                    new XElement("giltigtGodkannande", "asd")
                                                    )

                );



            //Här bygger vi ihop tjänstemeddelandet som ska följa med en sökning. 
            int ID = 0;
            string Svarskod = "tst";
            string Meddelande = "tst";
            string Tjansteansvarig = "tst";
            string AppNamnVer = "tst";
            DateTime Tidpunkt = DateTime.Now;
            string Anropsansvarig = "test";
            string Argument = "tst";

            Tjänstemeddelande tjanstemeddelande = new Tjänstemeddelande()
            {
                ID = ID,
                svarskod = Svarskod,
                meddelande = Meddelande,
                tjänsteansvarig = Tjansteansvarig,
                appNamnVer = AppNamnVer,
                tidpunkt = Tidpunkt,
                anropsansvarig = Anropsansvarig,
                argument = Argument
            };

            //Konverterar varje XLM-fordonspassage till ett FordonPassage-objekt
            //och lägger till det objeketet i en ny lista.
            List<FordonPassage> listFordonPassage = new List<FordonPassage>();
            foreach (var item in fordonpassage)
            {
                listFordonPassage.Add(FordonPassage.fromXML(item));
            }
            
            //All info om sökningen kommer finnas med i ett objekt av typen Sökning
            //skapas på följande vis.
            Sökning sokning = new Sökning()
            {
                tjänstemeddelande = tjanstemeddelande,
                fordonPassage = listFordonPassage
            };


            return View(sokning);
        }









        //---------------------ALLT DETTA KANSKE ÄR SKRÄP ?------------
        public ActionResult GetAllFiles()
        {
            throw new NotImplementedException();
        }

        public ActionResult GetEvent(String filename)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult GetAllEvents(DateTime from, DateTime to, string plats)
        {
            IEnumerable<XElement> events = client.HämtaEvents(from, to, plats);

            return View(events);
        }

        public ActionResult GetAllEvents()
        {
            return View();
        }

        public ActionResult Vehicles(String epc)
        {
            throw new NotImplementedException();
        }

        public ActionResult Location(String epc)
        {
            throw new NotImplementedException();
        }

        public ActionResult AllLocations()
        {
            throw new NotImplementedException();
        }

    }
}