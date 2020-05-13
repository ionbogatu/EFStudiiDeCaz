﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFStudiiDeCaz.Model
{
    class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int SKU { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }
    }
}
