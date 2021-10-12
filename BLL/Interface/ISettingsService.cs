using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Interface
{
    public interface ISettingsService
    {
        Task<int> AddAddress(Contact address);
        Task<int> AddSocialMedia(Contact social);
        Task<Banner> GetBanner();
        Task<Contact> GetContacts();
    }
}