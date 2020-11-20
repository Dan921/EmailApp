using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApp.Models
{
    public class Letter
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
    }
}
