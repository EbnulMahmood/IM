using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Management.CoreBusiness.Entities;

namespace Inventory.Management.UseCases.Categories.Contracts
{
    public interface IViewCategoriesUseCase
    {
        Task<IEnumerable<Category>> ExecuteAsync();
    }
}