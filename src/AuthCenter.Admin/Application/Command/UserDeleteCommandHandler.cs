using AuthCenter.Infrastructure.Repositories;
using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, MessageModel>
    {
        private readonly AppUserManager _userManager;
        public UserDeleteCommandHandler(AppUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<MessageModel> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            //查找用户
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            //删除用户
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return MessageModel.Ok(null,"删除成功",1);
            }
            return MessageModel.Failure(null, "删除失败", 0);
        }
    }
}
