using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http.Headers;
using Tek4TV_BTN.API;
using Tek4TV_BTN.IServices;

namespace Tek4TV_BTN.Controllers
{
    public class DetailController : Controller
    {
        HomeApi api = new HomeApi();

        public DetailController(IMenuServices menu)
        {
        }

        [Route("/chi-tiet/{slug}-{Id}.htm")]
        public async Task<IActionResult> IndexAsync(int Id)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ViewBag.Menus = api.GetPlaylist("menu_f61fb9fe-d512-44f9-a30a-111c31e71f86").Result;
            ViewBag.Weather = api.GetObject("weather").Result;
            ViewBag.Trend = api.GetObjectDev("fe4814ab-dce0-4d74-84e8-3a198f08dee9").Result;
            using (HttpClient httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = config["Domain:url"] + "/api/Video/json/" + Id;
                var responseMessage = await httpClient.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseBody = await responseMessage.Content.ReadAsStringAsync();
                    dynamic output = JsonConvert.DeserializeObject(responseBody);

                    ViewBag.Data = responseBody;
                    var thumb = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/image_default.png";
                    ViewBag.TitileHome = output["Title"];
                    ViewBag.DescriptionHome = output.Description == null ? output["Title"] : output.Description;
                    ViewBag.WitdthHome = "1200";
                    ViewBag.HeightHome = "630";
                    int indexTag = 1; List<dynamic> tags = new List<dynamic>();
                    string ViewBagTags = "";

                    foreach (var img in output.Image)
                    {
                        if (img.Type == "lg")
                        {
                            thumb = img.Cdn + img.Url;
                        }

                    }
                    ViewBag.ThumbnailUrl = thumb;

                    dynamic video = new ExpandoObject();
                    video.Title = output.Title;
                    video.Name = output.Title;
                    video.Description = output.Description;
                    video.Thumbnail = thumb;
                    video.Path = output.Path;
                    List<dynamic> related = new List<dynamic>();
                    var slug = ""; string cate = "";
                    int contentType = 0; string author = "Hà Nội TV";
                    foreach (var item in output.Metadata)
                    {
                        if (item.Name == "Related")
                        {
                            string jsonString = item.Value;
                            foreach (var value in JsonConvert.DeserializeObject<dynamic>(item.Value.ToString()))
                            {
                                var reThumb = "/thumb-vnews.jpg";
                                foreach (var img in value.Image)
                                {
                                    if (img.Type == "Thumbnail")
                                    {
                                        reThumb = "https://dug0nmhkbevod.vcdn.cloud/" + img.Url;
                                    }

                                }
                                int contentType1 = value.ContentType;
                                dynamic related1 = new ExpandoObject();
                                related1.Title = value.Title;
                                related1.Link = value.Slug;
                                related1.Image = reThumb;

                                related.Add(related1);
                            }

                        }
                        if (item.Name == "SubjectandKeywords")
                        {
                            foreach (var i in item.Value)
                            {
                                tags.Add(i);
                            }
                        }

                        if (item.Name == "ContentType")
                        {
                            contentType = item.Value;
                        }
                        if (item.Name == "Slug")
                        {
                            slug = item.Value;
                        }
                        if (/*String.IsNullOrEmpty(video.Path) && */item.Name == "FileCode")
                        {
                            video.Path = item.Value;
                        }
                        if (item.Name == "SubjectandKeywords")
                        {
                            List<string> value = item.Value.ToObject<List<string>>();
                            foreach (var i in item.Value)
                            {
                                if (indexTag != value.Count)
                                {
                                    ViewBagTags += i + ", ";
                                    indexTag++;
                                }
                                else
                                {
                                    ViewBagTags += i;
                                    indexTag++;
                                }
                            }
                        }
                        if (item.Name == "Author")
                        {
                            author = item.Value;
                        }
                        if (item.Name == "DVBCategories")
                        {
                            cate = item.Value;
                        }
                    }
                    video.Author = author;
                    video.Category = cate;
                    video.Body = output.Body;
                    video.Tags = tags;
                    video.Schedule = output.Schedule.ToString("dd-MM-yyyy, HH:mm");
                    video.Related = related;
                    ViewBag.Url = Request.GetDisplayUrl();
                    video.Keyword = output.Keyword;
                    video.PrivateID = output.PrivateID;
                    video.Link = slug;
                    ViewBag.Tags = ViewBagTags;
                    ViewBag.videoId = Id;
                    ViewBag.Type = "VideoObject";

                    string keyWord = output.Keyword;
                    if (keyWord.Contains(","))
                    {
                        keyWord = keyWord.Split(",")[0];
                    }

                    ViewBag.Next = api.GetObject(keyWord).Result; ;
                    return View(video);
                }
                else
                {
                    return Redirect("/");
                }

            }
        }

        [Route("/chi-tiet/preview/{slug}-{Id}.htm")]
        public async Task<IActionResult> IndexPreviewAsync(int Id)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ViewBag.Menus = api.GetPlaylist("menu_f61fb9fe-d512-44f9-a30a-111c31e71f86").Result;
            ViewBag.Weather = api.GetObject("weather").Result;
            ViewBag.Trend = api.GetObjectDev("fe4814ab-dce0-4d74-84e8-3a198f08dee9").Result;
            using (HttpClient httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = config["Domain:CMS"] + "/redis/v1/preview/" + Id;
                var responseMessage = await httpClient.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseBody = await responseMessage.Content.ReadAsStringAsync();
                    dynamic output = JsonConvert.DeserializeObject(responseBody);

                    ViewBag.Data = responseBody;
                    var thumb = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/image_default.png";
                    ViewBag.TitileHome = output["Title"];
                    ViewBag.DescriptionHome = output.Description == null ? output["Title"] : output.Description;
                    ViewBag.WitdthHome = "1200";
                    ViewBag.HeightHome = "630";
                    int indexTag = 1; List<dynamic> tags = new List<dynamic>();
                    string ViewBagTags = "";

                    foreach (var img in output.Image)
                    {
                        if (img.Type == "lg")
                        {
                            thumb = img.Cdn + img.Url;
                        }

                    }
                    ViewBag.ThumbnailUrl = thumb;

                    dynamic video = new ExpandoObject();
                    video.Title = output.Title;
                    video.Name = output.Title;
                    video.Description = output.Description;
                    video.Thumbnail = thumb;
                    video.Path = output.Path;
                    List<dynamic> related = new List<dynamic>();
                    var slug = ""; string cate = "";
                    int contentType = 0; string author = "Hà Nội TV";
                    foreach (var item in output.Metadata)
                    {
                        if (item.Name == "Related")
                        {
                            string jsonString = item.Value;
                            foreach (var value in JsonConvert.DeserializeObject<dynamic>(item.Value.ToString()))
                            {
                                var reThumb = "/thumb-vnews.jpg";
                                foreach (var img in value.Image)
                                {
                                    if (img.Type == "Thumbnail")
                                    {
                                        reThumb = "https://dug0nmhkbevod.vcdn.cloud/" + img.Url;
                                    }

                                }
                                int contentType1 = value.ContentType;
                                dynamic related1 = new ExpandoObject();
                                related1.Title = value.Title;
                                related1.Link = value.Slug;
                                related1.Image = reThumb;

                                related.Add(related1);
                            }

                        }
                        if (item.Name == "SubjectandKeywords")
                        {
                            foreach (var i in item.Value)
                            {
                                tags.Add(i);
                            }
                        }

                        if (item.Name == "ContentType")
                        {
                            contentType = item.Value;
                        }
                        if (item.Name == "Slug")
                        {
                            slug = item.Value;
                        }
                        if (/*String.IsNullOrEmpty(video.Path) && */item.Name == "FileCode")
                        {
                            video.Path = item.Value;
                        }
                        if (item.Name == "SubjectandKeywords")
                        {
                            List<string> value = item.Value.ToObject<List<string>>();
                            foreach (var i in item.Value)
                            {
                                if (indexTag != value.Count)
                                {
                                    ViewBagTags += i + ", ";
                                    indexTag++;
                                }
                                else
                                {
                                    ViewBagTags += i;
                                    indexTag++;
                                }
                            }
                        }
                        if (item.Name == "Author")
                        {
                            author = item.Value;
                        }
                        if (item.Name == "DVBCategories")
                        {
                            cate = item.Value;
                        }
                    }
                    video.Author = author;
                    video.Category = cate;
                    video.Body = output.Body;
                    video.Tags = tags;
                    video.Schedule = output.Schedule.ToString("dd-MM-yyyy, HH:mm");
                    video.Related = related;
                    ViewBag.Url = Request.GetDisplayUrl();
                    video.Keyword = output.Keyword;
                    video.PrivateID = output.PrivateID;
                    video.Link = slug;
                    ViewBag.Tags = ViewBagTags;
                    ViewBag.videoId = Id;
                    ViewBag.Type = "VideoObject";

                    string keyWord = output.Keyword;
                    if (keyWord.Contains(","))
                    {
                        keyWord = keyWord.Split(",")[0];
                    }

                    ViewBag.Next = api.GetObject(keyWord).Result; ;
                    return View("Index", video);
                }
                else
                {
                    return Redirect("/");
                }

            }
        }

        [Route("/chi-tiet/tin-tuc/{slug}")]
        public IActionResult News(int id, string slug)
        {
            return View();
        }
        [Route("/chi-tiet/anh/{slug}")]
        public IActionResult Image(int id, string slug)
        {
            return View();
        }

        [Route("/chi-tiet/voice/{slug}")]
        public IActionResult Voice(int id, string slug)
        {
            return View();
        }
        [Route("/chi-tiet/video/{slug}")]
        public IActionResult Video(int id, string slug)
        {
            return View();
        }
    }
}
