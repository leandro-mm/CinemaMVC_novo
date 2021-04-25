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
   
    public class TiposAudioController : Controller
    {
        //private CinemaEntities db = new CinemaEntities();
        private Database1Entities db = new Database1Entities();
        // GET: TiposAudio
        public ActionResult Index()
        {
            return View(db.TipoAudio.ToList());
        }

        // GET: TiposAudio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAudio tipoAudio = db.TipoAudio.Find(id);
            if (tipoAudio == null)
            {
                return HttpNotFound();
            }
            return View(tipoAudio);
        }

        // GET: TiposAudio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposAudio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoAudioID,Descricao")] TipoAudio tipoAudio)
        {
            if (ModelState.IsValid)
            {
                db.TipoAudio.Add(tipoAudio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoAudio);
        }

        // GET: TiposAudio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAudio tipoAudio = db.TipoAudio.Find(id);
            if (tipoAudio == null)
            {
                return HttpNotFound();
            }
            return View(tipoAudio);
        }

        // POST: TiposAudio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoAudioID,Descricao")] TipoAudio tipoAudio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoAudio);
        }

        // GET: TiposAudio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAudio tipoAudio = db.TipoAudio.Find(id);
            if (tipoAudio == null)
            {
                return HttpNotFound();
            }
            return View(tipoAudio);
        }

        // POST: TiposAudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoAudio tipoAudio = db.TipoAudio.Find(id);
            db.TipoAudio.Remove(tipoAudio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult CheckDescricao(string Descricao, int? TipoAudioID)
        {
            return Json(db.TipoAudio.Where(s => s.Descricao == Descricao && (TipoAudioID.HasValue ? s.TipoAudioID != TipoAudioID.Value : true)).FirstOrDefault() == null, JsonRequestBehavior.AllowGet);
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
