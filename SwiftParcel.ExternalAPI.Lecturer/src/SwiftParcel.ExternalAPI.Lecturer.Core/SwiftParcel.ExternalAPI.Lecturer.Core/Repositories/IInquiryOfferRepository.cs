using SwiftParcel.ExternalAPI.Lecturer.Core.Entities;

namespace SwiftParcel.ExternalAPI.Lecturer.Core.Repositories
{
    public interface IInquiryOfferRepository
    {
        Task<InquiryOffer> GetAsync(Guid id);
        Task AddAsync(InquiryOffer inquiryOffer);
    }
}