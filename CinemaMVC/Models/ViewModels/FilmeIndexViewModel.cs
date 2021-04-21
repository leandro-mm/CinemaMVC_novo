using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    public class FilmeIndexViewModel
    {
        public IEnumerable<Filme> Filmes { get; set; }

        [Display(Name = "Título")]
        public int FilmeID { get; set; }
        
        public SelectList FilmeSelectLIst { get; set; }

        [Display(Name = "Duração")]
        public TimeSpan Duracao { get; set; }
        public SelectList DuracaoSelectList { get; set; }


        public int FilmePerPage { get; set; }
        public int CurrentPage { get; set; }
        public int GetPageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Filmes.Count() / (double)FilmePerPage));
        }
        public IEnumerable<Filme> GetPaginatedFilmes()
        {
            int start = (CurrentPage - 1) * FilmePerPage;
            return Filmes.OrderBy(b => b.Titulo).Skip(start).Take(FilmePerPage);
        }
    }
}