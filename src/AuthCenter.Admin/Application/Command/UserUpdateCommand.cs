using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    public class UserUpdateCommand:IRequest<MessageModel>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
