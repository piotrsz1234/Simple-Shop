using System.Threading.Tasks;
using Data.Dto.Models;

namespace Common.Bll.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddEditProductAsync(AddEditProductModel model);
    }
}