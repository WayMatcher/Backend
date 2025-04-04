﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WayMatcherBL.Models;

namespace WayMatcherBL.Models
{
    public partial class WayMatcherContext
    {
        private IWayMatcherContextProcedures _procedures;

        public virtual IWayMatcherContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new WayMatcherContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IWayMatcherContextProcedures GetProcedures()
        {
            return Procedures;
        }
    }

    public partial class WayMatcherContextProcedures : IWayMatcherContextProcedures
    {
        private readonly WayMatcherContext _context;

        public WayMatcherContextProcedures(WayMatcherContext context)
        {
            _context = context;
        }

        public virtual async Task<int> LogAuditEntryAsync(string message, string entity_Type, int? entity_ID, int? user_ID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "Message",
                    Size = -1,
                    Value = message ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Entity_Type",
                    Size = 510,
                    Value = entity_Type ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Entity_ID",
                    Value = entity_ID ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "User_ID",
                    Value = user_ID ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [WayMatcher].[LogAuditEntry] @Message = @Message, @Entity_Type = @Entity_Type, @Entity_ID = @Entity_ID, @User_ID = @User_ID", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
