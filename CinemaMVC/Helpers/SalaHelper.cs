using CinemaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaMVC.Helpers
{
    public static class SalaHelper
    {
        public static List<TipoAnimacao> GetTipoAnimacaoBySalaID(int? SalaID)
        {
            List<TipoAnimacao> result = new List<TipoAnimacao>();

            if (SalaID.HasValue)
            {
                using (CinemaEntities db = new CinemaEntities())
                {
                    var salauAudioAnim = db.SalaAudioAnimacao
                        .Where(s => s.SalaID == SalaID);

                    if (salauAudioAnim != null && salauAudioAnim.Any())
                    {
                        foreach (var item in salauAudioAnim)
                        {
                            var tipoAnimacaoList = db.TipoAnimacao
                            .Where(t => t.TipoAnimacaoID == item.TipoAnimacaoID);

                            if (tipoAnimacaoList != null && tipoAnimacaoList.Any())
                            {
                                result = tipoAnimacaoList.ToList();
                            }

                        }
                    }
                }
            }

            return result;
        }//end method

        public static List<TipoAudio> GetTipoAudioBySalaID(int? SalaID)
        {
            List<TipoAudio> result = new List<TipoAudio>();

            if (SalaID.HasValue)
            {
                using (CinemaEntities db = new CinemaEntities())
                {
                    var salauAudioAnim = db.SalaAudioAnimacao
                        .Where(s => s.SalaID == SalaID);

                    if(salauAudioAnim != null && salauAudioAnim.Any())
                    {
                        foreach (var item in salauAudioAnim)
                        {
                            var tipoAudioList = db.TipoAudio
                            .Where(t => t.TipoAudioID == item.TipoAudioID);

                            if(tipoAudioList != null && tipoAudioList.Any())
                            {
                                result = tipoAudioList.ToList();
                            }

                        }
                    }
                }
            }

            return result;
        }//end method

        public static string GetTipoAudio(SalaAudioAnimacao obj)
        {
            var result = string.Empty;

            if (obj != null)
            {
                using (CinemaEntities db = new CinemaEntities())
                {
                    var salauAudioAnim = db.SalaAudioAnimacao
                        .Where(s => s.SalaID == obj.SalaID && s.TipoAnimacaoID == obj.TipoAnimacaoID && s.TipoAudioID == obj.TipoAudioID)
                        .FirstOrDefault();

                    if (salauAudioAnim != null)
                    {
                        var tipoAudio = db.TipoAudio
                            .Where(t => t.TipoAudioID == salauAudioAnim.TipoAudioID)
                            .FirstOrDefault();

                        if (tipoAudio != null)
                            result = tipoAudio.Descricao;
                    }
                }
            }

            return result;
        }//end method
        
       

    }//end class

}//end namespace