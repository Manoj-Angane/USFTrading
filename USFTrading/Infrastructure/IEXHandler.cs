using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using USFTrading.Models;
using Newtonsoft.Json;

namespace USFTrading.Infrastructure
{
    public class IEXHandler
    {
        static string BASE_URL = "https://api.iextrading.com/1.0/"; //This is the base URL, method specific URL is appended to this.
        HttpClient httpClient;

        public IEXHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /****
         * Calls the IEX reference API to get the list of symbols. 
        ****/
		public List<Company> GetCompanies()
        {
            
            List<String> companiesSymbolList = GetGainersSymbols();
			List<String> companiesSymbolList1 = GetLosersSymbols();

            List<Company> stockList = GetCompaniesDetail(companiesSymbolList,"Gainer");
            List<Company> stockList1 = GetCompaniesDetail(companiesSymbolList1,"Lossr");
			stockList = stockList.Concat(stockList1).ToList();
            return stockList;
        }
		
        public List<Sectors> GetSector()
        {
            string IEXTrading_API_PATH = BASE_URL + "stock/market/sector-performance";
            string sectorList = "";

            List<Sectors> sector = null;

            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                sectorList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (!sectorList.Equals(""))
            {
                sector = JsonConvert.DeserializeObject<List<Sectors>>(sectorList);
                //sector = sector.GetRange(0, 20);
            }

            return sector;
        }


        public List<Stock> GetTopTenStock()
        {
            string IEXTrading_STOCK_DATA_API_PATH = "";     
            List<String> companiesSymbolList = GetGainersSymbols();

            var stockList = new List<Stock>();
            var s = new Stock();

            foreach (var companySymbol in companiesSymbolList)
            {
                IEXTrading_STOCK_DATA_API_PATH = BASE_URL + "stock/{0}/quote";
                HttpClient httpClient;
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string companyDataString = "";
                IEXTrading_STOCK_DATA_API_PATH = string.Format(IEXTrading_STOCK_DATA_API_PATH, companySymbol);
                httpClient.BaseAddress = new Uri(IEXTrading_STOCK_DATA_API_PATH);
                HttpResponseMessage response = httpClient.GetAsync(IEXTrading_STOCK_DATA_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    companyDataString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!companyDataString.Equals(""))
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    s = JsonConvert.DeserializeObject<Stock>(companyDataString, settings);
                    stockList.Add(s);
                }


            }
            List<Stock> SortedList = stockList.OrderByDescending(o => o.changePercent).ToList();
            if (SortedList.Count > 10)
            {
                SortedList = SortedList.GetRange(0, 10);
            }
            return SortedList;
        }

        public List<String> GetGainersSymbols()
        {
            List<String> popularSymbols = new List<String>();
            ////
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ///
            List<Infocus> InfocusList = new List<Infocus>();
            string IEXTrading_GAINERS_API_PATH = BASE_URL + "stock/market/list/gainers";
            httpClient.BaseAddress = new Uri(IEXTrading_GAINERS_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_GAINERS_API_PATH).GetAwaiter().GetResult();
            string companies = "";
            if (response.IsSuccessStatusCode)
            {
                companies = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (companies != null || !companies.Equals(""))
            {
                InfocusList = JsonConvert.DeserializeObject<List<Infocus>>(companies);
            }


            foreach (var company in InfocusList)
            {
                popularSymbols.Add(company.symbol);
            }

            return popularSymbols;
        }
		
		public List<String> GetLosersSymbols()
        {
            List<String> popularSymbols = new List<String>();
            ////
            HttpClient httpClient;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            ///
            List<Infocus> InfocusList = new List<Infocus>();
            string IEXTrading_GAINERS_API_PATH = BASE_URL + "stock/market/list/losers";
            httpClient.BaseAddress = new Uri(IEXTrading_GAINERS_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_GAINERS_API_PATH).GetAwaiter().GetResult();
            string companies = "";
            if (response.IsSuccessStatusCode)
            {
                companies = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            if (companies != null || !companies.Equals(""))
            {
                InfocusList = JsonConvert.DeserializeObject<List<Infocus>>(companies);
            }


            foreach (var company in InfocusList)
            {
                popularSymbols.Add(company.symbol);
            }

            return popularSymbols;
        }
		public List<KeyStatGainers> GetKeyStatGainers()
        {
            string IEXTrading_STOCK_DATA_API_PATH = "";
            List<String> companiesSymbolList = GetGainersSymbols();

            var stockList = new List<KeyStatGainers>();
            var s = new KeyStatGainers();

            foreach (var companySymbol in companiesSymbolList)
            {
                IEXTrading_STOCK_DATA_API_PATH = BASE_URL + "stock/{0}/stats";
                HttpClient httpClient;
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string companyDataString = "";
                IEXTrading_STOCK_DATA_API_PATH = string.Format(IEXTrading_STOCK_DATA_API_PATH, companySymbol);
                httpClient.BaseAddress = new Uri(IEXTrading_STOCK_DATA_API_PATH);
                HttpResponseMessage response = httpClient.GetAsync(IEXTrading_STOCK_DATA_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    companyDataString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!companyDataString.Equals(""))
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    s = JsonConvert.DeserializeObject<KeyStatGainers>(companyDataString, settings);
                    s.symbol=companySymbol;
                    stockList.Add(s);
                }
            }
            return stockList;
        }

        public List<KeyStatLosers> GetKeyStatLosers()
        {
            string IEXTrading_STOCK_DATA_API_PATH = "";
            List<String> companiesSymbolList = GetLosersSymbols();

            var stockList = new List<KeyStatLosers>();
            var s = new KeyStatLosers();

            foreach (var companySymbol in companiesSymbolList)
            {
                IEXTrading_STOCK_DATA_API_PATH = BASE_URL + "stock/{0}/stats";
                HttpClient httpClient;
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string companyDataString = "";
                IEXTrading_STOCK_DATA_API_PATH = string.Format(IEXTrading_STOCK_DATA_API_PATH, companySymbol);
                httpClient.BaseAddress = new Uri(IEXTrading_STOCK_DATA_API_PATH);
                HttpResponseMessage response = httpClient.GetAsync(IEXTrading_STOCK_DATA_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    companyDataString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!companyDataString.Equals(""))
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    s = JsonConvert.DeserializeObject<KeyStatLosers>(companyDataString, settings);
                    s.symbol = companySymbol;
                    stockList.Add(s);
                }
            }
            return stockList;
        }

        public List<Company> GetCompaniesDetail(List<String> companiesSymbolList,string a)
        {
			string IEXTrading_STOCK_DATA_API_PATH = "";
            var stockList = new List<Company>();
            var s = new Company();

            foreach (var companySymbol in companiesSymbolList)
            {
                IEXTrading_STOCK_DATA_API_PATH = BASE_URL + "stock/{0}/company";
                HttpClient httpClient;
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string companyDataString = "";
                IEXTrading_STOCK_DATA_API_PATH = string.Format(IEXTrading_STOCK_DATA_API_PATH, companySymbol);
                httpClient.BaseAddress = new Uri(IEXTrading_STOCK_DATA_API_PATH);
                HttpResponseMessage response = httpClient.GetAsync(IEXTrading_STOCK_DATA_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    companyDataString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!companyDataString.Equals(""))
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    s = JsonConvert.DeserializeObject<Company>(companyDataString, settings);
                    stockList.Add(s);
                }
            }
			stockList=stockList.Select(c => {c.Gainer_Loser = a; return c;}).ToList();
            return stockList;
        }
    }
}
