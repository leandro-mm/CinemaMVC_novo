using CinemaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaMVC.Helpers
{
    public static class TipoAnimacaoHelper
    {
        /// <summary>
        /// Recupera sessões de uma sala
        /// </summary>
        /// <param name="TipoAnimacaoID"></param>       
        /// <returns>lista contendo sessões vinculadas a sala</returns>
        public static List<string> GetSalasByTipoAnimacaoID(int? TipoAnimacaoID)
        {
            List<string> result = new List<string>();

            try
            {
                if (TipoAnimacaoID.HasValue)
                {
                    using (CinemaEntities db = new CinemaEntities())
                    {
                        var salauAudioAnim = db.SalaAudioAnimacao
                             .Where(s => s.TipoAnimacaoID == TipoAnimacaoID.Value);

                        if (salauAudioAnim != null && salauAudioAnim.Any())
                        {
                            foreach (var item in salauAudioAnim)
                            {
                                result.Add(item.Sala.Nome);

                            }
                        }

                    }
                }
                
            }
            catch{}

            return result;
        }//end method
        
    }//end namespace
}//end class