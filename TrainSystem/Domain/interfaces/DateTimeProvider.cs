using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Train
{
    public interface IDateTimeProvider
    {
        public DateTime Now();
    }
    public class MyDateTimeProvider : IDateTimeProvider
    {
        public static IDateTimeProvider Ins;
        public DateTime Now()
        {
            return MyDateTimeProvider.Ins.Now();
        }
    }
}
