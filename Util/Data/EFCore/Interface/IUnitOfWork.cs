using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Util.Data.UOW
{
    interface IUnitOfWork
    {
        public void Commit();
        public void Rollback();
        public void Transaction();
    }
}
