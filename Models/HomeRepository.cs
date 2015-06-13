using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreeAppsDownload.Models;
using PagedList;

namespace FreeAppsDownload.Models
{
	public class HomeRepository:IHomeRepository
	{
		private Database1Entities cx = new Database1Entities();
		public List<ApplicationAndComment> getIndexList()
		{
			List<Application> std = cx.Applications.OrderByDescending(x => x.Id).ToList();
			// just populating the list
			int count = 0;
			List<ApplicationAndComment> cs = new List<ApplicationAndComment>();
			List<ApplicationAndComment> temp = new List<ApplicationAndComment>();
			if (std.Count >= 3)
			{
				count = 3;

			}
			else if(std.Count==2)
			{
                std = std.Concat(std).ToList();
				count = std.Count;
			}
            else if(std.Count==1)
            {
                std = std.Concat(std).Concat(std).ToList();
                count = std.Count;
            }
          
			for (int i = 0; i < count; i++)
			{

				var appid = std[i].Id;
				List<Comment> com = cx.Comments.Where(x => x.appid == appid).ToList();
				ApplicationAndComment appandcomment = new ApplicationAndComment();
				appandcomment.application = std[i];
				appandcomment.comment = com;
				cs.Add(appandcomment);
			}
			count = 0;
            try
            {
                ApplicationAndComment highest = cs[0];
                for (int i = 0; i < std.Count; i++)
                {
                    var appid = std[i].Id;
                    List<Comment> com = cx.Comments.Where(x => x.appid == appid).ToList();
                    ApplicationAndComment appandcomment = new ApplicationAndComment();
                    appandcomment.application = std[i];
                    appandcomment.comment = com;
                    if (count < com.Count)
                    {
                        temp.Add(highest);
                        highest = appandcomment;
                        count = com.Count;
                    }
                    else
                    {
                        temp.Add(appandcomment);
                    }
                }
                cs.Add(highest);
                highest = temp[0];
                for (int i = 0; i < temp.Count; i++)
                {

                    List<Comment> com = temp[i].comment;
                    ApplicationAndComment appandcomment = new ApplicationAndComment();
                    appandcomment.application = temp[i].application;
                    appandcomment.comment = com;
                    if (count < com.Count)
                    {
                        highest = appandcomment;
                        count = com.Count;
                    }

                }
                cs.Add(highest);
            }catch(Exception ex)
            {
                cs = new List<ApplicationAndComment>();
            }
		   return cs;
		}
        public List<ApplicationAndComment> getMostDownloded()
        {
            List<ApplicationAndComment> cs = new List<ApplicationAndComment>();
            try
            {
                List<Application> std = cx.Applications.OrderByDescending(x => x.download).ToList();
                
                int count = 0;
                if (std.Count >= 3)
                {
                    count = 3;
                }
                else if (std.Count == 2)
                {
                    std = std.Concat(std).ToList();
                    count = std.Count;
                }
                else if (std.Count == 1)
                {
                    std = std.Concat(std).Concat(std).ToList();
                    count = std.Count;
                }
                for (int i = 0; i < count; i++)
                {

                    var appid = std[i].Id;
                    List<Comment> com = cx.Comments.Where(x => x.appid == appid).ToList();
                    ApplicationAndComment appandcomment = new ApplicationAndComment();
                    appandcomment.application = std[i];
                    appandcomment.comment = com;
                    cs.Add(appandcomment);
                }
            }catch(Exception ex)
            {

            }
            return cs;
        }

		public User CheckUser(User user)
		{
			User newUser = null;
			try
			{
				newUser = cx.Users.First(x => (user.username.Equals(x.username.Trim()) || x.email.Trim().Equals(user.email)));
			}
			catch (Exception e)
			{
			}
			return newUser;
		}
		public void AddUser(User user)
		{
			cx.Users.Add(user);

			cx.SaveChanges();
			
		}
		public Boolean UpdateUser(User user)
		{
			try
			{
				cx.Entry(user).State = System.Data.EntityState.Modified;
				cx.SaveChanges();
				return true;
			}catch(Exception ex)
			{
				return false;
			}
		}

		public ApplicationAndComment getApplicationAndComment(int id)
		{
			Application product = cx.Applications.First(x => x.Id == id);

			List<Comment> com = cx.Comments.Where(x => x.appid == id).ToList();
			ApplicationAndComment cs = new ApplicationAndComment();
			cs.application = product;
			cs.comment = com;
			return cs;
		}
		public Boolean saveComment(Comment cmt)
		{
			try
			{
                List<Comment> coments = cx.Comments.Where(x => (x.username == cmt.username && x.appid==cmt.appid)).ToList();
                if(coments.Count>0)
                {
                    return false;
                }
				cx.Comments.Add(cmt);
				cx.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
		public List<Application> getViewAppsList(String os,String cat)
        {
            List<Application> std = null;
            try
            {
                 std = cx.Applications.Where(x => (x.category.Equals(cat) && x.OS.Equals(os))).Select(x => x).ToList();
            }catch(Exception ex)
            {

            }
			if (os == null && cat == null)
			{
				std = cx.Applications.ToList();
			}
			else if (os != null && cat == null)
			{
				std = cx.Applications.Where(x => (x.name.Contains(os))).Select(x => x).ToList();
			}
            else if(os==null && cat!=null)
            {
                std = cx.Applications.Where(x => (x.OS.Trim().Equals(cat.Trim()))).Select(x => x).ToList();
            }
			return std;
		}

        public List<Application> getViewAppsList(String os, String cat,int rating,string name)
        {
            List<Application> std=new List<Application>();
            if (name.Length > 0)
            {
               std  = cx.Applications.Where(x => (x.category.Equals(cat) && x.OS.Equals(os) && x.name.Trim().Contains(name.Trim()))).ToList();

            }
            else
                std = cx.Applications.Where(x => (x.category.Equals(cat) && x.OS.Equals(os))).ToList();
            List<Application> newlist = new List<Application>();
            foreach(var s in std)
            {
                List<Comment> com = cx.Comments.Where(x => x.appid ==s.Id).ToList();
                int avergae = 0;
                
                foreach(var c in com)
                {
                    avergae+=c.rating;

                }
                try
                {
                    avergae = avergae / com.Count;
                }catch(Exception ex)
                {
                    avergae = 0;
                }
                if(avergae>=rating)
                {
                    newlist.Add(s);
                }

            }
            return newlist;
        }
		public Application getApplication(int id)
		{
			return cx.Applications.First(x => x.Id == id);
		}
        public void IncreaseDownload(int id)
        {
             Application ap=cx.Applications.First(x => x.Id == id);
             ap.download = ap.download+1 ;
             cx.Entry(ap).State = System.Data.EntityState.Modified;
             cx.SaveChanges();
        }
        public Boolean checkSubscribe(Subscribe s)
        {
            List<Subscribe> sub=null;
            List<User> user=null ;
            sub= cx.Subscribes.Where(x => x.email.Trim() == s.email.Trim()).ToList();
            user = cx.Users.Where(x => x.email.Trim() == s.email.Trim()).ToList();
            if (sub.Count==0  && user.Count==0 )
            {
                s.date = DateTime.Now.ToString();
                cx.Subscribes.Add(s);
                cx.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public List<Category> getCat()
        {
           List<Category> cs= cx.Categories.ToList();
            foreach(Category c in cs)
            {
                c.Category1 = c.Category1.Trim();
                c.value = c.value.Trim();
            }
            return cs;
        }
       
	}
                            
}