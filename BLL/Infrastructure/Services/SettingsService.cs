using System;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interface;
using BLL.Specifications;

namespace BLL.Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SettingsService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Contact> GetContacts()
        {
            ContactSpecification spec = new ContactSpecification();
            var contact = await _unitOfWork.Repository<Contact>().GetEntitiesWithSpec(spec);
            return contact;
        }

        public async Task<int> AddAddress(Contact address)
        {
            ContactSpecification spec = new ContactSpecification();
            var contact = await _unitOfWork.Repository<Contact>().GetEntitiesWithSpec(spec);
            if (contact == null)
            {
                _unitOfWork.Repository<Contact>().Add(address);

            }
            else
            {
                contact.Address = address.Address ?? contact.Address;
                contact.City = address.City ?? contact.City;
                contact.State = address.State ?? contact.State;
                contact.Country = address.Country ?? contact.Country;
                contact.Email = address.Email ?? contact.Email;
                contact.PhoneNumber = address.PhoneNumber ?? contact.PhoneNumber;
                _unitOfWork.Repository<Contact>().Update(contact);
            }

            int commit = await _unitOfWork.Complete();
            return commit;
        }

        public async Task<int> AddSocialMedia(Contact social)
        {
            ContactSpecification spec = new ContactSpecification();
            var contact = await _unitOfWork.Repository<Contact>().GetEntitiesWithSpec(spec);
            if (contact == null)
            {
                _unitOfWork.Repository<Contact>().Add(social);

            }
            else
            {
                contact.Facebook = social.Facebook ?? contact.Facebook;
                contact.Instagram = social.Instagram ?? contact.Instagram;
                contact.Youtube = social.Youtube ?? contact.Youtube;
                contact.Twitter = social.Twitter ?? contact.Twitter;
                _unitOfWork.Repository<Contact>().Update(contact);
            }

            int commit = await _unitOfWork.Complete();
            return commit;
        }

        public async Task<Banner> GetBanner()
        {
            BannerSpecification spec = new BannerSpecification();
            var banner = await _unitOfWork.Repository<Banner>().GetEntitiesWithSpec(spec);
            return banner;
        }


    }
}
