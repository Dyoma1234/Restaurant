using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager
{
    interface ILogin
    {
        void Close();
        string GetPassword();
    }
}
