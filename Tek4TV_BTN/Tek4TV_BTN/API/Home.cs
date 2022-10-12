using System.Net.Http.Headers;

namespace Tek4TV_BTN.API
{
    public class Home
    {
        public async Task<dynamic> GetPlaylist(string id)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var _domain = config["Domain:url"];
            List<dynamic> emData = new List<dynamic>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_domain);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"api/Playlist/json/" + id;
                    var responseMessage = await httpClient.GetAsync(url);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var rs = await responseMessage.Content.ReadAsAsync<List<dynamic>>();
                        emData = rs;
                        return emData;
                    }
                    else
                    {
                        return emData;
                    }
                }

            }
            catch (Exception)
            {
                return emData;
            }
        }
    }
}
