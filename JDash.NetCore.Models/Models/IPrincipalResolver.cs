using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Models
{
   
    public interface IPrincipalResolver
    {
        void SetPrincipal(JDashPrincipalResult principal);
        JDashPrincipalResult GetPrincipal();
    }
}
