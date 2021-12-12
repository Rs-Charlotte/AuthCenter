using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Command
{
    public class RoleCreateCommand:IRequest<MessageModel>
    {
        public string RoleName { get; set; }
    }
}
