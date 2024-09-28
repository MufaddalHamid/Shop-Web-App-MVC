using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool_Shop_Web_App.Common;
using Tool_Shop_Web_App.UserMode;

namespace Tool_Shop_Web_App.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckAdminUser(string userName, string password)
        {
            try
            {
                using (UserServices objUserServices = new UserServices())
                {
                    DataTable userDetails = objUserServices.FetchUserDetails(userName);
                    if (userDetails != null && userDetails.Rows.Count > 0)
                    {
                        return Json(new { success = true, status = true, message = "Logged In Successfully!!" });
                    }
                    else
                    {
                        return Json(new { success = true, status = false, message = "Login Failed!!" });
                    }
                }
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, status = false, message = ex.Message });
            }
        }

    }
}