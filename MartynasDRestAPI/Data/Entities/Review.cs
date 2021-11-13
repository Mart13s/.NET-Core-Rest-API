using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class Review
    {
        public int id { get; set; }
        public int rating { get; set; }
        public string reviewName { get; set; }
        public string reviewText { get; set; }
        public StoreItem item { get; set; }

    }
}
