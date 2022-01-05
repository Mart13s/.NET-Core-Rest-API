using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class UserInternal
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime dateCreated { get; set; }
        public string passwordHash { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
