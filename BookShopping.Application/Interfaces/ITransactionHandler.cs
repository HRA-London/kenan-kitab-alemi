using System;
namespace BookShopping.Application.Interfaces
{
    public interface ITransactionHandler
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RoleBackTransactionAsync();
        Guid GetTransactionId();
    }
}

