using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    public class UserDeleteCommand:IRequest<MessageModel>
    {
        public Guid UserId { get; set; }
    }
}
