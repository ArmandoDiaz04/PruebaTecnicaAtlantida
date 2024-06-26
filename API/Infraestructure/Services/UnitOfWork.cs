using API.Core.Application.Contracts;
using API.Core.Application.Contracts.Repositories;
using API.Core.Domain.Generic;
using API.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

namespace API.Infraestructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TarjetaCreditoDbContext context;
        private Hashtable _repositories;
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;
        private IRepositoryTarjeta repositoryTarjeta;
        private IRepositoryMovimiento repositoryMovimiento;


        public UnitOfWork(TarjetaCreditoDbContext _context, IConfiguration configuration)
        {
            if (context is null)
                context = _context;

            _configuration = configuration;
        }

        public IRepositoryTarjeta RepositoryTarjeta => repositoryTarjeta ??= new RepositoryTarjeta(OpenConnectionDapper());

        public IRepositoryMovimiento RepositoryMovimiento => repositoryMovimiento ??= new RepositoryMovimiento(OpenConnectionDapper());

        public void Dispose()
        {
            if (context is not null)
            {
                context.Dispose();
            }

            if (_connection is not null)
            {
                _connection.Dispose();
            }

        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> TransactionScopeAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomain
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        private SqlConnection OpenConnectionDapper()
        {
            if (_connection is null)
                _connection = new SqlConnection(_configuration.GetConnectionString("default"));

            return _connection;
        }
    }
}
