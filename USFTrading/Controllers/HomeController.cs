using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USFTrading.DataAccess;
using Newtonsoft.Json;
using USFTrading.Models;
using USFTrading.Infrastructure;


namespace USFTrading.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }
		
		//************************************Views************************************

        public IActionResult Company()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            IEXHandler webHandler = new IEXHandler();
            List<Company> companies = webHandler.GetCompanies();
            //Save comapnies in TempData
            TempData["Company"] = JsonConvert.SerializeObject(companies);

            return View(companies);
        }


        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SectorAnalysis()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            IEXHandler webHandler1 = new IEXHandler();
            List<Sectors> sector = webHandler1.GetSector();

            //Save comapnies in TempData
            TempData["Sectors"] = JsonConvert.SerializeObject(sector);

            return View(sector);
        }



        public IActionResult KeyStatGainers()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
             IEXHandler webHandler = new IEXHandler();
             List<KeyStatGainers> keystat = webHandler.GetKeyStatGainers();

             //Save comapnies in TempData
             TempData["KeyStatGainers"] = JsonConvert.SerializeObject(keystat);

             return View(keystat);
        }

        public IActionResult KeyStatLosers()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            IEXHandler webHandler = new IEXHandler();
            List<KeyStatLosers> keystat = webHandler.GetKeyStatLosers();

            //Save comapnies in TempData
            TempData["KeyStatLosers"] = JsonConvert.SerializeObject(keystat);

            return View(keystat);
        }





        public IActionResult TopStock()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            IEXHandler webHandler1 = new IEXHandler();
            List<Stock> stock = webHandler1.GetTopTenStock();

            //Save comapnies in TempData
            TempData["Stock"] = JsonConvert.SerializeObject(stock);

            return View(stock);
        }

        //**********************Populating Table**********************
        public IActionResult PopulateCompany()
        {
            List<Company> companies = new List<Company>();
            if (TempData["Company"] != null)
            {
                companies = JsonConvert.DeserializeObject<List<Company>>(TempData["Company"].ToString());
                foreach (Company company in companies)
                {
                    //Database will give PK constraint violation error when trying to insert record with existing PK.
                    //So add company only if it doesnt exist, check existence using symbol (PK)
                    if (dbContext.Company.Where(c => c.symbol.Equals(company.symbol)).Count() == 0)
                    {
                        dbContext.Company.Add(company);
                    }
                }
                dbContext.SaveChanges();
                ViewBag.dbSuccessComp = 1;
                return View("Company", companies);
            }
            else
            {
                return View("Index");
            }
        }


        public IActionResult PopulateSector()
        {
            List<Sectors> sector = JsonConvert.DeserializeObject<List<Sectors>>(TempData["Sectors"].ToString());
            foreach (Sectors sect in sector)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Sector.Where(c => c.type.Equals(sect.type)).Count() == 0)
                {
                    dbContext.Sector.Add(sect);
                }
            }
            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("SectorAnalysis", sector);
        }

		        public IActionResult PopulateKeyStatGainers()
        {
            List<KeyStatGainers> sector = JsonConvert.DeserializeObject<List<KeyStatGainers>>(TempData["KeyStatGainers"].ToString());
            foreach (KeyStatGainers sect in sector)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.KeyStatGainers.Where(c => c.symbol.Equals(sect.symbol)).Count() == 0)
                {
                    dbContext.KeyStatGainers.Add(sect);
                }
            }
            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("KeyStatGainers", sector);
        }

        public IActionResult PopulateKeyStatLosers()
        {
            List<KeyStatLosers> sector=new List<KeyStatLosers>();
            if (TempData["KeyStatLosers"] != null)
            {
                sector = JsonConvert.DeserializeObject<List<KeyStatLosers>>(TempData["KeyStatLosers"].ToString());
                foreach (KeyStatLosers sect in sector)
                {
                    //Database will give PK constraint violation error when trying to insert record with existing PK.
                    //So add company only if it doesnt exist, check existence using symbol (PK)
                    if (dbContext.KeyStatLosers.Where(c => c.symbol.Equals(sect.symbol)).Count() == 0)
                    {
                        dbContext.KeyStatLosers.Add(sect);
                    }
                }
                dbContext.SaveChanges();
                ViewBag.dbSuccessComp = 1;
                return View("KeyStatLosers", sector);
            }
            else
            {
                return View("Index");
            }
        }


        public IActionResult PopulateStock()
        {
            List<Stock> sector = JsonConvert.DeserializeObject<List<Stock>>(TempData["Stock"].ToString());
            foreach (Stock sect in sector)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Stock.Where(c => c.symbol.Equals(sect.symbol)).Count() == 0)
                {
                    dbContext.Stock.Add(sect);
                }
            }
            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("TopStock",sector);
        }
		
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
