using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WizlearnTest.Models;

namespace WizlearnTest.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ValidateEmail(string Email)
        {
            try
            {
                WizlearnDBEntities db = new WizlearnDBEntities();
                List<Mst_Custormer> _userlist = db.Mst_Custormer.Where(a => a.mc_email == Email).ToList();
                if (_userlist != null && _userlist.Count > 0)
                {

                    return Json(new { login = true, success = true, data = "", message = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { login = true, success = false, data = "", message = "Please Signup First!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { login = false, success = false, data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoginProcess(string Email, string Password)
        {
            try
            {
                WizlearnDBEntities db = new WizlearnDBEntities();
                string _hashpass = MD5Hash(Password);
                List<Mst_Custormer> _userlist = db.Mst_Custormer.Where(a => a.mc_email.ToUpper() == Email.ToUpper() && a.mc_password== _hashpass && a.mc_isverify==1).ToList();
                if (_userlist != null && _userlist.Count > 0)
                {

                    return Json(new { login = true, success = true, data = "", message = "Verifyemail/Index" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { login = true, success = false, data = "", message = "Please Verify Email" }, JsonRequestBehavior.AllowGet);
                        }

            }
            catch (Exception ex)
            {
                return Json(new { login = false, success = false, data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}