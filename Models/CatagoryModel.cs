using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class CatagoryModel
    {
        public string catagoryName { get; set; }

        public int ID { get; set; }

        public int isActive { get; set; }

        public List<CatagoryModel> listOfAvailableCatagories { get; set; }
    }
}