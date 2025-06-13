using InsuranceApp.Core.DTO_s;
using InsuranceApp.Business.Services.Interfaces;
using InsuranceApp.Core.EntityFramework;
using InsuranceApp.Core.Helpers.ResponseModels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using InsuranceApp.Core.UnitofWork;
using InsuranceApp.Core.Entities;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using InsuranceApp.Core.ElasticSearch;
using System.Linq;

namespace InsuranceApp.Business
{
    public class OfferManager : IOfferService
    {
        private readonly IEfOfferDal _efOfferDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEfCustomerDal _efCustomerDal;
        private readonly IEfHealthPolicyDal _efHealthPolicyDal;
        private readonly ILogger<OfferManager> _logger;
        private readonly IGenericElasticRepository<OfferLog> _elasticLogger;
        private readonly IGenericElasticRepository<CustomerElasticDto> _elasticCustomerLogger;
        private readonly IGenericElasticRepository<OfferElasticDto> _elasticOfferLogger;
        private readonly IGenericElasticRepository<BaseElasticSearchEntity> _baseElasticSearchEntity;

        public OfferManager(IEfOfferDal efOfferDal,
            ILogger<OfferManager> logger,
            IUnitOfWork unitOfWork,
            IEfCustomerDal efCustomerDal,
            IGenericElasticRepository<OfferLog> elasticLogger,
            IGenericElasticRepository<CustomerElasticDto> elasticCustomerLogger,
            IGenericElasticRepository<OfferElasticDto> elasticOfferLogger,
            IGenericElasticRepository<BaseElasticSearchEntity> baseElasticSearchEntity,
            IEfHealthPolicyDal efHealthPolicyDal)
        {
            _efOfferDal = efOfferDal;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _efCustomerDal = efCustomerDal;
            _elasticLogger = elasticLogger;
            _elasticCustomerLogger = elasticCustomerLogger;
            _elasticOfferLogger = elasticOfferLogger;
            _baseElasticSearchEntity = baseElasticSearchEntity;
            _efHealthPolicyDal = efHealthPolicyDal;
        }

        public async Task<string> GenerateRandomCode()
        {
            Offer offer;
            string code;
            do
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                code = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                offer = await _efOfferDal.GetAsync(o => o.OfferCode.ToUpper() == code);
            } while (offer != null);
            return code;
        }

        public async Task<IDataResult<List<OfferResponseDto>>> GetOffer(OfferRequestDto dto)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var customerInfo = await _efCustomerDal.GetAsync(
                    c => c.FullName == dto.FullName &&
                    (c.Gender == 1) == dto.Gender &&
                    c.IsDeleted == false &&
                    c.Age == dto.Age);
                bool newUser = false;
                if (customerInfo == null)
                {
                    customerInfo = await _efCustomerDal.AddAsync(
                        new Customer
                        {
                            FullName = dto.FullName,
                            Age = dto.Age,
                            Gender = (dto.Gender ? 1 : 0),
                        });
                    await _unitOfWork.CommitAsync();
                    newUser = true;
                }

                var result = _efOfferDal.GetMatchingHealthPolicies(customerInfo);

                var healthPolicyTasks = result.HealthKeys.Select(h=> _efHealthPolicyDal.GetAsync(p=> p.Id == h && p.IsDeleted == false)).ToList();

                var healts = (await Task.WhenAll(healthPolicyTasks)).ToList();

                var offerEntity = new Offer
                {
                    IsDeleted = false,
                    OfferCode = GenerateRandomCode().Result,
                    CustomerId = customerInfo.Id,
                    OfferHealthPolicies = healts.Select(h => new OfferHealthPolicy { HealthPolicyId = h.Id }).ToList()
                };

                var createdOffer = await _efOfferDal.AddAsync(offerEntity);

                await _unitOfWork.CommitAsync();
                _unitOfWork.CommitTransaction();
                
                var response = healts.Select(policy => new OfferResponseDto
                {
                    ResponseTitle = $"Health Policy #{policy.PolicyNumber}",
                    ResponseDescription = $"Valid from {policy.StartDate:yyyy-MM-dd} to {policy.EndDate:yyyy-MM-dd}",
                    PremiumPrice = 1000, // Buraya opsiyonel olarak bir hesaplama veya sabit değer gelebilir
                    ValidUntil = policy.EndDate
                }).ToList();

                

                if (newUser)
                {
                    var customerElasticDto = CustomerElasticDto.GetViewModel(customerInfo);
                    await _elasticCustomerLogger.IndexAsync(customerElasticDto);
                    await _baseElasticSearchEntity.IndexAsync(customerElasticDto.ToSmartSearchEntity());
                }

                var offerElasticDto = OfferElasticDto.GetViewModel(offerEntity);
                await _elasticOfferLogger.IndexAsync(offerElasticDto);
                await _baseElasticSearchEntity.IndexAsync(offerElasticDto.ToSmartSearchEntity());


                var logEntry = new OfferLog
                {
                    OfferId = offerEntity.Id,
                    Action = nameof(OfferManager.GetOffer),
                    Message = $"Teklif verildi. \n Detay : {response}"
                };
                await _elasticLogger.IndexAsync(logEntry);

                return new SuccessDataResult<List<OfferResponseDto>>(response);
            }
            catch (System.Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError($"Teklif sürecinde bir hata oldu. Detay : {ex.Message}");
                var logEntry = new OfferLog
                {
                    Action = nameof(OfferManager.GetOffer),
                    Message = $"Teklif sürecinde bir hata oldu. Detay : {ex.Message}",
                };
                await _elasticLogger.IndexAsync(logEntry);
                throw;
            }
            
        }
    }
}
