using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using(HttpClient client = new HttpClient())
            {
                string uri = "https://financialmodelingprep.com/api/v3/majors-indexes/" + GetUriSuffix(indexType) + "?apikey=70c203a1543b06e297b86f065e29a9d8";

                HttpResponseMessage response = await client.GetAsync("https://financialmodelingprep.com/api/v3/majors-indexes/.DJI?apikey=70c203a1543b06e297b86f065e29a9d8");
                string jsonResponse = await response.Content.ReadAsStringAsync();

                MajorIndex majorIndex = JsonConvert.DeserializeObject<MajorIndex>(jsonResponse);
                majorIndex.Type = indexType;

                return majorIndex;
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.SP500:
                    return "..IXIC";
                case MajorIndexType.Nasdaq:
                    return ".INX";
                default:
                    return ".DJI";
            }
        }
    }
}
