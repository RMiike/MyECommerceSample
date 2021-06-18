using MECS.Core.Domain.Entities;
using MECS.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Interfaces
{
    public interface IPurshaseBFFService
    {
        Task<CartViewModel> Get();
        Task<int> GetQuantity();
        Task<ResponseResult> AddItem(ItemProductViewModel model);
        Task<ResponseResult> UpdateItem(Guid idProduct, ItemProductViewModel model);
        Task<ResponseResult> RemoveItem(Guid idProduct);
    }
}
