//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeAppsDownload.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public Application()
        {
            this.download = 0;
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string OS { get; set; }
        public string desc { get; set; }
        public string image1 { get; set; }
        public string image2 { get; set; }
        public string image3 { get; set; }
        public string file { get; set; }
        public string date { get; set; }
        public Nullable<int> download { get; set; }
    }
}
