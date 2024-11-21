using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SCM1.Models;

namespace SCM1.Models
{
    [Table("Store", Schema = "Sales")]
    public class Store
    {
        [Key]
        public int BusinessEntityID { get; set; }

        public string Name { get; set; } = string.Empty; // Default to an empty string

        public int? SalesPersonID { get; set; } // Nullable, so no need to initialize

        public string? Demographics { get; set; } // Nullable, so no need to initialize

        public DateTime ModifiedDate { get; set; } = DateTime.Now; // Default to current date/time
    }
}
