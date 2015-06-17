using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Jupiter.Models.Response
{
    [DataContract]
    public abstract class BaseResponseData
    {
        [DataMember(Name = "ErrorCode", EmitDefaultValue = false)]
        public int errorCode;
    }
}