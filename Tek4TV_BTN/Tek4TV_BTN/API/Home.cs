using System.Net.Http.Headers;

namespace Tek4TV_BTN.API
{
    public class HomeApi
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

        public async Task<dynamic> GetObject(string id)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var _domain = config["Domain:url"];
            var emData = new Object();
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
                        var rs = await responseMessage.Content.ReadAsAsync<dynamic>();
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

        public async Task<dynamic> GetPlaylistDev(string id)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var _domain = config["Domain:devurl"];
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

        public async Task<dynamic> GetObjectDev(string id)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var _domain = config["Domain:devurl"];
            var emData = new Object();
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
                        var rs = await responseMessage.Content.ReadAsAsync<dynamic>();
                        emData = rs;
                        return emData;
                    }
                    else
                    {
                        return emData;
                    }
                }

            }
            catch (Exception ex)
            {
                return emData;
            }


        }

        public async Task<dynamic> GetVideo(int type, int page, int size)
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
                    string url = $"api/Video/content/" + type + "/" + page + "/" + size;
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
