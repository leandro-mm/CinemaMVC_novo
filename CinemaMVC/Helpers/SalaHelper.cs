using CinemaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaMVC.Helpers
{
    public static class SalaHelper
    {
        public static List<string> GetTipoAudioAnimacao(Sala obj)
        {
            List<string> result = new List<string>();

            if (obj != null)
            {
                using (CinemaEntities db = new CinemaEntities())
                {
                    var salauAudioAnim = db.SalaAudioAnimacao
                        .Where(s => s.SalaID == obj.SalaID);

                    if(salauAudioAnim != null && salauAudioAnim.Any())
                    {
                        foreach (var item in salauAudioAnim)
                        {
                            var tipoAnimacao = db.TipoAnimacao
                            .Where(t => t.TipoAnimacaoID == item.TipoAnimacaoID)
                            .FirstOrDefault();

                            var tipoAudio = db.TipoAudio 
                            .Where(t => t.TipoAudioID == item.TipoAudioID)
                            .FirstOrDefault();

                            var DescricaoAnimacao = tipoAnimacao != null ? tipoAnimacao.Descricao : string.Empty;
                            var DescricaoAudio = tipoAudio != null ? tipoAudio.Descricao : string.Empty;
                            result.Add(DescricaoAudio + " | " + DescricaoAnimacao);                           
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