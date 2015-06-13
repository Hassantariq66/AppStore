using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAppsDownload.Models
{
   public interface IAdminRepository
    {
       Admin chckAdmin(Admin user);
       List<Comment> getAllComment();
       List<Application> getAllApplication();
       void AddApp(Application ap);
       void DeleteApp(int id);
       Application getApp(int id);
       void UpdateApp(Application ap);
       void DeleteComment(int id);
       void Add_Admin(Admin admin);

       List<User> getAllUser();
       List<Admin> getAllAdmin();
       void deleteAdmin(int id);
       Admin getAdmin(int id);

       void UpdateAdmin(Admin admin);

       void deleteUser(int id);
       List<Category> getCat();
       Boolean AddCat(Category cat);
       Category getCat(int id);
       Boolean deleteCat(int id);
       Boolean UpdateCat(Category catnew,Category catold);
       List<Subscribe> getSub();
       void deleteSub(int id);
      

    }
}
