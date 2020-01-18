using Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hello.IService
{
    public interface IAdminService
    {
        /// <summary>
        /// 根据登录账号+密码获取用户
        /// </summary>
        /// <param name="userName">登录账号</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        Task<SysUser> GetUser(string userName, string passWord);

        Task<SysUser> GetUserWithRoles(Expression<Func<SysUser, bool>> where);
        Task<SysUser> GetUserRoles(SysUser user);
    }
}
