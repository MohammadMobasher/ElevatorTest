using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.SideBar
{
    public class SidebarViewModel
    {

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; } = "zmdi-view-dashboard";
        public List<SidebarChildViewModel> Childs { get; set; } = null;

    }


    public class SidebarChildViewModel
    {

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; } = "zmdi-view-dashboard";

    }
}