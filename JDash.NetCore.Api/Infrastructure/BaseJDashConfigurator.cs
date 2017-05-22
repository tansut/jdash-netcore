using JDash.NetCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace JDash.NetCore.Api.Infrastructure
{
    public abstract class BaseJDashConfigurator
    {
        protected readonly HttpContext HttpContext;
        protected bool _ensureTablesCreated;
        private static bool _dbCreated = false;

        public BaseJDashConfigurator(HttpContext context, bool ensureTablesCreated)
        {
            this.HttpContext = context;
            this._ensureTablesCreated = ensureTablesCreated;
            this.AuthorizationHeader = context.Request.Headers["Authorization"];
        }

        public string AuthorizationHeader { get; private set; }

        internal JDashPrincipalResult GetDecryptedPrincipal()
        {
            return this.GetJDashPrincipal(this.AuthorizationHeader);
        }

        /// <summary>
        /// Gets the jdash principal from Authorization Header .
        /// </summary>
        /// <param name="authorizationHeader">The authorization header of http request, this header will be encrypted as JWT that you have encrypted before.</param>
        /// <returns>UnEncrypted User and Application Information</returns>
        public abstract JDashPrincipalResult GetJDashPrincipal(string authorizationHeader);

        internal IJDashPersistenceProvider GetProvider()
        {
            var provider = GetPersistanceProvider();
            if (_ensureTablesCreated && !_dbCreated)
            {
                provider.EnsureTablesCreated();
                _dbCreated = true;
            }
            return provider;
        }
        public abstract IJDashPersistenceProvider GetPersistanceProvider();
    }
}
