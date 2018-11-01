using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WizlearnTest.Models;

namespace WizlearnTest.Controllers
{
    public class VerifyemailController : BaseController
    {
        // GET: Verifyemail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Fail()
        {
            return View();
        }
        public ActionResult Verifiprocess(string emailstr)
        {
            try {
                
                string [] email = emailstr.Split('-');
                string stemail = email[1].ToString();
                string encryemail= email[2].ToString();
                if (MD5Hash(stemail) != encryemail)
                {
                  return  RedirectToAction("Fail");
                }
                using (var db = new WizlearnDBEntities())
                {
                    Mst_Custormer user = db.Mst_Custormer.Where(a=>a.mc_email== stemail).ToList()[0];
                    user.mc_isverify = 1;
                    db.Mst_Custormer.Attach(user);
                    db.Entry(user).Property(x => x.mc_isverify).IsModified = true;
                    db.SaveChanges();
                }

              return  RedirectToAction("Index", "Login", new { area = "" });

            } catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}