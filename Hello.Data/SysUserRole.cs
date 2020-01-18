using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public partial class SysUserRole
    {
        public virtual SysUser SysUser { get; set; }

        public virtual SysRole SysRole { get; set; }

    }
}
