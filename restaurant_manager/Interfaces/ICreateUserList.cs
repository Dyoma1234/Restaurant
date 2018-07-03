using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager
{
   public interface ICreateUserList
    {
       void AddUser(int id);
       void EraseUser(int id);
       void InitUserList();
       void Clear();
    }
}
