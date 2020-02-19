using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models.Enums{
    public enum ReturnCodes
    {
        Succeed = 1,
        Faild = 2,

        InternalServerError = -99999
    }

}