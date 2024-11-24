using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Common
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode {  get; set; }

        public bool IsSuccess { get; set; } = false;

        public string DisplayMessege { get; set; } = "";

        public object Result { get; set; }

        public List<APIError> Errors { get; set; } = new();

        public List<APIWarning> Warnings { get; set; }=new();

        public void AddError(string errorMessege)
        {
            APIError error = new APIError(description : errorMessege);
            Errors.Add(error);

        }

        public void AddWarning(string warningMessege)
        {
            APIWarning warning = new APIWarning(description: warningMessege);
            Warnings.Add(warning);

        }
    }
}
