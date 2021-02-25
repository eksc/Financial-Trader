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
            using(FinancialModelingPrepHttpClient client = new FinancialModelingPrepHttpClient())
            {
                string uri = "majors-indexes/" + GetUriSuffix(indexType) + "?apikey=70c203a1543b06e297b86f065e29a9d8";

                MajorIndex majorIndex = await client.GetAsync<MajorIndex>(uri);
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
                    return ".IXIC";
                case MajorIndexType.Nasdaq:
                    return ".INX";
                default:
                    throw new Exception("MajorIndexType does not have a suffix defined");
            }
        }
    }
}
