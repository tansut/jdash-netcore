using JDash.NetCore.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDash.NetCore.Api.Core
{
    internal class Configuration
    {
        private static object _configuratorLocker = new object();
        private static Type configurator;

        internal static Type ConfiguratorType
        {
            get
            {
                return configurator;
            }

            set
            {
                lock (_configuratorLocker)
                {
                    configurator = value;
                }
            }
        }
         
    }
}
