using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Train
{
    public class Notify:Exception
    {
        public Notify(string message) : base(message)
        {
        }
    }
}
