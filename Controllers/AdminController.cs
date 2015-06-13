using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeAppsDownload.Models;
using System.IO;

namespace FreeAppsDownload.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        IAdminRepository repost;
		public AdminController(IAdminRepository rep)
		{
			repost = rep;
		}
        public ActionResult Index()
        {
			//check whether user Login
			if (Session["admin"] == null)
			{

				return RedirectToAction("Login");
			}
			return	View();
            
        }
        public ActionResult Login()
        {
            Admin newUser = new Admin();
            return View(newUser);
        }
		public ActionResult LoginCheck(Admin user)
		{			
			
			Admin newUser = repost.chckAdmin(user);
			
			if (newUser != null)
			{
			  Session["admin"] = newUser;
			  return RedirectToAction("Index");
			}
			
			return RedirectToAction("Login");
		}
        public ActionResult ViewApps()
        {

			
			ViewBag.comment = repost.getAllComment();
            ViewBag.subscribe = repost.getSub();
            return View(repost.getAllApplication());
        }
        public ActionResult DeleteSub(int id)
        {
            repost.deleteSub(id);
            return RedirectToAction("ViewApps","Admin");
        }
		public ActionResult DeleteApp(int id)
		{
            repost.DeleteApp(id);
			
			return RedirectToAction("ViewApps");
		}
		public ActionResult UpdateApp(int id)
		{

            ViewBag.cat = repost.getCat();
			return View(repost.getApp(id));
		}
		public ActionResult UpdateAppContent(PostApp post)
		{
		 try
           {
            if (post.file.ContentLength > 0 && post.file != null && post.image1.ContentLength > 0 && post.image1 != null && post.image2.ContentLength > 0 && post.image2 != null && post.image3.ContentLength > 0 && post.image3 != null)
            {


				    int id = post.Id;

                    Application ap = repost.getApp(id);
                    ap.name = post.title;
                    ap.desc = post.desc;
                    ap.category = post.cat;
                    ap.OS = post.os;


                    if (post.os.Equals("android"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/Android"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/Android"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/Android/"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title+ post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/Android"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.file = fileName;
                        ap.date = DateTime.Now.ToString();
                        repost.UpdateApp(ap);
                        ViewBag.status = "Application Updated";




                    }
                    else if (post.os.Equals("ios"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat +"_"+ "image1"+ "_"+ Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.file = fileName;

                        ap.date = DateTime.Now.ToString();
                        repost.UpdateApp(ap);

						ViewBag.status = "Application Updated";

                    }
                    else if (post.os.Equals("wos"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Gallery/WOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/WOS" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.file = fileName;

                        ap.date = DateTime.Now.ToString();
                        repost.UpdateApp(ap);
						ViewBag.status = "Application Updated";

                    }
                    else if (post.os.Equals("bbos"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.image1 = fileName;

                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.file = fileName;

                        ap.date = DateTime.Now.ToString();
                        repost.UpdateApp(ap);
						ViewBag.status = "Application Updated";

                    }
                    else if (post.os.Equals("bos"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.file = fileName;
                        ap.date = DateTime.Now.ToString();

                        ap.date = DateTime.Now.ToString();
                        repost.UpdateApp(ap);
						ViewBag.status = "Application Updated";
                    }
                    else
                        ViewBag.status = "Application not Updated";
               
            }
            else
                ViewBag.status = "Please Fill the required Feilds";
           }
           catch (Exception ex)
           {
               ViewBag.status = "Application not added Exception";
           }
		return RedirectToAction("ViewApps");
        }
		public ActionResult DeleteComment(int id)
		{
            repost.DeleteComment(id);
			return RedirectToAction("ViewApps");
		}
        public ActionResult AddAdmin()
        {
            return View();
        }
		public ActionResult Add_Admin(Admin admin)
		{
			admin.date = DateTime.Now.ToString();
            repost.Add_Admin(admin);
			ViewBag.status = "Admin Successfully Added";
			return RedirectToAction("AddAdmin");
		}
        public ActionResult ViewAdminAndUser()
        {

            if (Session["admin"] == null)
			{
				return RedirectToAction("Login");
			}

            ViewBag.user = repost.getAllUser();
            return View(repost.getAllAdmin());
        }
		public ActionResult DeleteAdmin(int id)
		{
			repost.deleteAdmin(id);
			
			return RedirectToAction("ViewAdminAndUser");
		}
		public ActionResult UpdateAdmin(int id)
		{
			
			return View(repost.getAdmin(id));
		}
		public ActionResult Update_Admin(Admin admin)
		{
			admin.date = DateTime.Now.ToString();
            repost.UpdateAdmin(admin);
			return RedirectToAction("ViewAdminAndUser");
		}
		public ActionResult DeleteUser(int id)
		{
            repost.deleteUser(id);
			return RedirectToAction("ViewAdminAndUser");
		}
		public ActionResult AdminProfile()
		{

			return View((Admin)Session["admin"]);
		}
        public ActionResult Add_App()
        {
            
            return View(repost.getCat());
        }
        [HttpPost]
        public ActionResult Post_App(PostApp post)
        {
           try
           {
            if (post.file.ContentLength > 0 && post.file != null && post.image1.ContentLength > 0 && post.image1 != null && post.image2.ContentLength > 0 && post.image2 != null && post.image3.ContentLength > 0 && post.image3 != null)
            {
					
					Application ap = new Application();
                    ap.name = post.title;
                    ap.desc = post.desc;
                    ap.category = post.cat;
                    ap.OS = post.os;
                    ap.download = 0;
					

                    if (post.os.Equals("android"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/Android"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/Android"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/Android/"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title+ post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/Android"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/Android/" + fileName;
                        ap.file = fileName;
                        ap.date = DateTime.Now.ToString();
                        repost.AddApp(ap);
                        ViewBag.status = "Application added";




                    }
                    else if (post.os.Equals("ios"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat +"_"+ "image1"+ "_"+ Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/IOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.file = fileName;

                        ap.date = DateTime.Now.ToString();
                        repost.AddApp(ap);

                        ViewBag.status = "Application  added";

                    }
                    else if (post.os.Equals("wos"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/WOS" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/WOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/IOS/" + fileName;
                        ap.file = fileName;

                        ap.date = DateTime.Now.ToString();
                        repost.AddApp(ap);
                        ViewBag.status = "Application added";

                    }
                    else if (post.os.Equals("bbos"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.image1 = fileName;

                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BBOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/BBOS/" + fileName;
                        ap.file = fileName;

                        ap.date = DateTime.Now.ToString();
                        repost.AddApp(ap);
                        ViewBag.status = "Application added";

                    }
                    else if (post.os.Equals("bos"))
                    {
                        //image 1 code
                        String fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image1" + "_" + Path.GetFileName(post.image1.FileName);
                        String path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.image1.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.image1 = fileName;
                        //image 2 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image2" + "_" + Path.GetFileName(post.image2.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.image2.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.image2 = fileName;

                        //image 3 code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "image3" + "_" + Path.GetFileName(post.image3.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.image3.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.image3 = fileName;

                        //File save code
                        fileName = post.title + "_" + post.os + "_" + post.cat + "_" + "File" + "_" + Path.GetFileName(post.file.FileName);
                        path = Path.Combine(Server.MapPath("~/Images/Gallery/BOS"), fileName);
                        post.file.SaveAs(path);
                        fileName = "~/Images/Gallery/BOS/" + fileName;
                        ap.file = fileName;
                        ap.date = DateTime.Now.ToString();

                        ap.date = DateTime.Now.ToString();
                        repost.AddApp(ap);
                        ViewBag.status = "Application added";
                    }
                    else
                        ViewBag.status = "Application not added";
               
            }
            else
                ViewBag.status = "Please Fill the required Feilds";
           }
           catch (Exception ex)
           {
               ViewBag.status = "Application not added Exception";
           }

            return RedirectToAction("Add_App","Admin");
        }
		public ActionResult Logout()
		{


			Session["admin"] = null;
			Session.RemoveAll();
			Session.Abandon();

			return RedirectToAction("Login");
		}
        public ActionResult Categories()
        {
            return View(repost.getCat());
        }

        public ActionResult AddCategory(Category cat)
        {
            try
            {
                cat.Category1 = cat.Category1.ToUpper();
                cat.value = cat.value.ToLower();
                cat.date = DateTime.Now.ToString();
                if(repost.AddCat(cat))
                {
                    TempData["status"] = "Added Successfullly";
                }
                else
                    TempData["status"] = "Not Added.. :(";
            }
            catch(Exception ex)
            {
                TempData["status"] = "Conversion Error";
            }
            return RedirectToAction("Categories", "Admin");
        }
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                if (repost.deleteCat(id))
                {
                    TempData["status"] = "Deleted Successfullly";
                }
                else
                    TempData["status"] = "Not Deleted.. :(";
            }
            catch (Exception ex)
            {
                TempData["status"] = "Exception occure";
            }
            return RedirectToAction("Categories", "Admin");
        }

       
        public ActionResult UpdateCategory(int id)
        {

 
            Category cat = repost.getCat(id);
            cat.Category1 = cat.Category1.Trim();
            cat.value = cat.value.Trim();
            TempData["update"] = cat;
            return RedirectToAction("Categories","Admin");
        }
        public ActionResult Update_Category(Category cat)
        {

            cat.date = DateTime.Now.ToString();
            Category catold = repost.getCat(cat.Id);
            if(repost.UpdateCat(cat, catold))
            {
                TempData["status"] = "Updated Successfullly";
            }
            else
                TempData["status"] = "Not Updated.. :(";
            
            return RedirectToAction("Categories", "Admin");
        }
    }


    public class PostApp
    {
		public int Id { get; set; }
        public String title { set; get; }
        public String desc { set; get; }
        public String os { set; get; }
        public String cat { set; get; }
        public HttpPostedFileBase image1 { set; get; }
        public HttpPostedFileBase image2 { set; get; }
        public HttpPostedFileBase image3 { set; get; }

        public HttpPostedFileBase file { set; get; }

   }
}
