using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace AntsaWeatherApp
{
     [DataContract]
    class Query
    {

         [DataMember]
      public  Results results { get; set; }

        


        public override string ToString() {
            return "Query:" + results;
        }
    }
}
