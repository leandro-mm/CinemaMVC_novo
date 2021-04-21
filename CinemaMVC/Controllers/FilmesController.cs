using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaMVC.Models;

namespace CinemaMVC.Controllers
{
    [Authorize]
    public class FilmesController : Controller
    {
        private CinemaEntities db = new CinemaEntities();

        // GET: Filmes
        public ActionResult Index(int page = 1)
        {
            var ViewModel = new FilmeIndexViewModel
            {
                FilmePerPage = 5,
                Filmes = db.Filme.OrderBy(d => d.Titulo),
                CurrentPage = page
            };
            return View(ViewModel);
        }

        // GET: Filmes/Details/5
        public ActionResult Details(int? id)
        {
           
            if (id == null)
            {
                ViewBag.Mensagem = "Não existe Filme com id = " + id;
                return View("~/Views/Shared/Error.cshtml");

            }
            Filme filme = db.Filme.Find(id);
            if (filme == null)
            {
                ViewBag.Mensagem = "Não existe Filme com id = " + id;
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(filme);
        }

        // GET: Filmes/Create
        public ActionResult Create()
        {
            
            FilmeCreateViewModel ViewModel = new FilmeCreateViewModel();
            return View(ViewModel);
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmeCreateViewModel ViewModel)        
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    ImagemFilme imagemFilme = null;
                    if (ViewModel.uploadFile != null && ViewModel.uploadFile.ContentLength > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ViewModel.uploadFile.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            imagemFilme = new ImagemFilme { Imagem = array };
                        }
                    }

                    Filme filme = new Filme
                    {
                        Titulo = ViewModel.Titulo,
                        Descricao = ViewModel.Descricao,
                        Duracao = ViewModel.Duracao,
                        ImagemFilme = imagemFilme
                    };

                    db.Filme.Add(filme);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var erro = ex.Message;
                    var msg1 = ex.InnerException != null ? ex.InnerException.Message : "";
                    var msg2 = ex.InnerException != null ? (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "") : "";
                    var finalMessage = erro + " " + msg1 + " " + msg2;
                    ViewBag.Mensagem = finalMessage;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            //return View(ViewModel);
            return View();
        }//end method

        // GET: Filmes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            Filme filme = db.Filme.Find(id);
            if (filme == null)
            {
                return HttpNotFound();
            }

            FilmeEditViewModel ViewModel = new FilmeEditViewModel
            {
                FilmeID = filme.FilmeID,
                Titulo = filme.Titulo,
                Descricao = filme.Descricao,
                Duracao = filme.Duracao,
                ImagemID = filme.ImagemFilme == null ? 0 : filme.ImagemFilme.ImagemID,
                Imagem = filme.ImagemFilme == null ? null : filme.ImagemFilme.Imagem
            };
            return View(ViewModel);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FilmeID,Titulo,Descricao,Duracao,UploadedFile,ImagemID,Imagem")] FilmeEditViewModel ViewModel)
        {
            try
            {
                 if (ModelState.IsValid)
                {
                    byte[] array = null;
                    if (ViewModel.UploadedFile != null && ViewModel.UploadedFile.ContentLength > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ViewModel.UploadedFile.InputStream.CopyTo(ms);
                             array = ms.GetBuffer();
                        }
                    }
                    else
                    {
                        array = ViewModel.Imagem;
                    }

                    Filme filme = db.Filme.Where(f => f.FilmeID == ViewModel.FilmeID).FirstOrDefault();

                    if(filme != null)
                    {
                        filme.Titulo = ViewModel.Titulo;
                        filme.Descricao = ViewModel.Descricao;
                        filme.Duracao = ViewModel.Duracao;
                        filme.ImagemFilme.Imagem = array;
                        db.Entry(filme).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                   
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                var msg1 = ex.InnerException != null ? ex.InnerException.Message : "";
                var msg2 = ex.InnerException != null ? (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "") : "";
                var finalMessage = erro + " " + msg1 + " " + msg2;
                ViewBag.Mensagem = finalMessage;
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(ViewModel);
        }

        // GET: Filmes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Delete", "Filmes", new { id }) });

            Filme filme = db.Filme.Find(id);
            if (filme == null)
            {
                return HttpNotFound();
            }
            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            Filme filme = db.Filme.Find(id);
            db.Filme.Remove(filme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult CheckNomeFilme(string Titulo, int? FilmeID)
        {
            return Json(db.Filme.Where(f=>f.Titulo == Titulo && (FilmeID.HasValue ? f.FilmeID != FilmeID.Value: true)).FirstOrDefault() == null, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
