using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace AntsaWeatherApp
{
    [DataContract]
    class Channel
    {
        [DataMember]
        private AntsaWeatherApp.Location location { get; set; }
        [DataMember]
        private AntsaWeatherApp.Units country { get; set; }
        [DataMember]
        public string title { get; set; }

        public override string ToString()
        {
            return "Channel:" + location + "  title " + title;
        }
    }
}
