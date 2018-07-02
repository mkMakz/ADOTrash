﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticAccident.Modules
{
    public class Accident
    {
        public DateTime AccidentDay { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime Time { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string TrafficwayIdentifier{ get; set; }
        public string BodyType { get; set; }
        public string  SeatingPosition { get; set; }
        public string PersonType { get; set; }
        public string ProtectionSystemUse { get; set; }
        public string  Ejection { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string HispanicOrigin { get; set; }
    }
}
