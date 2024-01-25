﻿namespace SwiftParcel.ExternalAPI.Lecturer.Application.DTO
{
    public class OfferRequestDto
    {
        public Guid InquiryId { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public InquiryAddressDto Address { get; set; }
    }
}