using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core.Adapters
{
    public interface IAddable<Model>
    {
        void Add<Model>();
    }
    public interface IRemoveable
    {
    }
    public interface IEditable<Model>
    {
        Model Edit();
    }
}
