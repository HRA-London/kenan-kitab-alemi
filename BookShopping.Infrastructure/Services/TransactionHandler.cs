using System;
using BookShopping.Application.Interfaces;
using BookShopping.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookShopping.Infrastructure.Services
{
    public class TransactionHandler : ITransactionHandler
    {
        private readonly ApplicationDbContext _context;
        public TransactionHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public Guid GetTransactionId()
        {

            return _context.Database.CurrentTransaction == null ?
                    Guid.Empty :
                    _context.Database.CurrentTransaction.TransactionId;
        }

        public async Task RoleBackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}

