using CinemaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaMVC.Helpers
{
    public static class SessaoHelper
    {
        /// <summary>
        /// Verifica se o registro SalaAudioAnimacao possui uma sessão vinculada
        /// </summary>
        /// <param name="SalaID"></param>
        /// <param name="TipoAudioID"></param>
        /// <param name="TipoAnimacaoID"></param>
        /// <returns>verdadeiro se o registro SalaAudioAnimacao possui uma sessão vinculada</returns>
        public static bool HasSessaoVinculada(int SalaID, int? TipoAudioID, int? TipoAnimacaoID)
        {
            var result = false;

            if(TipoAudioID.HasValue && TipoAnimacaoID.HasValue)
            {
                try
                {
                    using (CinemaEntities db = new CinemaEntities())
                    {
                        SalaAudioAnimacao salaAudioAnimacao =
                            db.SalaAudioAnimacao
                            .Where(s => s.SalaID == SalaID && s.TipoAudioID == TipoAudioID.Value && s.TipoAnimacaoID == TipoAnimacaoID.Value)
                            .FirstOrDefault();

                        if (salaAudioAnimacao != null)
                        {
                            Sessao sessao =
                                db.Sessao
                                .Where(s => s.SalaAudioAnimacaoID == salaAudioAnimacao.SalaAudioAnimacaoID)
                                .FirstOrDefault();

                            result = (sessao != null) ? true : false;

                        }
                    }
                }
                catch
                {
                    result = true;
                }
            }
           
            return result;
        }//end method

        /// <summary>
        /// Verifica se a Sala possui uma sessão vinculada
        /// </summary>
        /// <param name="SalaID"></param>       
        /// <returns>verdadeiro se a sala possui uma sessão vinculada</returns>
        public static bool HasSessaoVinculada(int SalaID)
        {
            var result = false;

            try
            {
                using (CinemaEntities db = new CinemaEntities())
                {
                    var salaAudioAnimacao =
                        db.SalaAudioAnimacao
                        .Where(s => s.SalaID == SalaID);

                    foreach (var item in salaAudioAnimacao)
                    {
                        Sessao sessao =
                           db.Sessao
                           .Where(s => s.SalaAudioAnimacaoID == item.SalaAudioAnimacaoID)
                           .FirstOrDefault();

                        if (sessao != null)
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

        /// <summary>
        /// Recupera sessões de uma sala
        /// </summary>
        /// <param name="SalaID"></param>       
        /// <returns>lista contendo sessões vinculadas a sala</returns>
        public static List<string> GetSessoesBySalaID(int? SalaID)
        {
            List<string> result = new List<string>();

            try
            {
                if (SalaID.HasValue)
                {
                    using (CinemaEntities db = new CinemaEntities())
                    {
                        var salauAudioAnim = db.SalaAudioAnimacao
                             .Where(s => s.SalaID == SalaID.Value);

                        if (salauAudioAnim != null && salauAudioAnim.Any())
                        {
                            foreach (var item in salauAudioAnim)
                            {
                                var sessaoList = db.Sessao.Where(s => s.SalaAudioAnimacaoID == item.SalaAudioAnimacaoID);

                                if (sessaoList != null && sessaoList.Any())
                                {
                                    foreach (var sessao in sessaoList.OrderBy(d => d.Data).ThenBy(h=>h.HorarioInicio))
                                    {
                                        result.Add(sessao.Data.ToShortDateString() + " das " + sessao.HorarioInicio + " às " + sessao.HorarioFim);
                                    }

                                }

                            }
                        }

                    }
                }
                
            }
            catch{}

            return result;
        }//end method

        public static List<string> GetSessoesByFilmeID(int? FilmeID)
        {
            List<string> result = new List<string>();

            try
            {
                if (FilmeID.HasValue)
                {
                    using (CinemaEntities db = new CinemaEntities())
                    {
                        var sessaoList = db.Sessao.Where(f=>f.FilmeID == FilmeID.Value);

                        if (sessaoList != null && sessaoList.Any())
                        {
                            foreach (var sessao in sessaoList.OrderBy(d => d.Data).ThenBy(h => h.HorarioInicio))
                            {
                                result.Add(sessao.Data.ToShortDateString() + " das " + sessao.HorarioInicio + " às " + sessao.HorarioFim);
                            }
                        }

                    }
                }

            }
            catch { }

            return result;
        }//end method



    }//end namespace
}//end class