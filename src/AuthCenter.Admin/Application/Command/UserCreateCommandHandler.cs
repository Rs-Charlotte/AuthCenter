using AuthCenter.Domain.Entities;
using AuthCenter.Infrastructure.Repositories;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, UserCreatedViewModel>
    {
        private readonly AppUserManager _userManager;
        private readonly AppRoleManager _roleManager;

        public UserCreateCommandHandler(AppUserManager userManager, AppRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserCreatedViewModel> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            // 数据验证

            // 对象映射
            List<string> errs = new List<string>();
            var newUser = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.Phone
            };
            
            // 数据存储
            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(newUser.Id.ToString());
                var addRoleResult = await _userManager.AddToRoleAsync(user, "user");
                if (addRoleResult.Succeeded)
                {
                    return new UserCreatedViewModel() { Id = newUser.Id, Errs = null };
                }
                else
                {
                    errs.AddRange(addRoleResult.Errors.Select(x => x.Description));
                }
            }
            errs.AddRange(result.Errors.Select(x => x.Description));

            return new UserCreatedViewModel() { Id = null, Errs = errs };
        }
    }
}
