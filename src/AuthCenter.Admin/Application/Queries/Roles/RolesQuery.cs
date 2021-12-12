using EatMeat.Core.Common.Result;
using MediatR;

namespace AuthCenter.Admin.Application.Queries.Roles
{
    public class RolesQuery:IRequest<MessageModel>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsLocked { get; set; } = false;
        /// <summary>
        /// 当前页（从1开始）
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页最大多少条数据
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
