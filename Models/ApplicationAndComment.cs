using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeAppsDownload.Models
{
	public class ApplicationAndComment
	{
		public Application application { get; set; }
		public List<Comment> comment { get; set; }
	}
}