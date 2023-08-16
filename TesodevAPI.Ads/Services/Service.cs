using TesodevAPI.Ads.Repositories;
using TesodevAPI.Ads.Models;
using TesodevAPI.Ads.Controllers;
using MongoDB;

namespace TesodevAPI.Ads.Services
{
    //business logiclerin kosacagi katman
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;
        }

       /* public List<Models.Merchant> GetAll()
        {
            var merchant = _repository.Get();
            return merchant;
        }*/
        public void Create(Models.Merchant merchant)
        {
            _repository.Create(merchant);
        }

    };
}
