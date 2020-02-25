﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    
    public class CreateProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ange Produkt Namn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ange Produkt Pris")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Ange Antal")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Ange Kategori Id")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Ange Brand Id")]
        public int BrandId { get; set; }

        public string Description { get; set; }
        public string Photo { get; set; }
      
    }
}