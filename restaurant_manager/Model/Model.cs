using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_manager
{
    class Model
    {
        public Model1Container db { set; get; }
        public Model()
        {
            db = new Model1Container();
        }
    }
}
