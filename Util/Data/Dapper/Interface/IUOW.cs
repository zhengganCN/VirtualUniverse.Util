using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Data.Dapper.Interface
{
    interface IUOW
    {
        public void Commit();
        public void Rollback();
        public void Transaction();
    }
}
