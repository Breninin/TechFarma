using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfTechPharma.Interfaces
{
    interface IDAO<T>
    {
        string Insert(T t);

        void Update(T t);

        void Delete(T t);

        List<T> List();

        T GetById(int id);
    }
}
