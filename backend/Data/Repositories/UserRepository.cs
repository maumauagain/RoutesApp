using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context)
            : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return Query(u => u.IsActive);
        }
    }
}
