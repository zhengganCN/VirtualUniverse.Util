using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Util.Data.EFCore.Interface
{
    interface IUOW
    {
        public void Commit();
        public void Rollback();
        public void Transaction();
    }
}
