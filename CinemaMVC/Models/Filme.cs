//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Filme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Filme()
        {
            this.Sessao = new HashSet<Sessao>();
        }
    
        public int FilmeID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public System.TimeSpan Duracao { get; set; }
        public Nullable<int> ImagemID { get; set; }
    
        public virtual Imagem Imagem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sessao> Sessao { get; set; }
    }
}
