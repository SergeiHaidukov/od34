using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Od34.Models;

namespace Od34.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles ="admin")]
        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                PostModel post = new PostModel();

                post._post_entity.title = Request.Form["title"];
                post._post_entity.meta_title = Request.Form["meta_title"];
                post._post_entity.description = Request.Form["description"];
                post._post_entity.meta_description = Request.Form["meta_description"];
                post._post_entity.post_body = Request.Form["post_body"];
                post._post_entity.status = Convert.ToInt32(Request.Form["status"]);

                post.ModifyPost();

                return RedirectToAction("Create");

                //return RedirectToAction("Edit", new { id = post._post_entity.id_post });
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
