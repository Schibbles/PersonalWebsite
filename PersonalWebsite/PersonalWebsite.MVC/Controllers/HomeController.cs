using PersonalWebsite.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace PersonalWebsite.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult resume()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string returnMessage = $"You have received an email from {cvm.Name}.<br />" +
                $"Subject: {cvm.Subject}.<br />" +
                $"Message: {cvm.Message}<br />" +
                $"Please respond to {cvm.Email}.";


            MailMessage mm = new MailMessage(
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailTo"].ToString(),
                cvm.Subject,
                returnMessage
                );

            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());

            try
            {
                client.Send(mm);
            }
            catch (Exception)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be processed at this time. Please try again later.";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);

        }

        [HttpGet]
        public ActionResult portfolio()
        {
            return View();
        }

        [HttpGet]
        public ActionResult links()
        {
            return View();
        }
    }
}