using SwiftParcel.ExternalAPI.Baronomat.Core.Entities;

namespace SwiftParcel.ExternalAPI.Baronomat.Core.Repositories
{
    public interface IInquiryOfferRepository
    {
        Task<InquiryOffer> GetAsync(Guid id);
        Task AddAsync(InquiryOffer inquiryOffer);
    }
}