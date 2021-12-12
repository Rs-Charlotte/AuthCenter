using AuthCenter.Infrastructure.Repositories;
using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    /// <summary>
    /// 用于更新用户的用户名，邮箱，手机号
    /// </summary>
    public class UserUpdateCommandHandler:IRequestHandler<UserUpdateCommand, MessageModel>
    {
        private readonly AppUserManager _userManager;
        public UserUpdateCommandHandler(AppUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageModel> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            //查找用户
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            //更改用户信息
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PhoneNumber = request.PhoneNumber;
            //数据存储
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return MessageModel.Ok(null,"更新成功",1);
            }
            return MessageModel.Failure(null,"更新失败",0);
        }
    }
}
