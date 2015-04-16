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
         public AntsaWeatherApp.Location location {  get; set; }
       
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public AntsaWeatherApp.Astronomy astronomy { get; set; }

        [DataMember]
        public AntsaWeatherApp.Item item {get;set;}


        public override string ToString()
        {
            return "Channel:" + location + "  title " + title+ " astronomy:"+astronomy;
        }
    }
}
