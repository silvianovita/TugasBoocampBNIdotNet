using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [ForeignKey("Education")]
        public int Education_Id { get; set; }


        public virtual Education Education { get; set; }
        public virtual Account Account { get; set; }
    }
}
