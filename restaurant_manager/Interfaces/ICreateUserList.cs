using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager
{
   public interface ICreateUserList
    {
       void AddUser(int id, string name, string lastname, string login, string password);
       void InintNav();
       void Clear();
    }
}
