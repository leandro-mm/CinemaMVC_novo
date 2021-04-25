using CinemaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaMVC.Helpers
{
    public static class TipoAudioHelper
    {
        /// <summary>
        /// Recupera sessões de uma sala
        /// </summary>
        /// <param name="TipoAnimacaoID"></param>       
        /// <returns>lista contendo sessões vinculadas a sala</returns>
        public static List<Sala> GetSalasByTipoAudioID(int? TipoAudioID)
        {
            List<Sala> result = new List<Sala>();

            try
            {
                if (TipoAudioID.HasValue)
                {
                    using (CinemaEntities db = new CinemaEntities())
                    {
                        var salauAudioAnimList = db.SalaAudioAnimacao
                             .Where(s => s.TipoAudioID == TipoAudioID.Value);

                        if (salauAudioAnimList != null && salauAudioAnimList.Any())
                        {
                            foreach (var item in salauAudioAnimList)
                            {
                                result.Add(item.Sala);

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