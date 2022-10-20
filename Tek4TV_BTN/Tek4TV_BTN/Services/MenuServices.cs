using Newtonsoft.Json;
using System.Net.Http.Headers;
using Tek4TV_BTN.IServices;

namespace Tek4TV_BTN.Services
{
    public class MenuServices : IMenuServices
    {
        public async Task<dynamic> GetMenuAsync()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var domainApi = config["Domain:Url"];
            List<dynamic> arrayNulls = new List<dynamic>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(domainApi);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = "/api/Playlist/json/menu_8958c00a-f60a-447d-ad8d-d021806e5cd1";
                    var responseMessage = await httpClient.GetAsync(url);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string responseBody = await responseMessage.Content.ReadAsStringAsync();
                        dynamic output = JsonConvert.DeserializeObject(responseBody);
                        return output;

                    }
                    else
                    {
                        return arrayNulls;
                    }
                }
            }
            catch (Exception)
            {
                return arrayNulls;
            }
        }
    }
}
