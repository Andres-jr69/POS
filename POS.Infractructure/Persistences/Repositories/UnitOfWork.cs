using Microsoft.Extensions.Configuration;
using POS.Infractructure.FileStorage;
using POS.Infractructure.Persistences.Context;
using POS.Infractructure.Persistences.Interfaces;

namespace POS.Infractructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PosContext _context;
        public ICategoryRepository category {  get; private  set; }

        public IUserRepositorio user { get; private set; }

        public IAzureStorage Storage { get; private set; }

        public UnitOfWork(PosContext context, IConfiguration configuration)
        {
            _context = context;
            category = new CategoryRepository(_context);
            user = new UserRepository(_context);
            Storage = new AzureStorage(configuration);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
