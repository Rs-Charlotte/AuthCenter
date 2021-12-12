using AuthCenter.Infrastructure.Repositories;
using EatMeat.Core.Common.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.Admin.Application.Queries.Roles
{
    public class RolesQueryHandler : IRequestHandler<RolesQuery, MessageModel>
    {
        private readonly AppRoleManager _roleManager;
        public RolesQueryHandler(AppRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<MessageModel> Handle(RolesQuery request, CancellationToken cancellationToken)
        {
            //var role = await

            //数据查询
            var roles = await _roleManager.Roles.AsNoTracking()
                .Where(p => p.IsDeleted == request.IsDeleted)
                .OrderBy(p => p.CreateTime)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new RoleViewModel()
                {
                    Guid = u.Id.ToString(),
                    Name = u.Name
                })
                .ToListAsync();
            //
            return MessageModel.Ok(null,"查找成功",roles);
        }
    }
}
