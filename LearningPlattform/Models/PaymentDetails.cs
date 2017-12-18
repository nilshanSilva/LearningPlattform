using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningPlattform.Models
{
    public class PaymentDetails
    {
        [Display(Name = "Country Code"), Required]
        public string CountryCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Display(Name ="Postal Code"), Required]
        public string PostalCode { get; set; }

        [Required]
        public string State { get; set; }

        //Credit Card

        [Required]
        public string CVV { get; set; }

        [Required, Display(Name ="Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiry Date"), DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        [ScaffoldColumn(false), Required]
        public int CourseId { get; set; }

        [ScaffoldColumn(false)]
        public bool IsInvalidData { get; set; }
    }
}