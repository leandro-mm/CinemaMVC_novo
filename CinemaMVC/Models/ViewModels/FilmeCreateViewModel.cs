using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaMVC.Models
{
    public class FilmeCreateViewModel
    {
        [Required]
        [Remote("CheckNomeFilme", "Filmes", ErrorMessage = "Filme já cadastrado com esse nome.")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Duração")]
        public TimeSpan? Duracao { get; set; }

        [Required]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase uploadFile { get; set; }
         
    }
}