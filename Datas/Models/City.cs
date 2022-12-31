using System;
using System.Collections.Generic;
using System.Text;

namespace Datas.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime LastUpdatedOn { get; set; }

         public int LastUpdatedBy { get; set; }

    }
}
