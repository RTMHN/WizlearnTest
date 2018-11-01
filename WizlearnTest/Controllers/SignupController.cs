using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WizlearnTest.Models;

namespace WizlearnTest.Controllers
{
    public class SignupController : BaseController
    {
        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadGender()
        {
            try
            {
                List<ComboBoxObject> oList = new List<ComboBoxObject>();
                ComboBoxObject o2 = new ComboBoxObject();
                o2.Text = "Select";
                o2.Value = "";
                oList.Add(o2);

                ComboBoxObject o3 = new ComboBoxObject();
                o3.Text = "Male";
                o3.Value = "Male";
                oList.Add(o3);
                ComboBoxObject o4 = new ComboBoxObject();
                o4.Text = "Female";
                o4.Value = "Female";
                oList.Add(o4);
                ComboBoxObject o5 = new ComboBoxObject();
                o5.Text = "Other";
                o5.Value = "Other";
                oList.Add(o5);
                return Json(new { success = true, login = true, data = oList }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { success = false, login = true, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UserSignup(string Firstname, string LastName, string FullName, string Mobile, string Address, string Gender
            , string Dob, string Password, string Email)
        {
            try
            {
                WizlearnDBEntities db = new WizlearnDBEntities();
                //save
                Mst_Custormer ob = new Mst_Custormer();
                ob.mc_fname = Firstname;
                ob.mc_lname = LastName;
                ob.mc_fullname = FullName;
                ob.mc_address = Address;
                ob.mc_createby = Firstname;
                ob.mc_create_dt = DateTime.Now;
                ob.mc_dob = Convert.ToDateTime(Dob);
                ob.mc_email = Email;
                ob.mc_isverify = 0;
                ob.mc_mobile = Mobile;
                ob.mc_password = MD5Hash(Password);
                db.Mst_Custormer.Add(ob);
                db.SaveChanges();
                SendMail(Email);
                Session["CurrentEmail"] = Email;
                return Json(new { login = true, success = true, data = "", message = "Singup Complete! Verify Email" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { login = false, success = false, data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void SendMail(string email)
        {
            string verifypath = Request.Url.AbsoluteUri;
            string[] _path = verifypath.Split('/');
            string _currentpath = _path[0] + "/" + _path[1] + "/" + _path[2] + "/Verifyemail/Verifiprocess?emailstr=" + "-" + email + "-" + MD5Hash(email);
            MailMessage loginInfo = new MailMessage();
            string em = email;
            loginInfo.To.Add(em.ToString());
            loginInfo.From = new MailAddress("wizlearntestsube@gmail.com");
            loginInfo.Subject = "Verify wizlearntest signup";
            loginInfo.Body = "Please Verify email , <br>" + "<a href=" + _currentpath + "> PleaseClick Here. </a>"; ;
            loginInfo.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("wizlearntestsube@gmail.com", "herathimsm");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(loginInfo);
        }
        public JsonResult ValidateEmail(string Email)
        {
            try
            {
                WizlearnDBEntities db = new WizlearnDBEntities();

                List<Mst_Custormer> _userlist = db.Mst_Custormer.Where(a => a.mc_email == Email).ToList();
                if (_userlist != null && _userlist.Count > 0)
                {

                    return Json(new { login = true, success = false, data = "", message = "Email already used!." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { login = true, success = true, data = "", message = "" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { login = false, success = false, data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //ValidateDOB
        public JsonResult ValidateDOB(string DOB)
        {
            try
            {
                DateTime temp;
                if (DateTime.TryParse(DOB, out temp))
                {
                    return Json(new { login = true, success = true, data = "", message = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { login = true, success = false, data = "", message = "Invalid Date" }, JsonRequestBehavior.AllowGet);
                }
               

            }
            catch (Exception ex)
            {
                return Json(new { login = false, success = false, data = "", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}