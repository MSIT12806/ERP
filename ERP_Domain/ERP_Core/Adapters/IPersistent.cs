using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core.Adapters
{
    public interface IEmployeePersistent
    {
        void Edit(Employee newData);
        void Remove(string id);
        void Add(Employee data);
        Employee Get(string id);
        IEnumerable<Employee> GetList();
    }
    //public interface IAddable
    //{
    //    void Add<Model>(Model data);
    //}
    //public interface IRemoveable
    //{
    //    void Remove(string id);
    //}
    //public interface IEditable
    //{
    //    void Edit<Model>(Model newData);
    //}
}
