using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaMVC.Models;

namespace CinemaMVC.Controllers
{
    [Authorize]
    public class TiposAnimacaoController : Controller
    {
        //private CinemaEntities db = new CinemaEntities();
        private Database1Entities db = new Database1Entities();
        // GET: TiposAnimacao
        public ActionResult Index()
        {
           
            return View(db.TipoAnimacao.ToList());
        }

        // GET: TiposAnimacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

          
            TipoAnimacao tipoAnimacao = db.TipoAnimacao.Find(id);
            if (tipoAnimacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnimacao);
        }

        // GET: TiposAnimacao/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: TiposAnimacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoAnimacaoID,Descricao")] TipoAnimacao tipoAnimacao)
        {
               if (ModelState.IsValid)
            {
                db.TipoAnimacao.Add(tipoAnimacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoAnimacao);
        }

        // GET: TiposAnimacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TipoAnimacao tipoAnimacao = db.TipoAnimacao.Find(id);
            if (tipoAnimacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnimacao);
        }

        // POST: TiposAnimacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoAnimacaoID,Descricao")] TipoAnimacao tipoAnimacao)
        {
                 if (ModelState.IsValid)
            {
                db.Entry(tipoAnimacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoAnimacao);
        }

        // GET: TiposAnimacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

                   TipoAnimacao tipoAnimacao = db.TipoAnimacao.Find(id);
            if (tipoAnimacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnimacao);
        }

        // POST: TiposAnimacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAnimacao tipoAnimacao = db.TipoAnimacao.Find(id);
            db.TipoAnimacao.Remove(tipoAnimacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult CheckDescricao(string Descricao, int? TipoAnimacaoID)
        {
            return Json(db.TipoAnimacao.Where(s => s.Descricao == Descricao && (TipoAnimacaoID.HasValue ? s.TipoAnimacaoID != TipoAnimacaoID.Value : true)).FirstOrDefault() == null, JsonRequestBehavior.AllowGet);
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
