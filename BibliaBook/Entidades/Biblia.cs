using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliaBook.Entidades;

namespace BibliaBook.Entidades
{
    public class Biblia
    {
        [Key]
        public int BibliaId { get; set; }
        public string Descripcion { get; set; }
        public string Siglas { get; set; }
        public int TipoId { get; set; }
    }
}
