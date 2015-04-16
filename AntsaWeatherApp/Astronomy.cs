using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AntsaWeatherApp
{
    [DataContract]
    class Astronomy
    {
         [DataMember]
        public string sunset{get;set;}
         [DataMember]
         public string sunrise { get; set; }
    }
}
