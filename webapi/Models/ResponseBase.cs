using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Models.Enums;

namespace webapi.Models{
    public class ResponseBase{
        public ReturnCodes ReturnCode { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }        

        public IEnumerable<Object> List_Data {get;set;}
    }
}