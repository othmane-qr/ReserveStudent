using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveStudent.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RequestingStudentId")]
        public Student RequestingStudent { get; set; }
        public string RequestingStudentId { get; set; }
        [ForeignKey("ReservationTypeId")]
        public ReservationType ReservationType { get; set; }
        public int ReservationTypeId { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateReservationViewModel
    {
        public List<SelectListItem> ReservationTypes { get; set; }
        [Display(Name = "Reservation Type")]
        public int ReservationTypeId { get; set; }
        public DateTime Date { get; set; }
    }
}
