using EShop.UserService.Application.Features.Users.Responses;
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

        public GetAllUsersQueryHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }

        public async Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWorks.UserRepository.GetAllAsync();
            var response = new List<GetAllUsersQueryResponse>();

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
