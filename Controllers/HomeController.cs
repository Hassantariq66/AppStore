using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeAppsDownload.Models;
using PagedList;



namespace FreeAppsDownload.Controllers
{

    public class HomeController : Controller
    {
        //
        // GET: /Home/
		IHomeRepository repost;

        public List<Category>cat = new List<Category>();
		public HomeController(IHomeRepository rep)
		{

			repost = rep;
               
            cat=rep.getCat();
          // HttpContext.Session.Add("cat",cat);

		}

        public ActionResult Index()
        {
           
            Session["category"] = cat;
            if(repost.getIndexList().Count<=0)
            {
                TempData["noApp"] = true;
            }
            else
            {
                TempData["noApp"] = null;
            }
			ViewBag.list = repost.getIndexList();
            ViewBag.download = repost.getMostDownloded();
			return View();
            
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginCheck(User user)
        {

			User newUser = repost.CheckUser(user);
			if (newUser != null)
			{
				Session["user"] = newUser;
			}
			else
			{

				Session["user"] = null;
				return RedirectToAction("Login");
			}
            if(Session["comment"]!=null)
            {
         
                int id = (int)Session["comment"];
                Session["comment"] = null;
                return RedirectToAction("Single", "Home", new { id = id});
            }
			return RedirectToAction("Update");
        }
		public ActionResult Logout()
		{
			
			
				Session["user"] = null;
			//	Session.RemoveAll();
			//	Session.Abandon();
             //   Session["category"] = cat;
				return RedirectToAction("Login");
		}
        public ActionResult Register(User user)
        {
			ViewBag.Message = null;
            return View();
        }
		public ActionResult Update()
		{
			return View(Session["user"]);
		}
		public ActionResult RegisterCheck(User user)
        {

			
			User newUser = repost.CheckUser(user);
			
			if(newUser!=null)
			{
				ViewBag.Message="User Already Exist";
				return View("Register");
			}
			user.date = DateTime.Now.ToString();

			repost.AddUser(user);
			Session["user"] =user;
			ViewBag.Message = "User Added";
            if (Session["comment"] != null)
            {

                int id = (int)Session["comment"];
                Session["comment"] = null;
                return RedirectToAction("Single", "Home", new { id = id });
            }
            return RedirectToAction("Update");
        }

		public ActionResult UpdateUser(User user)
		{

			var userid=user.Id;

			user.date = DateTime.Now.ToString();

			if (repost.UpdateUser(user))
			{
				Session["user"] = user;
				ViewBag.Message = "Successfully Updated";
			}
			else
			{
				ViewBag.Message = "Updated Failed";
			}

			return View("Update", Session["user"]);
		}
        public ActionResult Single(int id)
        {		 
             return View(repost.getApplicationAndComment(id));
        }
		public ActionResult submitComment(Comment cmt)
		{
			User user = (User)Session["user"];

			if (user == null)
			{
                Session["comment"] = cmt.appid;
				return	RedirectToAction("Login", "Home");
			}
			else
			{
				cmt.username = user.username.Trim();
				cmt.date = DateTime.Now.ToString();
				if (repost.saveComment(cmt))
				{
					return RedirectToAction("Single", "Home", new { id = cmt.appid });
				}
                else
                {
                    cmt.descr = user.password;
                    Session["review"] = user;
                }
			}

			return RedirectToAction("Single", "Home", new { id = cmt.appid });	

			
		}

        public JsonResult CheckUserName(String username)
        {


            Database1Entities cx = new Database1Entities();
            User user=null;
            try{
                user = cx.Users.First(x => x.username.Trim() == username.Trim());
            } catch(Exception ex)
            {

            }
            if(user==null)
            {
                return this.Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return this.Json(false, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CheckEmail(String email)
        {


            Database1Entities cx = new Database1Entities();
            User user = null;
            try
            {
                user = cx.Users.First(x => x.email.Trim() == email.Trim());
            }
            catch (Exception ex)
            {

            }
            if (user == null)
            {
                return this.Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return this.Json(false, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ViewApps(String os, String cat, int?rating,int? page, String a = null)
        {
			
				var OS = os;
				var Games = cat;

				var pageNumber = page ?? 1;
                var rat = rating ?? 0;
                List<Application> std = new List<Application>();
                if (rating >0 && a != null)
                {
                    std = repost.getViewAppsList(os, cat, rat, a);
                }
                else
                {
                    std = repost.getViewAppsList(os, cat);
                }
				var std1 = std.Concat(std).Concat(std);
               
                ViewBag.OnePageOfProducts =std1.ToPagedList(pageNumber, 9);
              

               
				ViewBag.os = os;
				ViewBag.cat = cat;
                ViewBag.rating = rating;
                ViewBag.a = a;
			
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Sort(List<Application> sort,String os,String cat,int id )
        {
            if (id == 1)
            {
               
                ViewBag.os = os;
                ViewBag.cat = cat;
               
            }
           
            return View();
        }
        public ActionResult subscribe(Subscribe s)
        {
            if(repost.checkSubscribe(s))
            {
                Session["subscribe"] = "Successfully Subscribe :)";
            }
            else
                Session["subscribe"] = "Subscribsion Failded!";
            
            return RedirectToAction("Index","Home");
        }
		public FileResult Download(int Id)
		{
			
			string contentType = "application/apk";


			Application ap = repost.getApplication(Id);
            repost.IncreaseDownload(Id);
			//Parameters to file are
			//1. The File Path on the File Server
			//2. The content type MIME type
			//3. The parameter for the file save by the browser
			return File(ap.file, contentType,  (ap.name.Trim()+".apk"));

		}
		

    }
    
}

