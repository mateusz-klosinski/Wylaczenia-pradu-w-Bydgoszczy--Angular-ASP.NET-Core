using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Services
{
    public class DisconnectionService
    {

        private IConfigurationRoot _config;

        public DisconnectionService(IConfigurationRoot config)
        {
            _config = config;
        }

        public async Task<DisconnectionSorted> DownloadAndFormatDisconnectionsIntoListAsync()
        {
            string stringData = await getDataFromEneaWebsite();
            List<string> rawListData = CreateRawListFromRawStringDataAndFormatIt(stringData);

            return CreateDisconnectionList(rawListData);
        }

        private DisconnectionSorted CreateDisconnectionList(List<string> rawListData)
        {
            List<Disconnection> disconnectionsList = new List<Disconnection>();
            int count = rawListData.Count % 3 == 0 ? rawListData.Count : rawListData.Count - 1;
            for (int i = 0; i < count; i += 3)
            {
                disconnectionsList.Add(new Disconnection(rawListData[i], rawListData[i + 1], rawListData[i + 2]));
            }

            DisconnectionSorted sortedList = new DisconnectionSorted
            {

            Today = disconnectionsList.Where(d => d.Date == DateTime.Today),
            Tommorow = disconnectionsList.Where(d => d.Date == DateTime.Today + TimeSpan.FromDays(1)),
            Others = disconnectionsList.Where(d => d.Date > DateTime.Today + TimeSpan.FromDays(1)),
            };

            return sortedList;
        }

        private List<string> CreateRawListFromRawStringDataAndFormatIt(string stringData)
        {
            List<string> listData = StringHelper.convertStringToListByNewLines(stringData);
            StringHelper.removeHtmlTagsFromList(ref listData);
            StringHelper.removeUnnecesaryInformationFromList(ref listData);
            StringHelper.removeBlanksFromList(ref listData);

            return listData;
        }

        private async Task<string> getDataFromEneaWebsite()
        {
            string downloadUrl = _config["Urls:EneaUrl"];
            var httpClient = new HttpClient();

            var stringData = await httpClient.GetStringAsync(downloadUrl);
            return stringData;
        }
    }
}
