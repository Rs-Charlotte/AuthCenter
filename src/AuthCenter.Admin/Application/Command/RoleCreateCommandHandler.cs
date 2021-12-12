using AuthCenter.Domain.Entities;
using AuthCenter.Infrastructure.Repositories;
using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, MessageModel>
    {
        private readonly AppRoleManager _roleManager;
        public RoleCreateCommandHandler(AppRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<MessageModel> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            //对象映射
            var newRole = new Role()
            {
                Name = request.RoleName
            };
            //数据存储
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return MessageModel.Ok(null,"成功创建新角色",1);
            }
            return MessageModel.Failure(null,"创建角色失败",0);
        }
    }
}
