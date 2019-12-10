using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassExample1.Models
{
    public class ProductModel
    {
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        public string Image { get; set; }
        public int Id { get; set; }

    }
}