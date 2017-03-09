using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Od34.Models;

namespace Od34.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<PostModel> posts = new List<PostModel>();
            posts = PostModel.GetAllPosts();
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}