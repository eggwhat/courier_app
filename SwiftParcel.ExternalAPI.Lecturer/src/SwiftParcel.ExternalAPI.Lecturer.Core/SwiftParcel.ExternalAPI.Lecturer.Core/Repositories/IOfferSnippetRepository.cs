using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Core.Repositories
{
    public interface IOfferSnippetRepository
    {
        Task<OfferSnippet> GetAsync(Guid id);
        Task AddAsync(OfferSnippet offerSnippet);
        Task UpdateAsync(OfferSnippet offerSnippet);
    }
}