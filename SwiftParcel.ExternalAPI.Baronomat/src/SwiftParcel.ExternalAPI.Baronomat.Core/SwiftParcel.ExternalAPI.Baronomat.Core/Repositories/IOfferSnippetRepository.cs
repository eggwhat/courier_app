using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Core.Repositories
{
    public interface IOfferSnippetRepository
    {
        Task<OfferSnippet> GetAsync(Guid id);
        Task AddAsync(OfferSnippet offerSnippet);
        Task UpdateAsync(OfferSnippet offerSnippet);
    }
}