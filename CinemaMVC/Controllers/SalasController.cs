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
    public class SalasController : Controller
    {
        private CinemaEntities db = new CinemaEntities();

        // GET: Salas
        public ActionResult Index()
        {
            return View(db.Sala.ToList());
        }

        // GET: Salas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

              Sala sala = db.Sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // GET: Salas/Create
        public ActionResult Create()
        {
           
            SalaCreateViewModel viewModel = new SalaCreateViewModel
            {
                TipoAnimacaoID3selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao"),
                TipoAudioID3selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao"),
                TipoAnimacaoID2selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao"),
                TipoAudioID2selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao"),
                TipoAnimacaoID1selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao"),
                TipoAudioID1selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao")
            };
            return View(viewModel);
        }

        // POST: Salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,QtdAssentos,TipoAudioID1,TipoAudioID1selectList,TipoAnimacaoID1,TipoAnimacaoID1selectList,TipoAudioID2,TipoAudioID2selectList,TipoAnimacaoID2,TipoAnimacaoID2selectList,TipoAudioID3,TipoAudioID3selectList,TipoAnimacaoID3,TipoAnimacaoID3selectList")] SalaCreateViewModel ViewModel)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    Sala sala = new Sala
                    {
                        Nome = ViewModel.Nome,
                        QtdAssentos = ViewModel.QtdAssentos
                    };

                    //--Armazena as opções Tipo Animacao e Tipo Audio obrigatórias
                    SalaAudioAnimacao salaAudioAnimacao1 = new SalaAudioAnimacao
                    {
                        Sala = sala,
                        TipoAnimacaoID = ViewModel.TipoAnimacaoID1,
                        TipoAudioID = ViewModel.TipoAudioID1
                    };

                    db.Sala.Add(sala);
                    db.SalaAudioAnimacao.Add(salaAudioAnimacao1);

                    //--Vincula à sala as 2 opções Tipo Animacao e Tipo Audio opcionais
                    if (ViewModel.TipoAnimacaoID2.HasValue && ViewModel.TipoAnimacaoID2.Value != 0 &&
                        ViewModel.TipoAudioID2.HasValue && ViewModel.TipoAudioID2.Value != 0)
                    {
                        if (ViewModel.TipoAnimacaoID1 != ViewModel.TipoAnimacaoID2.Value ||
                            ViewModel.TipoAudioID1 != ViewModel.TipoAudioID2.Value)
                        {
                            SalaAudioAnimacao salaAudioAnimacao2 = new SalaAudioAnimacao
                            {
                                Sala = sala,
                                TipoAnimacaoID = ViewModel.TipoAnimacaoID2.Value,
                                TipoAudioID = ViewModel.TipoAudioID2.Value
                            };

                            db.SalaAudioAnimacao.Add(salaAudioAnimacao2);
                        }

                        if (ViewModel.TipoAnimacaoID3.HasValue && ViewModel.TipoAnimacaoID3.Value != 0 &&
                            ViewModel.TipoAudioID3.HasValue && ViewModel.TipoAudioID3.Value != 0)
                        {
                            if ((ViewModel.TipoAnimacaoID3.Value != ViewModel.TipoAnimacaoID2.Value &&
                                ViewModel.TipoAnimacaoID3.Value != ViewModel.TipoAnimacaoID1) ||
                                (ViewModel.TipoAudioID3.Value != ViewModel.TipoAudioID2.Value &&
                                ViewModel.TipoAudioID3.Value != ViewModel.TipoAudioID1))
                            {
                                SalaAudioAnimacao salaAudioAnimacao3 = new SalaAudioAnimacao
                                {
                                    Sala = sala,
                                    TipoAnimacaoID = ViewModel.TipoAnimacaoID3.Value,
                                    TipoAudioID = ViewModel.TipoAudioID3.Value
                                };

                                db.SalaAudioAnimacao.Add(salaAudioAnimacao3);
                            }

                        }
                    }

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

            //--Configura os valores anteriormente selecionados na tela
            ViewModel.TipoAnimacaoID3selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAudioID3selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAnimacaoID2selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAudioID2selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAnimacaoID1selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAudioID1selectList.First(x => x.Value == "selectedValue").Selected = true;

            return View(ViewModel);
        }

        // GET: Salas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            Sala sala = db.Sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }

            SalaEditViewModel ViewModel = new SalaEditViewModel
            {
                SalaID = sala.SalaID,
                Nome = sala.Nome,
                QtdAssentos = sala.QtdAssentos
            };

            //--configura DropDownListFor para cada opção de Tipo de Áudio e Tipo de Animação encontrado na tabela SalaAudioAnimacao
            var salaAudioAnimacaoDb = sala.SalaAudioAnimacao.Where(s => s.SalaID == sala.SalaID).ToList();
            if (salaAudioAnimacaoDb != null && salaAudioAnimacaoDb.Any())
            {
                for (int i = 0; i < salaAudioAnimacaoDb.Count(); i++)
                {
                    var TipoAnimacaoID = salaAudioAnimacaoDb[i].TipoAnimacaoID;
                    var TipoAudioID = salaAudioAnimacaoDb[i].TipoAudioID;
                    var SalaAudioAnimacaoID = salaAudioAnimacaoDb[i].SalaAudioAnimacaoID;
                    switch (i)
                    {
                        case 0:
                            ViewModel.SalaAudioAnimacaoID1 = SalaAudioAnimacaoID;
                            ViewModel.TipoAnimacaoID1 = TipoAnimacaoID;
                            ViewModel.TipoAnimacaoID1selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao", TipoAnimacaoID);
                            ViewModel.TipoAudioID1 = TipoAudioID;
                            ViewModel.TipoAudioID1selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao", TipoAudioID);
                            break;
                        case 1:
                            ViewModel.SalaAudioAnimacaoID2 = SalaAudioAnimacaoID;
                            ViewModel.TipoAnimacaoID2 = TipoAnimacaoID;
                            ViewModel.TipoAnimacaoID2selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao", TipoAnimacaoID);
                            ViewModel.TipoAudioID2 = TipoAudioID;
                            ViewModel.TipoAudioID2selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao", TipoAudioID);
                            break;
                        case 2:
                            ViewModel.SalaAudioAnimacaoID3 = SalaAudioAnimacaoID;
                            ViewModel.TipoAnimacaoID3 = TipoAnimacaoID;
                            ViewModel.TipoAnimacaoID3selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao", TipoAnimacaoID);
                            ViewModel.TipoAudioID3 = TipoAudioID;
                            ViewModel.TipoAudioID3selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao", TipoAudioID);
                            break;
                        default:
                            break;
                    }
                }
            }

            //--Verifica se a sala foi criada somente com 1 tipo de áudio e 1 tipo de animação 
            if (!ViewModel.TipoAnimacaoID2.HasValue)
            {
                ViewModel.TipoAnimacaoID2selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao");
                ViewModel.TipoAudioID2selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao");
            }

            if (!ViewModel.TipoAnimacaoID3.HasValue)
            {
                ViewModel.TipoAnimacaoID3selectList = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao");
                ViewModel.TipoAudioID3selectList = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao");
            }

            return View(ViewModel);
        }


        // POST: Salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaID,Nome,QtdAssentos,SalaAudioAnimacaoID1,SalaAudioAnimacaoID2,SalaAudioAnimacaoID3,TipoAudioID1,TipoAudioID1selectList,TipoAnimacaoID1,TipoAnimacaoID1selectList,TipoAudioID2,TipoAudioID2selectList,TipoAnimacaoID2,TipoAnimacaoID2selectList,TipoAudioID3,TipoAudioID3selectList,TipoAnimacaoID3,TipoAnimacaoID3selectList")] SalaEditViewModel ViewModel)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    Sala salaDb = db.Sala.Where(s => s.SalaID == ViewModel.SalaID).FirstOrDefault();

                    if(salaDb == null)
                    {
                        ViewBag.Mensagem = "Não existe um registro na tabela Sala com esse id "+ ViewModel.SalaID;
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    salaDb.Nome = ViewModel.Nome;
                    salaDb.QtdAssentos = ViewModel.QtdAssentos;

                    //--Atualiza o relacionamento SalaAudioAnimacao
                    List<SalaAudioAnimacao> itensExcluir = new List<SalaAudioAnimacao>();
                    List<SalaAudioAnimacao> itensAdicionar = new List<SalaAudioAnimacao>();

                    salaDb.SalaAudioAnimacao
                        .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID1)
                        .FirstOrDefault()
                        .TipoAnimacaoID = ViewModel.TipoAnimacaoID1;

                    salaDb.SalaAudioAnimacao
                        .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID1)
                        .FirstOrDefault()
                        .TipoAudioID = ViewModel.TipoAudioID1;

                    if (ViewModel.TipoAnimacaoID2.HasValue && ViewModel.TipoAudioID2.HasValue)
                    {
                        if (ViewModel.SalaAudioAnimacaoID2 == 0)
                        {
                            //--insere
                            itensAdicionar.Add(
                                new SalaAudioAnimacao
                                {
                                    SalaID = salaDb.SalaID,
                                    TipoAudioID = ViewModel.TipoAudioID2.Value,
                                    TipoAnimacaoID = ViewModel.TipoAnimacaoID2.Value
                                });
                        }
                        else
                        {
                            //--atualiza
                            salaDb.SalaAudioAnimacao
                            .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID2)
                            .FirstOrDefault()
                            .TipoAnimacaoID = ViewModel.TipoAnimacaoID2.Value;

                            salaDb.SalaAudioAnimacao
                                .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID2)
                                .FirstOrDefault()
                                .TipoAudioID = ViewModel.TipoAudioID2.Value;
                        }
                    }
                    else
                    {
                        if (ViewModel.SalaAudioAnimacaoID2 > 0)
                        {
                            //--remove do banco de dados a opção
                            var deleteItem = salaDb.SalaAudioAnimacao
                                .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID2)
                                .FirstOrDefault();

                            if (deleteItem != null) itensExcluir.Add(deleteItem);// salaDb.SalaAudioAnimacao.Remove(deleteItem);
                        }
                    }

                    if (ViewModel.TipoAnimacaoID3.HasValue && ViewModel.TipoAudioID3.HasValue)
                    {
                        if (ViewModel.SalaAudioAnimacaoID3 == 0)
                        {
                            //--insere
                            itensAdicionar.Add(
                               new SalaAudioAnimacao
                               {
                                   SalaID = salaDb.SalaID,
                                   TipoAudioID = ViewModel.TipoAudioID3.Value,
                                   TipoAnimacaoID = ViewModel.TipoAnimacaoID3.Value
                               });
                        }
                        else
                        {
                            //--atualiza
                            salaDb.SalaAudioAnimacao
                           .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID3)
                           .FirstOrDefault()
                           .TipoAnimacaoID = ViewModel.TipoAnimacaoID3.Value;

                            salaDb.SalaAudioAnimacao
                                .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID3)
                                .FirstOrDefault()
                                .TipoAudioID = ViewModel.TipoAudioID3.Value;
                        }
                    }
                    else
                    {
                        if (ViewModel.SalaAudioAnimacaoID3 > 0)
                        {
                            //--remove do banco de dados a opção
                            var deleteItem = salaDb.SalaAudioAnimacao
                                .Where(s => s.SalaAudioAnimacaoID == ViewModel.SalaAudioAnimacaoID3)
                                .FirstOrDefault();

                            if (deleteItem != null) itensExcluir.Add(deleteItem);// salaDb.SalaAudioAnimacao.Remove(deleteItem);
                        }
                    }

                    db.Entry(salaDb).State = EntityState.Modified;
                    db.SaveChanges();

                    foreach (var item in itensExcluir)
                    {
                        db.SalaAudioAnimacao.Remove(item);
                    }

                    foreach (var item in itensAdicionar)
                    {
                        db.SalaAudioAnimacao.Add(item);
                    }

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

            //--Configura os valores SalaAudioAnimacao anteriormente selecionados na tela
            ViewModel.TipoAnimacaoID1selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAudioID1selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAnimacaoID2selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAudioID2selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAnimacaoID3selectList.First(x => x.Value == "selectedValue").Selected = true;
            ViewModel.TipoAudioID3selectList.First(x => x.Value == "selectedValue").Selected = true;

            return View(ViewModel);
        }

        // GET: Salas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            Sala sala = db.Sala.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
               
                Sala sala = db.Sala.Find(id);
                var salaAudioAnimacao = db.SalaAudioAnimacao.Where(s => s.SalaID == sala.SalaID);

                if (salaAudioAnimacao != null && salaAudioAnimacao.Any())
                {
                    foreach (var item in salaAudioAnimacao)
                    {
                        db.SalaAudioAnimacao.Remove(item);
                    }
                }

                db.Sala.Remove(sala);
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

        public JsonResult CheckNomeSala(string Nome, int? SalaID)
        {
            return Json(db.Sala.Where(s => s.Nome == Nome && (SalaID.HasValue ? s.SalaID != SalaID.Value : true)).FirstOrDefault() == null, JsonRequestBehavior.AllowGet);
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
