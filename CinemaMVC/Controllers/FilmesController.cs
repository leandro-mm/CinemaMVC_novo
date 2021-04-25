﻿using System;
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
    
    public class FilmesController : Controller
    {
        //private CinemaEntities db = new CinemaEntities();
        private Database1Entities db = new Database1Entities();
        
        private readonly int FilmePerPageSetting = 3;

        // GET: Filmes
        public ActionResult Index(int page = 1)
        {
            var ViewModel = new FilmeIndexViewModel
            {
                FilmePerPage = FilmePerPageSetting,
                Filmes = db.Filme.OrderBy(d => d.Titulo),
                FilmeSelectLIst = new SelectList(db.Filme, "FilmeID", "Titulo"),               
                CurrentPage = page
            };
            return View(ViewModel);
        }

        // POst: Filmes
        [HttpPost]
        public ActionResult Index(FilmeIndexViewModel ViewModel)
        {
            ViewModel.Filmes = db.Filme.Where(f=> f.FilmeID == (ViewModel.FilmeID ?? f.FilmeID));            
            ViewModel.FilmeSelectLIst = new SelectList(db.Filme, "FilmeID", "Titulo", ViewModel.FilmeID ?? ViewModel.FilmeID);
            ViewModel.FilmePerPage = FilmePerPageSetting;
            return PartialView("_Index", ViewModel);
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
                    Imagem imagemFilme = null;
                    if (ViewModel.uploadFile != null && ViewModel.uploadFile.ContentLength > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ViewModel.uploadFile.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                            imagemFilme = new Imagem { Imagem1 = array };
                        }
                    }

                    Filme filme = new Filme
                    {
                        Titulo = ViewModel.Titulo,
                        Descricao = ViewModel.Descricao,
                        Duracao = ViewModel.Duracao.Value,
                        Imagem = imagemFilme
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
                ImagemID = filme.Imagem == null ? 0 : filme.Imagem.ImagemID,
                Imagem = filme.Imagem == null ? null : filme.Imagem.Imagem1
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
                        filme.Duracao = ViewModel.Duracao.Value;
                        filme.Imagem.Imagem1 = array;
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
