using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IM.UseCases.PluginInterfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }

        void Dispose();
        Task<bool> SaveAsync();
    }
}