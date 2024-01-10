using AutoMapper;
using DigitalBankApi.Data;
using DigitalBankApi.DTOs;
using DigitalBankApi.Interfaces.IRepositories;
using DigitalBankApi.Interfaces.IServices;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories;
using FluentValidation;

namespace DigitalBankApi.Services
{
    public class SupportRequestService : ISupportRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<SupportRequestDto> _validator;

        public SupportRequestService(AdminDbContext context, IMapper mapper, IValidator<SupportRequestDto> validator)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<SupportRequestDto> Create(SupportRequestDto supportRequest)
        {
            var supportRequestValid = _validator.Validate(supportRequest);

            if (!supportRequestValid.IsValid)
            {
                var errorMessages = supportRequestValid.Errors
                    .Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToList();

                throw new Exception(string.Join(Environment.NewLine, errorMessages));
            }

            var entity = _mapper.Map<SupportRequestDto, SupportRequests>(supportRequest);

            await _unitOfWork.SupportRequests.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SupportRequestDto>(supportRequest);
        }

        public async Task<List<SupportRequestDto>> ListCustomerSupportRequest(int customerId)
        {
            if (await _unitOfWork.Customers.GetAsync(customerId) == null)
            {
                throw new Exception("Customer not found");
            }

            var allSupportRequests = await _unitOfWork.SupportRequests.GetAllAsync();

            var customerSupportRequests = allSupportRequests.Where(request => request.CustomerId == customerId).ToList();

            var supportRequestDtos = _mapper.Map<List<SupportRequestDto>>(customerSupportRequests);

            return supportRequestDtos;
        }

        public async Task<SupportRequests> ListSupportRequest(int supportRequestId)
        {
            if (await _unitOfWork.SupportRequests.GetAsync(supportRequestId) == null)
            {
                throw new Exception("Support request not found.");
            }

            var showSupportRequest = await _unitOfWork.SupportRequests.GetAsync(supportRequestId);

            return showSupportRequest;
        }

        public async Task<AnswerRequestDto> FirstNotAnsweredRequest()
        {
            var firstUnansweredRequest = (await _unitOfWork.SupportRequests.GetAllAsync())
                .FirstOrDefault(request => !request.isAnswered);

            if (firstUnansweredRequest == null)
            {
                throw new Exception("Support request not found.");
            }

            var entity = _mapper.Map<SupportRequests, AnswerRequestDto>(firstUnansweredRequest);

            return entity;
        }

        public async Task<AnswerRequestDto> AnswerRequest(int id, string answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer));

            var supportRequest = await _unitOfWork.SupportRequests.GetAsync(id);

            if (supportRequest != null)
            {
                supportRequest.isAnswered = true;
                supportRequest.Answer = answer;
                supportRequest.AnswerDate = DateTime.Now.ToUniversalTime();

                await _unitOfWork.SupportRequests.UpdateAsync(supportRequest);

                await _unitOfWork.CompleteAsync();

                return _mapper.Map<AnswerRequestDto>(supportRequest);
            }

            throw new Exception("An error occurred");
        }
    }
}
