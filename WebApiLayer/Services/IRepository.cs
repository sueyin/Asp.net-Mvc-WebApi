using System;
using System.Collections.Generic;

namespace Gateway.Services
{
   interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        bool Create(T item);

        bool Edit(T item);

        void Delete(int id);
    }
}
