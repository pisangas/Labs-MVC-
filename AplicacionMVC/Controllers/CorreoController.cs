using AplicacionMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AplicacionMVC.Controllers
{
    public class CorreoController : Controller
    {
        // GET: Correo
        public ActionResult EnviarCorreo()
        {
            return View();
        }

        // POST: Correo
        [HttpPost]
        public ActionResult EnviarCorreo(CorreoViewModel correoViewModel)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("pisangas@gmail.com", "54021038&Eduardo");
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage()
                {
                    Subject = correoViewModel.Asunto,
                    From = new MailAddress("pisangas@gmail.com", "54021038&Eduardo"),
                    Body = correoViewModel.Cuerpo
                };

                mailMessage.To.Add(new MailAddress(correoViewModel.Destinatario));
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.StackTrace);
            }
            return View(correoViewModel);
        }
    }
}