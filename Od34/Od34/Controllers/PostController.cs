using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Od34.Models;
using System.IO;
using DevExpress.Web;

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


                UploadedFile[] up_files = UploadControlExtension.GetUploadedFiles("UploadControl", PostControllerUploadControlSettings.UploadValidationSettings);

                if (up_files[0].IsValid)
                {
                    // Save uploaded file to some location
                    string file_name = Guid.NewGuid().ToString("N") + Path.GetExtension(up_files[0].FileName);
                    string file_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/UploadImages/");
                    string file_full_path = file_path + file_name;
                    up_files[0].SaveAs(file_full_path);
                    post._post_entity.header_image = file_name;
                }

                post._post_entity.title = collection["title"];
                post._post_entity.meta_title = collection["meta_title"];                         
                post._post_entity.description = collection["description"];
                post._post_entity.meta_description = collection["meta_description"];
                post._post_entity.post_body = HtmlEditorExtension.GetHtml("HtmlEditor");
                post._post_entity.dbcreate = DateTime.Now;
                post._post_entity.status = Convert.ToInt32(collection["status"]);                

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

        public ActionResult HtmlEditorPartial()
        {
            return PartialView("_HtmlEditorPartial");
        }
        public ActionResult HtmlEditorPartialImageSelectorUpload()
        {
            HtmlEditorExtension.SaveUploadedImage("HtmlEditor", PostControllerHtmlEditorSettings.ImageSelectorSettings);
            return null;
        }
        public ActionResult HtmlEditorPartialImageUpload()
        {
            HtmlEditorExtension.SaveUploadedFile("HtmlEditor", PostControllerHtmlEditorSettings.ImageUploadValidationSettings, PostControllerHtmlEditorSettings.ImageUploadDirectory);
            return null;
        }

        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControl", PostControllerUploadControlSettings.UploadValidationSettings, PostControllerUploadControlSettings.FileUploadComplete);
            return null;
        }
    }
    public class PostControllerUploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                // Save uploaded file to some location
                string file_name = Guid.NewGuid().ToString("N") + Path.GetExtension(e.UploadedFile.FileName);
                string file_path = HttpContext.Current.Server.MapPath("~/Content/UploadImages/");
                string file_full_path = file_path + file_name;                
                e.UploadedFile.SaveAs(file_full_path, false);
                e.CallbackData = file_name;
            }
        }
    }

    public class PostControllerHtmlEditorSettings
    {
        public const string ImageUploadDirectory = "~/Content/UploadImages/";
        public const string ImageSelectorThumbnailDirectory = "~/Content/Thumb/";

        public static DevExpress.Web.UploadControlValidationSettings ImageUploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".png" },
            MaxFileSize = 4000000
        };

        static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings imageSelectorSettings;
        public static DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings ImageSelectorSettings
        {
            get
            {
                if (imageSelectorSettings == null)
                {
                    imageSelectorSettings = new DevExpress.Web.Mvc.MVCxHtmlEditorImageSelectorSettings(null);
                    imageSelectorSettings.Enabled = true;
                    imageSelectorSettings.UploadCallbackRouteValues = new { Controller = "Post", Action = "HtmlEditorPartialImageSelectorUpload" };
                    imageSelectorSettings.CommonSettings.RootFolder = ImageUploadDirectory;
                    imageSelectorSettings.CommonSettings.ThumbnailFolder = ImageSelectorThumbnailDirectory;
                    imageSelectorSettings.CommonSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" };
                    imageSelectorSettings.UploadSettings.Enabled = true;
                }
                return imageSelectorSettings;
            }
        }
    }

}
