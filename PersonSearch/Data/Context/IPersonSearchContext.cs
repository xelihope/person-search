using PersonSearch.Data.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PersonSearch.Data.Context
{
    public interface IPersonSearchContext
    {
        DbSet<Person> People { get; set; }

        DbSet<Address> Addresses { get; set; }

        DbSet<Hobby> Hobbies { get; set; }

        Task<int> SaveChangesAsync();
    }
}
