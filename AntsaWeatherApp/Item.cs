using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace AntsaWeatherApp
{
    [DataContract]
    class Item
    {
        [DataMember]
        public AntsaWeatherApp.Condition condition { get; set; }
    }
}
