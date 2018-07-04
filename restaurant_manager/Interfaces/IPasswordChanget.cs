using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager
{
    interface IPasswordChanget
    {
        string GetPassword_F();
        string GetPassword_S();
        void ClearPassword();

    }
}
