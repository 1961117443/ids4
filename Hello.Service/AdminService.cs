using Data;
using Hello.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Hello.Service
{
    public class AdminService : IAdminService
    {
        private readonly IFreeSql freeSql;

        
        public AdminService(IFreeSql freeSql)
        {
            this.freeSql = freeSql;
        }
        public async Task<SysUser> GetUser(string userCode, string passWord)
        { 
            var user = await freeSql.Select<SysUser>().Where(w => w.Code == userCode && w.Password == passWord).FirstAsync();
            return user;
        }

        public async Task<SysUser> GetUserWithRoles(Expression<Func<SysUser, bool>> where)
        {
            var user = await freeSql.Select<SysUser>()
                .Where(where)
                .FirstAsync();

            if (user!=null)
            {
                user =await GetUserRoles(user);
            }
            
             
            return user;

        }

        public async Task<SysUser> GetUserRoles(SysUser user)
        {
            if (user != null)
            {
                user.Roles = await freeSql.Select<SysUserRole>()
                    .Where(w => w.SysUserID == user.ID).Include(a => a.SysRole)
                    .Select(a => a.SysRole)
                    .ToListAsync();
            }
            return user;
        }
    }
}
