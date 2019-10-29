using System;
namespace Microfisher.Swagger.Api.Models
{
    public class CreateViewModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public long Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public long Password { get; set; }
    }
}
