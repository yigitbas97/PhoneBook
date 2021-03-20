using Microsoft.EntityFrameworkCore;
using PhoneBook.Context;
using PhoneBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.EntitiesDAO
{
    public class PersonDAO : IPersonDAO
    {
        public void Add(Person person)
        {
            using (PhoneBookContext context = new PhoneBookContext())
            {
                var addedObject = context.Entry(person);
                addedObject.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Person person)
        {
            using (PhoneBookContext context = new PhoneBookContext())
            {
                var deletedObject = context.Entry(person);
                deletedObject.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<Person> GetAll()
        {
            using (PhoneBookContext context = new PhoneBookContext())
            {
                return context.People.ToList();
            }
        }

        public Person GetById(int id)
        {
            using (PhoneBookContext context = new PhoneBookContext())
            {
                return context.People.Find(id);
            }
        }

        public void Update(Person person)
        {
            using (PhoneBookContext context = new PhoneBookContext())
            {
                var updatedObject = context.Entry(person);
                updatedObject.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
