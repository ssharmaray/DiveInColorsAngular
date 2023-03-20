using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiValidatioErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors {get; set;}
        public ApiValidatioErrorResponse() : base(400)
        {
        }
    }
}