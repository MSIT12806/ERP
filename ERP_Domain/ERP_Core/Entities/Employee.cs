using ERP_Core.Adapters;

namespace ERP_Core
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsManager { get; set; }
        public string DepartmentID { get; set; }

        public void Join(IEmployeePersistent db)
        {
            db.Add(this);
        }
        public void Leave(IEmployeePersistent db)
        {
            db.Remove(this.Id);
        }
        public void EditData(IEmployeePersistent db)
        {
            db.Edit(this);
        }
    }
}