using Core.Utilities;
using DataLayer.ViewModels.SideBar;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElevatorAdmin.ViewComponents
{
    public class SidebarComponent : ViewComponent
    {
        private readonly UsersAccessRepository _usersAccessRepository;
        private readonly UsersRoleRepository _usersRoleRepository;
        public SidebarComponent(UsersAccessRepository usersAccessRepository
            , UsersRoleRepository usersRoleRepository)
        {
            _usersAccessRepository = usersAccessRepository;
            _usersRoleRepository = usersRoleRepository;
        }


        public IViewComponentResult Invoke()
        {
            List<SidebarViewModel> items = new List<SidebarViewModel>();

            items.Add(new SidebarViewModel { Controller = "Home", Action = "Index", Title = "صفحه اصلی", Icon = "fa fa-bars" });
            items.Add(new SidebarViewModel { Area = "SiteSetting", Controller = "ManageSiteSetting", Action = "Index", Title = "تنظیمات سایت", Icon = "fa fa-align-left" });
            items.Add(new SidebarViewModel { Controller = "UserManage", Action = "Index", Title = "مدیریت کاربران", Icon = "fa fa-user" });
            items.Add(new SidebarViewModel { Controller = "RoleManage", Action = "Index", Title = "مدیریت نقش ها", Icon = "fa fa-users" });
            items.Add(new SidebarViewModel { Area = "SlideShow", Controller = "ManageSlideShow", Action = "Index", Title = "مدیریت اسلایدشو", Icon = "fa fa-sliders" });
            items.Add(new SidebarViewModel { Area = "LogoManufactory", Controller = "ManageLogoManufactory", Action = "Index", Title = "مدیریت لوگوها", Icon = "fa fa-sliders" });

            items.Add(new SidebarViewModel
            {
                Controller = "",
                Action = "",
                Title = "مدیریت پکیج ها",
                Icon = "fa fa-cubes",
                Childs = new List<SidebarChildViewModel> {
                    new SidebarChildViewModel {Area = "ProductPackage", Controller = "ManageProductPackageController", Action = "Index", Title = "مدیریت پکیج ‌ها" },
                    new SidebarChildViewModel {Area = "ProductPackage", Controller = "ManagePackageQuestionController", Action = "Index" , Title = "مدیریت سوالات پکیح" }
            }
            });


            items.Add(new SidebarViewModel
            {
                Controller = "",
                Action = "",
                Title = "مدیریت محصولات",
                Icon = "fa fa-cubes",
                Childs = new List<SidebarChildViewModel> {
                    new SidebarChildViewModel {Area = "ProductGroup", Controller = "ManageProductGroup", Action = "Index", Title = "مدیریت گروه‌ها" },
                    new SidebarChildViewModel {Area = "Feature", Controller = "ManageFeature", Action = "Index" , Title = "مدیریت ویژگی‌ها" },
                    new SidebarChildViewModel {Area = "ProductUnit", Controller = "ManageProductUnit", Action = "Index" , Title = "مدیریت واحد کالا‌ها" },
                    new SidebarChildViewModel {Area = "Product", Controller = "ManageProduct", Action = "Index" , Title = "مدیریت محصولات" },
                    new SidebarChildViewModel {Area = "ProductGroupDependency", Controller = "ManageProductGroupDependency", Action = "Index" , Title = "مدیریت وابستگی‌ها" },
                    new SidebarChildViewModel {Area = "ProductPackage", Controller = "ManageProductPackage", Action = "Index" , Title = "مدیریت پیکج" },
                    new SidebarChildViewModel {Area = "Condition", Controller = "ManageCondition", Action = "Index" , Title = "مدیریت شروط" }
            }
            });
            items.Add(
                new SidebarViewModel
                {
                    Controller = "",
                    Action = "",
                    Title = "مدیریت اخبار",
                    Icon = "fa fa-newspaper-o",
                    Childs = new List<SidebarChildViewModel> {
                        new SidebarChildViewModel {Area = "News", Controller = "ManageNews", Action = "Index", Title = "اخبار" },
                        new SidebarChildViewModel {Area = "NewsGroup", Controller = "ManageNewsGroup", Action = "Index", Title = "گروه اخبار" },
                    }
                }
                );

            items.Add(new SidebarViewModel { Area = "SuggestionsAndComplaint", Controller = "ManageSuggestionsAndComplaint", Action = "Index", Title = "شکایات و پیشنهادات", Icon = "fa fa-retweet" });

            items.Add(
                new SidebarViewModel
                {
                    Controller = "",
                    Action = "",
                    Title = "سوالات پرتکرار",
                    Icon = "fa fa-question",
                    Childs = new List<SidebarChildViewModel> {
                        new SidebarChildViewModel {Area = "FAQ", Controller = "ManageFAQ", Action = "Index", Title = "مدیریت سوالات" },
                        new SidebarChildViewModel {Area = "FaqGroup", Controller = "ManageFaqGroup", Action = "Index", Title = "مدیریت گروه‌ها" },

                    }
                }
                );


            //ViewBag.Controller = controller;
            //ViewBag.Action = action;
            //ViewBag.Icon = icon;
            //ViewBag.Title = title;

            return View(items);
        }
    }
}
