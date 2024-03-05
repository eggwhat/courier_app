using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Core.Repositories
{
    public interface IOfferSnippetRepository
    {
        Task<OrderSnippet> GetAsync(Guid id);
        Task AddAsync(OrderSnippet offerSnippet);
        Task UpdateAsync(OrderSnippet offerSnippet);
    }
}