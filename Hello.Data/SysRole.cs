using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public partial  class SysRole
    {
        public virtual IList<SysUser> Users { get; set; }
    }
}
