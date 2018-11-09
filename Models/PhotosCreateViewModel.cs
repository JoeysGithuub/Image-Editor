using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageEdit.Models
{
    public class PhotosCreateViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Photo Photo { get; set; }

    }
}