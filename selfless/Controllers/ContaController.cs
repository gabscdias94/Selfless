using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using selfless.Web.Models;
using System.Net.Mail;

namespace selfless.Web.Controllers
{
    public class ContaController : Controller
    {

        public ActionResult Index()
        {
            using (var contexto = new Selfless_db())
            {
                return View(contexto.Usuarios.ToList());
            }
        }
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult EsqueceuSenha()
        {
            return View();
        }

        public void VerificarEmail(string email, string ativacaoCode, string emailFor = "Verificar Conta")
        {
            var verificarUrl = "/Conta/"+emailFor +"/" + ativacaoCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verificarUrl);

            var fromEmail = new MailAddress("selflessAdm@gmail.com", "Selfless portal de doações");
            var toEmail = new MailAddress(email);
            string subject = "";
            string body = "";
            var fromEmailPassword = "********";
            if (emailFor == "Verificar Conta")
            {
                subject = "Sua conta foi criada com sucesso!";
                body = "<br/><br/> Conta criada com sucesso, clique no link para confirmação" + "<br/><br/> <a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetSenha")
            {
                subject = "Alterar Senha";
                body = "Oi, <br/>bt/> Clique" + "<br/><br/><a href=" + link + ">Reset Senha Link</a>";
            }
           
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)

            };
        }

        public ActionResult EsqueceuSenha(string email)
        {
            string mensagem = "";
            bool status = false;
            using (var contexto = new Selfless_db())
            {
                var conta = contexto.Usuarios.Where(a => a.email == email).FirstOrDefault();
                if (conta != null)
                {
                    string resetCode = Guid.NewGuid().ToString();

                }
                else
                {
                    mensagem = "Conta não encontrada";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            try
            {
                using (var contexto = new Selfless_db())
                {
                    if (usuario != null)
                    {
                        var novo = new Usuario
                        {
                            nome = usuario.nome,
                            senha = usuario.senha,
                            email = usuario.email,
                            cnpj = usuario.cnpj

                        };

                        contexto.Usuarios.Add(usuario);
                        contexto.SaveChanges();
                        return Json(usuario, JsonRequestBehavior.AllowGet);

                    }

                    else
                    {
                        throw new ApplicationException("Erro ao registrar usuário!");
                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public JsonResult Login(Usuario usuario)
        {
            using (var contexto = new Selfless_db())
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Perfil", "Conta");
                var usr = contexto.Usuarios.Single(a => a.email == usuario.email && a.senha == usuario.senha);
                if (usr != null)
                {
                    Session["id"] = usr.id.ToString();
                    Session["email"] = usr.email.ToString();
                    Session["nome"] = usr.nome.ToString();
                    return Json(new { Url = redirectUrl });
                }

                else
                {
                    ModelState.AddModelError("", "E-mail ou Password está errado.");
                }
            }
            return Json(usuario, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Perfil()
        {
            if (Session["id"] != null)
            {
                return View("Perfil");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

    }
}
