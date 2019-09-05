using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RequestManager.Model
{
    public class Error
    {
        public Error()
        {
            Code = (int)HttpStatusCode.InternalServerError;
        }
        public Error(Exception exception) : this()
        {
            Exception = exception;
            Message = exception.Message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
    }
}
