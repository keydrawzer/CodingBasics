namespace CodingBasics.Services

{
    using System;
    using System.Linq;
    using CodingBasics.Models;

    public class PersonService
    {
        private readonly AdventureWorks2022Context _context;

        public PersonService(AdventureWorks2022Context context)
        {
            _context = context;
        }
        public IEnumerable<Person> GetAllPersons()
        {
            return _context.People.ToList();
        }

        public IEnumerable<Person> GetPersonsByName(string name)
        {
            return _context.People
                .Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public IEnumerable<Person> GetPersonsByType(string type)
        {
            return _context.People
                .Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(p.PersonType.ToLower()))
                .ToList();
        }

        public List<Person> GetPersonsByNameAndType(string name, string type)
        {
            return _context.People
                .Where(p => (p.FirstName + " " + p.LastName).Equals(name) && p.PersonType == type)
                .ToList();
        }

    }

}
