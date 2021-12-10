using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace EatMeat.Core.Common.Result
{
    public class MessageModel : ActionResult, IStatusCodeActionResult, IActionResult
    {
        public int? StatusCode { get; private set; }
        public bool IsSuccessed { get; private set; }
        public List<string> Errors { get; private set; }
        public int? MessageId { get; private set; }
        public string Message { get; private set; }
        public object Response { get; private set; }

        public static MessageModel Ok(int? msgId, string msg, object res)
        {
            return new MessageModel()
            {
                StatusCode = 200,
                IsSuccessed = true,
                MessageId = msgId,
                Message = msg,
                Response = res
            };
        }

        public static MessageModel Created(int? msgId, string msg, object res)
        {
            return new MessageModel()
            {
                StatusCode = 201,
                IsSuccessed = true,
                MessageId = msgId,
                Message = msg,
                Response = res
            };
        }
        public static MessageModel Failure(int? msgId, string msg, object res)
        {
            return new MessageModel()
            {
                StatusCode = 400,
                IsSuccessed = false,
                MessageId = msgId,
                Message = msg,
                Response = res
            };
        }

        public static MessageModel NotFound(int? msgId, string msg, object res)
        {
            return new MessageModel()
            {
                StatusCode = 404,
                IsSuccessed = false,
                MessageId = msgId,
                Message = msg,
                Response = res
            };
        }

        public static MessageModel File()
        {
            return null;
        }

        public void AddErrors(List<string> errors)
        {
            if (Errors == null)
            {
                Errors = errors;
            }
            else
            {
                errors.AddRange(errors);
            }
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCode ?? 200;
            context.HttpContext.Response.ContentType = "application/json";
            JsonSerializerSettings setting = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            var jsonStr = JsonConvert.SerializeObject(this, setting);
            var bytes = Encoding.UTF8.GetBytes(jsonStr);
            context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            return base.ExecuteResultAsync(context);
        }
    }
}
