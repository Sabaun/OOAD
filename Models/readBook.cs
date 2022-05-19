using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class readBook
    {
        public int BookID { get; set; }

 
        public string bookName { get; set; }

    
        public string writerName { get; set; }
        public string bookCatagory { get; set; }

     
        public string publishDate { get; set; }


        public string bookPath { get; set; }

        public string imagePath { get; set; }
    }
}