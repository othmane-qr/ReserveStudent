using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveStudent.Models
{
    public class ReservationType
    {
        [Key]
        public int Id { get; set; }
        public string NameType { get; set; }
        public int AccessNumber { get; set; }
    }
}
