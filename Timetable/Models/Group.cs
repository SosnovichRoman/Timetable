using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Timetable.Models
{
    [DataContract]
    public class Group
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }

        public ICollection<Day> Day { get; set; }

    }
}
