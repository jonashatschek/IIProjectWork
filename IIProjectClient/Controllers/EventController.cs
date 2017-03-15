using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Hosting;
using IIProjectClient.projectService;
using IIProjectClient.Models;
using PagedList;

namespace IIProjectClient.Controllers
{
    public class EventController : Controller
    {
        private iProjectServiceClient client = new iProjectServiceClient();
        private List<string> användare = new List<string>();
        private List<FordonPassage> listFordonPassage = new List<FordonPassage>();
        
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Sökning(int? sida)
        {
            listFordonPassage = (List<FordonPassage>)TempData["modellen"];
            int sidnummer = (sida ?? 1);
            int antalsidor = 5;
            TempData["modellen"] = listFordonPassage;
            return View(listFordonPassage.ToPagedList(sidnummer, antalsidor));
        }
            
        [HttpPost]
        public ActionResult Index(DateTime from, DateTime tom, string plats, string namn)
        {

            användare.Add("Krösus Sork");
            användare.Add("Vargen");
            användare.Add("Heffaklumpen");
            användare.Add("Gargamel");

            string Svarskod = "";
            string Meddelande = "";
            string Tjansteansvarig = "PG-8";
            string AppNamnVer = "IIProjectWork, 1.0";
            DateTime Tidpunkt = DateTime.Now.Date;
            string Anropsansvarig = namn;
            string Argument = from.Date.ToString("d") + ", " + tom.Date.ToString("d") + ", " + plats;

            if (!användare.Contains(namn))
            {
                Svarskod = "3";
                Meddelande = "Ej behörig bandit.";
            }
            else
            {
                if (DateTime.Compare(from,tom) >= 0)
                {
                    Svarskod = "2";
                    Meddelande = "Kontrollera input.";
                }
                else
                {
                    Svarskod = "1";
                    Meddelande = "Behörig användare.";

                    XElement allaPlatser = client.HämtaAllaPlatser();
                    var specplatsXML = from p in allaPlatser.Descendants("Location")
                                       where p.Element("Name").Value == plats
                                       select
                                       new XElement("Plats",
                                           new XElement("epc", p.Element("Epc").Value));
                    string specplats = specplatsXML.Elements("epc").FirstOrDefault().Value;

                    XElement platsen = client.HämtaPlats(specplats);
                    Plats söktPlats = new Plats()
                    {
                        platsNamn = platsen.Element("Location").Element("Name").Value,
                        platsEPC = platsen.Element("Location").Element("Epc").Value
                    };

                    IEnumerable<XElement> events = client.HämtaEvents(from, tom, specplats);
                    var fordonpassage = from fp in events.Descendants("ObjectEvent")
                                        let fordonsEPC = fp.Element("epcList").Element("epc").Value
                                        let tidpunkt = fp.Element("eventTime").Value
                                        select
                                            new XElement("FordonPassage",
                                                new XElement("fordonsEPC", fordonsEPC),
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
                                                            new XElement("giltigtGodkannande", e.Element("FordonsIndivid").Element("Godkannande").Element("FordonsgodkannandeFullVardeSE").Value)
                                                            )

                                            );

                    foreach (var item in fordonpassage)
                    {
                        listFordonPassage.Add(FordonPassage.fromXML(item));
                    }



                }
            }

            Tjänstemeddelande tjanstemeddelande = new Tjänstemeddelande()
            {
                svarskod = Svarskod,
                meddelande = Meddelande,
                tjänsteansvarig = Tjansteansvarig,
                appNamnVer = AppNamnVer,
                tidpunkt = Tidpunkt,
                anropsansvarig = Anropsansvarig,
                argument = Argument
            };
            foreach (var item in listFordonPassage)
            {
                XElement godkannande = client.HämtaFordon(item.fordonsEPC).Element("Fordonsindivider").Element("FordonsIndivid").Element("Godkannande");

                if (item.giltigtGodkannande.Contains("Tillsvidare godkännande"))
                {
                    item.giltigtGodkannande = String.Empty;
                    item.giltigtGodkannande = "Tillsvidare godkännande, Fr.o.m.: " + godkannande.Element("GiltigtFrom").Value.Substring(0, 10);
                }
                else
                {
                    string ejGodkand = item.giltigtGodkannande;
                    item.giltigtGodkannande = String.Empty;
                    item.giltigtGodkannande = ejGodkand + ", Fr.o.m.: " + godkannande.Element("GiltigtFrom").Value.Substring(0, 10) + ", T.o.m.: " + godkannande.Element("GiltigtTom").Value.Substring(0, 10);
                }
                item.tjanstemeddelande = tjanstemeddelande;
            }
            TempData["modellen"] = listFordonPassage; 
            return RedirectToAction("Sökning");
        }

/* Såhär ska hela XML-svaret se ut i slutändan. 
<sökning>
   <id></id>
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
    }
}