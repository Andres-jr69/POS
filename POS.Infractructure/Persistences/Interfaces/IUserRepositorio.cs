using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infractructure.Persistences.Interfaces
{
    public interface IUserRepositorio : IGenericRepository<User>
    {
        Task<User> AccountByUserName(string userName);
    }
}
