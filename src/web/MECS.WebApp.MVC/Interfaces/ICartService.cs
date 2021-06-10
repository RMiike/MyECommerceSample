using MECS.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace MECS.WebApp.MVC.Interfaces
{
    public interface ICartService
    {
        Task<CartViewModel> Get();
        Task<ResponseResult> AddItem(ItemProductViewModel model);
        Task<ResponseResult> UpdateItem(Guid idProduct, ItemProductViewModel model);
        Task<ResponseResult> RemoveItem(Guid idProduct);


    }
}
