using EShop.UserService.Application.Features.Users.Responses;
using EShop.UserService.Infrastructure.Cachings.ICachingService;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.UserService.Application.Features.Users.Queries.GetAll
{
    public sealed record GetAllUsersQuery : IRequest<List<GetAllUsersQueryResponse>>;
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly ICacheService _cacheService;
        public GetAllUsersQueryHandler(IUnitOfWorks unitOfWorks, ICacheService cacheService)
        {
            _unitOfWorks = unitOfWorks;
            _cacheService = cacheService;
        }

        public async Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWorks.UserRepository.GetAllAsync();
            var response = new List<GetAllUsersQueryResponse>();

            //await _cacheService.SetCacheAsync("GetAllUsers", users, TimeSpan.FromMinutes(5)); //test

            foreach (var user in users)
            {
                response.Add(new GetAllUsersQueryResponse(
                    user.Id.ToString(),
                    user.PhoneNumber,
                    user.FullName,
                    user.Email,
                    user.Address
                ));
            }

            return response;
        }
    }
}
