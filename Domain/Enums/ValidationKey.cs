using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ValidationKey
    {
        ServerError = 600,
        NotExist = 601,
        InvalidCredentialsTryAgain = 602,
        YouHaveExcedRetrailCount = 603,
        AccountIslLocked = 604,
        AccountNotExist = 605,
        MobileAndEmailNotAvailable = 606,
        FailedToChangePassword = 607,
        WrongOldPassword = 608,
        UserNameAlreadyExist = 609,
        InvalidParameters = 610,
        InvalidDate = 611,
    }

    public static class Extensions
    {
        public static string GetValueAsString(this ValidationKey value)
        {
            var code = (long)value;
            return code.ToString();
        }
    }
}
