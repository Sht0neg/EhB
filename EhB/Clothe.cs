using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EhB
{
    public class Clothe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string Titel { get; set; }

        public int? Count { get; set; }

        public string? Size { get; set; }

        public DateTime DateProd { get; set; }
    }
}
