using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPIAssignment.Models
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        [Index(IsUnique = true)]
        public string OrderNbr { get; set; }
        [Required]
        public DateTime DateReceived { get; set;}
        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public double Total { get; set; }
    }
}