using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveStudent.Models
{
    public class EditeRoles
    {
        public string Id { get; set; }

        [Required]
        [Display( Name="Enter The New : ")]
        public string RoleName { get; set; }

        public List<string> Users{ get; set; }
    }
}
