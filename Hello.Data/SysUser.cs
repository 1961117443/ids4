using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public partial class SysUser
    {
        public virtual IList<SysRole> Roles { get; set; }
    }
}
