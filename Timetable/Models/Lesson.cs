using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class Lesson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string place { get; set; }
        public string type { get; set; }
        public string teacher { get; set; }
        public string starts { get; set; }
        public string ends { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}
