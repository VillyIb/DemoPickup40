using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace AppCode.ExceptionExtentions
{
    public class ExceptionExtention
    {
        private StringBuilder Logger { get; set; }

        private void Log(DbValidationError value)
        {
            if (value != null)
            {
                Logger.AppendFormat("    {0},  Property: {1}", value.ErrorMessage, value.PropertyName);
            }
        }


        private void Log(DbEntityValidationResult value)
        {
            if (value != null)
            {
                foreach (var current in value.ValidationErrors)
                {
                    Log(current);
                }
            }
        }

        private void LogAny(Exception ex)
        {
            Logger.AppendFormat("{0}", ex.StackTrace);
        }


        // -- specific exceptions.

        private void Log(DbEntityValidationException ex)
        {
            foreach (var current in ex.EntityValidationErrors)
            {
                Log(current);
            }
            LogAny(ex);
        }

        private void Log(SqlError error)
        {
            Logger.AppendFormat(
                "  - SqlError: {0}, {1}, {2}, {3}, {4}, {5}, {6}, "
                , error.Class
                , error.LineNumber
                , error.Message
                , error.Procedure
                , error.Server
                , error.Source
                , error.State
               );
        }


        /// <summary>
        /// Log the specified SqlException.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        private void Log(SqlException ex)
        {
            var errors = new StringBuilder();

            foreach (SqlError error in ex.Errors)
            {
                Log(error);
            }

            try
            {
                Logger.AppendFormat(
                    "{0}Class: {1}{0}Message: {2}{0}LindeNo: {3}{0}Number: {4}{0}Procedure: {5}{0}Server: {6}{0}Source: {7}{0}State {8}{0} "
                    , Environment.NewLine + "    "
                    , ex.Class
                    , ex.Message
                    , ex.LineNumber
                    , ex.Number
                    , ex.Procedure
                    , ex.Server
                    , ex.Source
                    , ex.State
                    );
            }
            catch (Exception exx)
            {
                Logger.AppendFormat("{0}", exx.Message);
            }

            LogAny(ex);
        }


        private void Log(DbUpdateException ex)
        {
            Logger.AppendFormat("\r\n\n    DbEntityEntry: {0}", ex);
            foreach (DbEntityEntry ee in ex.Entries)
            {
                Logger.AppendFormat("\r\n        Entry: {0}", ee);
            }
        }


        private void Log(UpdateException ex)
        {
            Logger.AppendFormat("\r\n\n    DbEntityEntry: {0}", ex);
            foreach (ObjectStateEntry ee in ex.StateEntries)
            {
                Logger.AppendFormat("\r\n        Entry: {0}", ee);
            }
        }


        private void LogDispatch(Exception ex)
        {
            var exceptionType = ex.GetType().ToString();

            var t1 = ex as DbEntityValidationException;
            var t2 = ex as SqlException;
            var t3 = ex as DbUpdateException;
            var t4 = ex as UpdateException;
            //var t3 = ex as SoapException;
            //var t4 = ex as SmtpException;

            if (t1 != null)
            {
                Log(t1);
            }
            else if (t2 != null)
            {
                Log(t2);
            }
            else if (t3 != null)
            {
                Log(t3);
            }
            else if (t4 != null)
            {
                Log(t4);
            }
            else
            {
                LogAny(ex);
            }

            var tx = exceptionType;
        }

        public void LogException(Exception ex)
        {
            if (ex != null)
            {
                LogDispatch(ex);
                LogException(ex.InnerException); // recursive call
            }
        }

        public ExceptionExtention()
        {
            Logger = new StringBuilder();
        }


        public static string Analyze(Exception ex)
        {
            var current = new ExceptionExtention();
            current.LogException(ex); 

            return current.Logger.ToString();

        }

    }
}
