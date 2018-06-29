using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager
{
  public static class Cur_session
    {
         public  static void New_session(string userName, string password, string name, string lastName, string pos,string phone)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Pos = pos ?? throw new ArgumentNullException(nameof(pos));
            PhoneNumber = phone ?? throw new ArgumentNullException(nameof(phone));

        }

        public static string UserName { set; get; }
        public static string Password { set; get; }
        public static string Name { set; get; }
        public static string LastName { set; get; }
        public static string Pos { set; get; }
        public static string PhoneNumber { set; get; }
    }
}
