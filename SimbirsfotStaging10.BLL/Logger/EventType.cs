using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbirsfotStaging10.Logger
{
    public enum EventType
    { 
        RegistrationSucces=1,
        RegistrationFail,
        SignInSucces,
        SignInFail,
        Logout,
        CreateCardWithPlatforms
    }
}
