using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tek4TV_BTN.API;
using Tek4TV_BTN.Models;

namespace Tek4TV_BTN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        HomeApi api = new HomeApi();
        public HomeController()
        {
            //  _menu = menu;
        }
        public IActionResult Index()
        {
            ViewBag.Menus = api.GetPlaylist("menu_f61fb9fe-d512-44f9-a30a-111c31e71f86").Result;
            ViewBag.Weather = api.GetObject("weather").Result;
            ViewBag.Trend = api.GetObjectDev("fe4814ab-dce0-4d74-84e8-3a198f08dee9").Result;
            ViewBag.Headline = api.GetPlaylist("hightlight").Result;
            ViewBag.Live = api.GetObject("aa13ae69-3ae9-46e3-ab6a-17b6aef3cd66").Result;
            ViewBag.Home = api.GetPlaylist("group_home_1").Result;
            ViewBag.Popcast = api.GetObjectDev("d3514cc0-1514-4a57-a4ce-c436da1db2fd").Result;
            ViewBag.Radio = api.GetObjectDev("77f07e83-1dd2-4a20-b05e-c9e1ae7675f7").Result;
            ViewBag.Look = api.GetObject("24d6f593-f44e-451b-8901-0f250febc495").Result;
            return View();
        }
        [Route("chuyen-muc/{PrivateKey}")]
        public IActionResult HomeLevel2(string PrivateKey)
        {
            List<dynamic> listData = new List<dynamic>();
            ViewBag.Menus = api.GetPlaylist("menu_f61fb9fe-d512-44f9-a30a-111c31e71f86").Result;
            ViewBag.Weather = api.GetObject("weather").Result;
            ViewBag.Trend = api.GetObjectDev("fe4814ab-dce0-4d74-84e8-3a198f08dee9").Result;
            ViewBag.Category = api.GetObject(PrivateKey).Result;
            ViewBag.Video = api.GetObject("a55c4ca4-164b-4483-8313-d81d0f22ca70").Result;
            foreach (var item in ViewBag.Category.Components)
            {
                string key = item.PrivateKey;
                listData.Add(api.GetObject(key).Result);
            }
            ViewBag.Data = listData;
            return View();
        }
    }
}