using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AntsaWeatherApp
{   //Root container for data parsed from yahoo. Just few fields are marked from whole data, just to check that concept works
    [DataContract]
    class WeatherData
    {
        [DataMember]
        public Query query { get; set; }

        public override string ToString()
        {
            return "WeatherData:" + query;
        }

    }

  
}
