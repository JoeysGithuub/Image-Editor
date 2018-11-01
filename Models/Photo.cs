    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageEdit.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte ImagePath { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}