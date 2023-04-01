using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase
{
    public interface IAccountPersistent
    {
        public bool Match(string accountID);
    }
}
