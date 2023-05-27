using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Result : INullable
    {

        /// <summary>
        /// Message
        /// </summary>
        public string? Status { get; }
        /// <summary>
        /// Status (Error, Success)
        /// </summary>
        public string? StatusResult { get; }

        public bool IsNull => string.IsNullOrEmpty(Status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">Message</param>
        /// <param name="statusResult">Status (Error, Success)</param>
        public Result(string status, string statusResult)
        {
            Status = status;
            StatusResult = statusResult;
        }
        
        public Result(DBTelegraph.Result result)
        {
            Status = result.Status;
            StatusResult = result.StatusResult;
        }

        public static implicit operator Result(DBTelegraph.Result result)
        {
            return new Result(result.Status, result.StatusResult);
        }

        public static implicit operator DBTelegraph.Result(Result result)
        {
            return new DBTelegraph.Result(result.Status, result.StatusResult);
        }

    }
}
