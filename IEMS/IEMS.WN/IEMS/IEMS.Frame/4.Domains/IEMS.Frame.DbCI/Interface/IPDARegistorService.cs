using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MSTL.DbAccess;
using IEMS.Frame.Entity;
namespace IEMS.Frame.DbCI
{
    public interface IPDARegistorService:IDbCIService
    {
        DataTable GetPDARegSeq();
    }
}
