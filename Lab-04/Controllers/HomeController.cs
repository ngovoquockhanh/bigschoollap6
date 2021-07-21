using Lab_04.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_04.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BigSchoolContext context = new BigSchoolContext();
            var upcommingCourse = context.Courses.Where(p => p.Datetime > DateTime.Now).OrderBy(p => p.Datetime).ToList();
            /// LẤY user login hiện tại
            var userID = User.Identity.GetUserId();
            foreach (Course i in upcommingCourse)
            { 
                /// tim name của user từ lectureId 
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.LecturerID);
                i.Name = user.Name;
                /// llay ds tham gia khoa hoc
               
                if (userID != null)
                {
                    i.isLogin = true;

                   ///kt user đó  chưa tham gia khoa học
                    Attendance find = context.Attendances.FirstOrDefault(p => p.CourseId == i.Id && p.Attendee == userID);
                    if (find == null)


                        i.isShowGoing = true;
                    Following findFollow = context.Followings.FirstOrDefault(p => p.FollowerId == userID && p.FolloweeId == i.LecturerID);
                    if (findFollow == null)

                        i.isShowFollow = true;


                }

            }
            return View(upcommingCourse);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}