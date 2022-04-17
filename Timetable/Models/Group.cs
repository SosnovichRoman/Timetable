using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Day> Day { get; set; }

    }
}
