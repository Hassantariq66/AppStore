using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAppsDownload.Models
{
    public class AdminRepository:IAdminRepository
    {
        
        private Database1Entities cx = new Database1Entities();
        public Admin chckAdmin(Admin user)
        {
            Admin newUser = null;
            try
            {
                newUser = cx.Admins.First(x => (user.username.Equals(x.username.Trim()) && user.password.Equals(x.password.Trim())));
            }
            catch (Exception ex)
            {

            }
            return newUser;

        }
        public List<Comment> getAllComment()
        {
           return  cx.Comments.ToList();
        }
        public List<Application> getAllApplication()
        {
            return cx.Applications.ToList();
        }
        public void AddApp(Application ap)
        {
            cx.Applications.Add(ap);
            cx.SaveChanges();
        }
        public void DeleteApp(int id)
        {
            Application ap = cx.Applications.First(x => x.Id == id);
            List<Comment> cmt = null;
            cmt=cx.Comments.Where(x => x.appid == id).ToList();
            foreach(Comment c in cmt)
            {
                cx.Comments.Remove(c);
            }
            cx.Applications.Remove(ap);
            cx.SaveChanges();
        }
        public Application getApp(int id)
        {
            return cx.Applications.First(x => x.Id == id);
        }
        public void UpdateApp(Application ap)
        {
            cx.Entry(ap).State = System.Data.EntityState.Modified;
            cx.SaveChanges();
        }
        public void DeleteComment(int id)
        {
            Comment ap = cx.Comments.First(x => x.Id == id);
            cx.Comments.Remove(ap);
            cx.SaveChanges();
        }
        public void Add_Admin(Admin admin)
        {
            cx.Admins.Add(admin);
            cx.SaveChanges();
        }
        public List<User> getAllUser()
        {
           return cx.Users.ToList();
        }
        public List<Admin> getAllAdmin()
        {
            return cx.Admins.ToList();
        }
        public void deleteAdmin(int id)
        {
            Admin ap = cx.Admins.First(x => x.Id == id);
            cx.Admins.Remove(ap);
            cx.SaveChanges();
        }
        public Admin getAdmin(int id)
        {
            return cx.Admins.First(x => x.Id == id);
        }
        public void UpdateAdmin(Admin admin)
        {
            cx.Entry(admin).State = System.Data.EntityState.Modified;
            cx.SaveChanges();
        }

        public void deleteUser(int id)
        {
            User ap = cx.Users.First(x => x.Id == id);
            List<Comment> cmt = null;
            cmt = cx.Comments.Where(x => x.username.Trim()==ap.username.Trim()).ToList();
            foreach (Comment c in cmt)
            {
                cx.Comments.Remove(c);
            }
            cx.Users.Remove(ap);
            cx.SaveChanges();
        }
        public List<Category> getCat()
        {
            List<Category> cs = cx.Categories.ToList();
            foreach (Category c in cs)
            {
                c.Category1 = c.Category1.Trim();
                c.value = c.value.Trim();
            }
            return cs;
        }
        public  Boolean AddCat(Category cat)
        {
            try
            {
                cx.Categories.Add(cat);
                cx.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
       public Category getCat(int id)
        {
            return (cx.Categories.First(x => x.Id == id));
        }
       public Boolean deleteCat(int id)
       {
            try
            {
                Category cat = cx.Categories.First(x => x.Id == id);
                List<Application> app = cx.Applications.Where(x => x.category.Trim().Equals(cat.value.Trim())).ToList();
                foreach(Application ap in app)
                {
                    cx.Applications.Remove(ap);
                }
                cx.Categories.Remove(cx.Categories.First(x=>x.Id==id));
                cx.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
       }
       public Boolean UpdateCat(Category catnew,Category catold)
       {                                               
           try
           {
               List<Application> app = cx.Applications.Where(x => x.category.Trim().Equals(catold.value.Trim())).ToList();
               foreach (Application ap in app)
               {
                   ap.category = catnew.value;
                   cx.Entry(ap).State = System.Data.EntityState.Modified;
               }
               catold.Category1 = catnew.Category1;
               catold.value = catnew.value;
               cx.Entry(catold).State = System.Data.EntityState.Modified;
               cx.SaveChanges();
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
      public List<Subscribe> getSub()
       {
          return cx.Subscribes.ToList();
       }
      public void deleteSub(int id)
      {
          cx.Subscribes.Remove(cx.Subscribes.First(x => x.Id == id));
          cx.SaveChanges();
      }

    }
}