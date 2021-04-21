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
    public class SessoesController : Controller
    {
        private CinemaEntities db = new CinemaEntities();

        // GET: Sessoes
        public ActionResult Index()
        {
                var ViewModel = db.vwSessao.ToList();
            return View(ViewModel);
        }

        // GET: Sessoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

               Sessao sessao = db.Sessao.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            return View(sessao);
        }

        // GET: Sessoes/Create
        public ActionResult Create()
        {
          
            SessaoCreateViewModel viewModel = new SessaoCreateViewModel
            {
                SalaAudioAnimacaoSelectList = new SelectList(db.vwSala, "SalaAudioAnimacaoID", "SalaNome"),
                FilmeSelectList = new SelectList(db.Filme, "FilmeID", "Titulo")
            };            
            return View(viewModel);
        }

        // POST: Sessoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Data,HorarioInicio,HorarioFim,ValorIngresso,SalaAudioAnimacaoID,FilmeID")] SessaoCreateViewModel ViewModel)
        {
               if (ModelState.IsValid)
            {
                try
                {
                    Sessao sessao = new Sessao
                    {
                        Data = ViewModel.Data.Value,
                        HorarioInicio = ViewModel.HorarioInicio.Value,
                        HorarioFim = GetHoraFimSessao(ViewModel.HorarioInicio, ViewModel.FilmeID),
                        ValorIngresso = ViewModel.ValorIngresso.Value,
                        SalaAudioAnimacaoID = ViewModel.SalaAudioAnimacaoID.Value,
                        FilmeID = ViewModel.FilmeID.Value
                    };

                    db.Sessao.Add(sessao);
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

            ViewModel.HorarioFim = (ViewModel.HorarioInicio.HasValue && ViewModel.FilmeID.HasValue) ? GetHoraFimSessao(ViewModel.HorarioInicio.Value, ViewModel.FilmeID.Value) : new TimeSpan(0, 0, 0);
            ViewModel.FilmeSelectList = new SelectList(db.Filme, "FilmeID", "Titulo", ViewModel.FilmeID);
            ViewModel.SalaAudioAnimacaoSelectList= new SelectList(db.vwSala, "SalaAudioAnimacaoID", "SalaNome", ViewModel.SalaAudioAnimacaoID);

            return View(ViewModel);
        }
               
        // GET: Sessoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Mensagem = "Não existe Sessão com esse id= " + id;
                return View("~/Views/Shared/Error.cshtml");

            }

          
            Sessao sessao = db.Sessao.Find(id);
            if (sessao == null)
            {
                ViewBag.Mensagem = "Não existe Sessão com esse id= " + id;
                return View("~/Views/Shared/Error.cshtml");
            }

            SessaoEditViewModel ViewModel = new SessaoEditViewModel
            {
                SessaoID = sessao.SessaoID,
                Data = sessao.Data,
                HorarioInicio = sessao.HorarioInicio,
                HorarioFim = sessao.HorarioFim,
                ValorIngresso = sessao.ValorIngresso,
                SalaAudioAnimacaoID = sessao.SalaAudioAnimacaoID,
                SalaAudioAnimacaoSelectList = new SelectList(db.vwSala, "SalaAudioAnimacaoID", "SalaNome", sessao.SalaAudioAnimacaoID),
                FilmeID = sessao.FilmeID,
                FilmeSelectList = new SelectList(db.Filme, "FilmeID", "Titulo", sessao.FilmeID)
            };

           
            return View(ViewModel);
        }

        // POST: Sessoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessaoID,Data,HorarioInicio,HorarioFim,ValorIngresso,FilmeID,SalaID,TipoAudioID,TipoAnimacaoID")] Sessao sessao)
        {
         
            if (ModelState.IsValid)
            {
                db.Entry(sessao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.FilmeID = new SelectList(db.Filme, "FilmeID", "Titulo", sessao.FilmeID);
            //ViewBag.SalaID = new SelectList(db.Sala, "SalaID", "Nome", sessao.SalaID);
            //ViewBag.TipoAnimacaoID = new SelectList(db.TipoAnimacao, "TipoAnimacaoID", "Descricao", sessao.TipoAnimacaoID);
            //ViewBag.TipoAudioID = new SelectList(db.TipoAudio, "TipoAudioID", "Descricao", sessao.TipoAudioID);
            return View(sessao);
        }

        // GET: Sessoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

         
            Sessao sessao = db.Sessao.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            return View(sessao);
        }

        // POST: Sessoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

          
            Sessao sessao = db.Sessao.Find(id);            
            db.Sessao.Remove(sessao);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        private TimeSpan GetHoraFimSessao(TimeSpan? horarioInicio, int? filmeID)
        {
            try
            {
                if (!filmeID.HasValue || !horarioInicio.HasValue)
                    throw new Exception("Não foi possível calcular a hora final da sessão");
                var filme = db.Filme.Where(f => f.FilmeID == filmeID.Value).FirstOrDefault();
                if (filme == null)
                    throw new Exception("Não foi possível calcular a hora final da sessão");
                return horarioInicio.Value.Add(filme.Duracao);
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                var msg1 = ex.InnerException != null ? ex.InnerException.Message : "";
                var msg2 = ex.InnerException != null ? (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "") : "";
                var finalMessage = erro + " " + msg1 + " " + msg2;           
                throw new Exception(finalMessage);
            }
            
        }

        public List<string> GethorariosDisponiveis(int SalaID, int FilmeID, TimeSpan timeSpanHoraInicio, TimeSpan timeSpanHoraFinal, DateTime dataSessao)
        {
            List<string> result = null;

            //-- horario total do filme
            var filme = db.Filme.Where(f => f.FilmeID == FilmeID).FirstOrDefault();

            if (filme != null)
            {
                var sala = db.Sala
                            .Where(s => s.SalaID == SalaID)
                            .FirstOrDefault();

                if(sala != null)
                {
                    var tempoTotalFilme = filme.Duracao;

                    //--verifica as sessão existente para a data e sala
                    foreach (var sessao in db.Sessao
                        .Where(sessao => sessao.Data == dataSessao && 
                                sala.SalaAudioAnimacao.Select(s=>s.SalaAudioAnimacaoID).ToList().Contains(sessao.SalaAudioAnimacaoID))
                    ){
                       //var horarioInicio = 

                    }
                }
            }
            return result;
        }

        public JsonResult GetDadosHorarioSessao(int? FilmeID, string HoraInicio, int? SalaAudioAnimacaoID, string DataSessao)
        {
            List<string> horariosDisponiveis = null;
            var errorMessage = string.Empty;
            string horaFinalValue = string.Empty;

            try
            {
                TimeSpan timeSpanHoraFinal, timeSpanHoraInicio;
                
                //--calcular horário final da sessão
                if (FilmeID.HasValue && !string.IsNullOrWhiteSpace(HoraInicio))
                {
                    if (TimeSpan.TryParse(HoraInicio, out timeSpanHoraInicio))
                    {
                        timeSpanHoraFinal = GetHoraFimSessao(timeSpanHoraInicio, FilmeID.Value);
                        horaFinalValue = timeSpanHoraFinal.ToString();

                        //--verificar se há conflito nos horários já existentes para a sala selecionada
                        if (SalaAudioAnimacaoID.HasValue && !string.IsNullOrWhiteSpace(DataSessao))
                        {
                            DateTime dateValue;
                            if (DateTime.TryParse(DataSessao, out dateValue))
                            {
                                errorMessage = VerificarConflitoHorario(SalaAudioAnimacaoID.Value, dateValue, timeSpanHoraInicio, timeSpanHoraFinal) ?
                                "O horário conflitou com sessão já existente" : string.Empty;
                            }
                        }

                    }
                }

                //--apresentar sugestão de datas
                if (SalaAudioAnimacaoID.HasValue && !string.IsNullOrWhiteSpace(DataSessao) && FilmeID.HasValue)
                {
                    //-- horario total do filme
                    //--pega sala
                    //--pega sessoes da sala para a data selecionada
                    //--horarios preenchidos para a sessão da sala
                    //--calcula possibilidades em cima dos horários preenchidos e tempo total do filme
                    horariosDisponiveis = null;
                }
            }
            catch (Exception ex)
            {
                errorMessage = Helpers.ErrorHelper.GetErrorMessage(ex);
            }
            

            return Json(new { horaFinalValue, horariosDisponiveis, errorMessage }, JsonRequestBehavior.AllowGet);
        }        

        private bool VerificarConflitoHorario(int SalaAudioAnimacaoID, DateTime dataSessao, TimeSpan timeSpanHoraInicio, TimeSpan timeSpanHoraFinal)
        {
            var result = false;

            try
            {
                var sala = db.SalaAudioAnimacao
                            .Where(s => s.SalaAudioAnimacaoID == SalaAudioAnimacaoID)
                            .FirstOrDefault();

                if (sala != null)
                {
                    //--verifica as sessões cadastradas para a sala
                    foreach (var sessao in sala.Sessao.Where(sessao => sessao.Data == dataSessao))
                    {
                        if ((timeSpanHoraInicio >= sessao.HorarioInicio && timeSpanHoraInicio <= sessao.HorarioFim) ||
                                (timeSpanHoraFinal >= sessao.HorarioInicio && timeSpanHoraFinal <= sessao.HorarioFim) ||
                                (timeSpanHoraInicio < sessao.HorarioInicio && timeSpanHoraFinal > sessao.HorarioFim))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                result = true;
            }

            return result;
            
        }//end method

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
