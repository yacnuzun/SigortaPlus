using InsuranceApp.Core.DTO_s;
using InsuranceApp.Core.Interfaces;
using System;

namespace InsuranceApp.Core.Entities
{
    public class OfferElasticDto: BaseElasticSearchEntity, ISmartSearchConvertible
    {
        public string OfferCode { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidUntil { get; set; }

        public int CustomerId { get; set; }
        public CustomerElasticDto Customer { get; set; }

        public static OfferElasticDto GetViewModel(Offer offer) { 
            return new OfferElasticDto()
            {
                OfferCode = offer.OfferCode,
                Price = offer.Price,
                ValidUntil = offer.ValidUntil,
                CustomerId = offer.CustomerId,
                UpdatedDate = offer.UpdatedDate,
                IsDeleted = offer.IsDeleted,
                Id = offer.Id,
                CreatedDate = offer.CreatedDate,
                Customer = CustomerElasticDto.GetViewModel(offer.Customer),
                Description = offer.OfferCode,
                Title = offer.OfferCode,
                Type = "Offer",
                Url = "/offer/offer.OfferCode"
            }; 
        }

        public BaseElasticSearchEntity ToSmartSearchEntity()
        {
            return new BaseElasticSearchEntity
            {
                CreatedDate = this.CreatedDate,
                Description = this.Description,
                Id = this.Id,
                IsDeleted = this.IsDeleted,
                Title = this.Title,
                Type = this.Type,
                UpdatedDate = this.UpdatedDate,
                Url = this.Url,
            };
        }
    }
}
