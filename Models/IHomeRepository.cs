using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAppsDownload.Models
{
	public interface IHomeRepository
	{
		
	  List<ApplicationAndComment> getIndexList();
      List<ApplicationAndComment> getMostDownloded();
	  User CheckUser(User user);
	  void AddUser(User user);
	  Boolean UpdateUser(User user);

	  ApplicationAndComment getApplicationAndComment(int id);
	  Boolean saveComment(Comment cmt);

	  List<Application> getViewAppsList(String os,String cat);
	  Application getApplication(int id);
      void IncreaseDownload(int id);
      Boolean checkSubscribe(Subscribe s);
      List<Category> getCat();
      List<Application> getViewAppsList(String os, String cat, int rating, string name);
	}
	
}
