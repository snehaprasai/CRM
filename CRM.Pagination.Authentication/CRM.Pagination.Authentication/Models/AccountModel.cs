using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Pagination.Authentication.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Interest { get; set; }
        public int MinimumBalance { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
    public class AccountViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Interest")]
        public double Interest { get; set; }
        [Required]
        [DisplayName("Minimum Balance")]
        public int MinimumBalance { get; set; }
        public bool Status { get; set; }
    }

}