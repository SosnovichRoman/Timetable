using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable.Models
{
    public class Day
    {
        public int id { get; set; }
        public int dayOfWeek { get; set; }
        public string GroupName { get; set; } //Удалить
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Lesson> Lesson { set; get; }
    }
}
