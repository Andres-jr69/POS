using POS.Infractructure.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infractructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o matricula de nuestras interfaces a nivel de repository
        ICategoryRepository category {  get; }
        IUserRepositorio user {  get; }
        IAzureStorage Storage { get; }
        void SaveChange();
        Task SaveChangesAsync();

    }
}
