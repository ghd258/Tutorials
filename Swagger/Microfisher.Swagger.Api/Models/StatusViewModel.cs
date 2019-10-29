using System;
namespace Microfisher.Swagger.Api.Models
{
    public class StatusViewModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}
