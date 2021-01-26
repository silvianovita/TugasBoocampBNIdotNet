using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }
        
        [ForeignKey("Person")]
        public String FirstName { get; set; }
        public virtual Person Person { get; set; }
        public virtual Profiling Profiling { get; set; }
    }
}
