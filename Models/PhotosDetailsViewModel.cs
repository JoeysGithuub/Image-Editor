using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageEdit.Models
{
    public class PhotosDetailsViewModel
    {
        public Category Category { get; set; }
        public Photo Photo { get; set;  }
    }
}