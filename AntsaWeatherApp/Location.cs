using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace AntsaWeatherApp
{
    [DataContract]
    class Location
    {
        [DataMember] 
        public string city { get; set; }
        [DataMember] 
        public string country { get; set; }
        [DataMember] 
        public string region { get; set; }

        public override string ToString()
        {
            return "Location:" + city+" country:"+country+ " region:"+region;
        }
    }
}
