using JDash.NetCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace JDash.NetCore.Api
{
    public abstract class BaseJDashConfigurator
    {
        protected readonly HttpContext HttpContext;
        private bool ensureTablesCreated;
        private static bool _dbCreated = false;

        public BaseJDashConfigurator(HttpContext context)
        {
            this.HttpContext = context;
            this.EnsureTablesCreated = true;
            this.AuthorizationHeader = context.Request.Headers["Authorization"];
        }

        public string AuthorizationHeader { get; private set; }

        protected bool EnsureTablesCreated
        {
            get { return ensureTablesCreated; }
            set { ensureTablesCreated = value; }
        }

        internal JDashPrincipal GetDecryptedPrincipal()
        {
            return this.GetPrincipal(this.AuthorizationHeader);
        }

        /// <summary>
        /// Gets the jdash principal from Authorization Header .
        /// </summary>
        /// <param name="authorizationHeader">The authorization header of http request, this header will be encrypted as JWT that you have encrypted before.</param>
        /// <returns>UnEncrypted User and Application Information</returns>
        public abstract JDashPrincipal GetPrincipal(string authorizationHeader);

        internal IJDashProvider _GetProvider()
        {
            var provider = GetProvider();
            if (EnsureTablesCreated && !_dbCreated)
            {
                provider.EnsureTablesCreated();
                _dbCreated = true;
            }
            return provider;
        }
        public abstract IJDashProvider GetProvider();
    }
}
