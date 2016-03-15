using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TimeControl.Models
{
    public class Person : IdentityUser
    {
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
    }
 
    public class PersonDatabase : List<Person>
    {
        public PersonDatabase()
        {
            Add(new Person() { PersonId=Guid.NewGuid(),PersonName="MS"});
            Add(new Person() { PersonId = Guid.NewGuid(), PersonName = "SA" });
            Add(new Person() { PersonId = Guid.NewGuid(), PersonName = "VP" });
        }
    }
}