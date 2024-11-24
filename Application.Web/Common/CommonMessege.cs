using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Common
{
    public static class CommonMessege
    {

        public const string RegistrationSuccess = "Registration Success";
        public const string RegistrationFailed = "Registration Failed";

        public const string LoginSuccess = "Login Success";
        public const string LoginFailed = "Login Failed";


        public static string CreateOperationSuccess = "Record Created Successfully";
        public static string UpdateOperationSuccess = "Record Updated Successfully";
        public static string DeleteOperationSuccess = "Record Delete Successfully";

        public static string CreateOperationFailed = "Created Operation Failed";
        public static string UpdateOperationFailed = "Updated Operation Failed";
        public static string DeleteOperationFailed = "Deleted Operation Failed";

        public static string RecordNotFound = "Record Not Found";
        public static string SystemError = "Something Went Wrong";
    }
}
