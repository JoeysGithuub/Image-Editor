    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageEdit.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
       [Required]
        public string ImagePath { get; set; }
  
        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}