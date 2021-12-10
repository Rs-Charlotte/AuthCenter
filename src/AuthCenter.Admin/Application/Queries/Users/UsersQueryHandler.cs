using AuthCenter.Domain.Entities;
using AuthCenter.Infrastructure.Repositories;
using EatMeat.Core.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.Admin.Application.Queries.Users
{
    public class UsersQueryHandler : IRequestHandler<UsersQuery, PageModel<UserViewModel>>
    {
        private readonly AppUserManager _userManager;

        public UsersQueryHandler(AppUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<PageModel<UserViewModel>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            // 数据验证
            if (request.PageIndex <= 0 && request.PageSize <= 0)
            {
                throw new Exception("pageIndex或pageSize不能小于等于0！");
            }

            // 数据查询
            var users = await _userManager.Users.AsNoTracking()
                .Where(p => p.IsDeleted == request.IsDeleted)
                .OrderBy(p => p.CreateTime)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserViewModel()
                {
                    Guid = u.Id.ToString(),
                    UserName = u.UserName,
                    Email = u.Email,
                    Phone = u.PhoneNumber
                })
                .ToListAsync();

            // 数据映射
            var totalItems = await _userManager.Users
                .Where(p => p.IsDeleted == request.IsDeleted)
                .CountAsync();
            var res = new PageModel<UserViewModel>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = users,
                TotalItems = totalItems,
                TotalPages = totalItems / request.PageSize
            };

            // 事件转发

            return res;
        }
    }
}
