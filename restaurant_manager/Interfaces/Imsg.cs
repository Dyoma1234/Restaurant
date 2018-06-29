using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager.Interfaces
{
    interface Imsg
    {
        void ShowInfo(string msg, string cap);
        void ShowWarning(string msg,string cap);
    }
}
