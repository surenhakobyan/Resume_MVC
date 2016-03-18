using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MVCCV.Models;

namespace MVCCV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ContactModel model = new ContactModel();
            ViewBag.Title = "My CV with MVC";
            return View(model);
        }

        [HttpPost]
        public ActionResult SendMail(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();

                    mail.To.Add("surojermuk@gmail.com");
                    mail.From = new MailAddress(model.Email);
                    mail.Subject = model.Subject;

                    mail.Body = String.Concat(model.Message, " - From: ", model.Email, ": ",
                        model.Name);

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential
                         ("surojermuk@gmail.com", "mypassword");
                    smtp.Port = 587;

                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                return RedirectToAction("Index");
        }

    }
}
