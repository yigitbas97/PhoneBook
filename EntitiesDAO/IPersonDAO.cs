using PhoneBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.EntitiesDAO
{
    public interface IPersonDAO
    {
        List<Person> GetAll();
        void Add(Person person);
        void Update(Person person);
        void Delete(Person person);
        Person GetById(int id);
    }
}
