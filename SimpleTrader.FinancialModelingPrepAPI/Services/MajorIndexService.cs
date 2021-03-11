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
        private readonly FinancialModelingPrepHttpClient _httpClient;

        public MajorIndexService(FinancialModelingPrepHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            string uri = "majors-indexes/" + GetUriSuffix(indexType);

            MajorIndex majorIndex = await _httpClient.GetAsync<MajorIndex>(uri);
            majorIndex.Type = indexType;

            return majorIndex;
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
