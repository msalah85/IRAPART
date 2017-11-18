
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using Share.CMS.DataUtility;
using Share.CMS.DataAccess;

namespace Share.CMS.Business
{
    public class Save
    {
        // Insert/Edit using single array.
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int SaveRow(string procedureName, List<string[]> procedureParamters)
        {
            var parameters = new SqlParameter[procedureParamters.Count];

            // build stored procedure paramters.
            for (int i = 0; i < procedureParamters.Count; i++)
            {
                parameters[i] = new SqlParameter("@" + procedureParamters[i][0], procedureParamters[i][1]);
            }

            // start save to db.
            int RowsAffected = 0;
            int Result = new DbObject().RunProcedure(procedureName, parameters, out RowsAffected);
            return Result;
        }

        // Insert/Edit using 2 arrays.
        [DataObjectMethod(DataObjectMethodType.Update)]
        public int SaveRow(string procedureName, string[] paramters, string[] paramtersVal)
        {
            var parameters = new SqlParameter[paramtersVal.Length];

            // build stored procedure paramters.
            for (int i = 0; i < paramtersVal.Length; i++)
            {
                if (string.IsNullOrEmpty(paramtersVal[i]))
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], DBNull.Value);
                }
                else if (paramters[i].ToLower().Contains("password"))
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], EncryptDecryptString.Encrypt(paramtersVal[i], "Taj$$Key"));
                }
                else if (paramtersVal[i].Length >= 50)
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], System.Data.SqlDbType.NVarChar, paramtersVal[i].Length * 2);
                    parameters[i].Value = paramtersVal[i];
                }
                else
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], paramtersVal[i]);
                }
            }

            // start save to db.
            int RowsAffected = 0;
            int Result = new DbObject().RunProcedure(procedureName, parameters, out RowsAffected);
            return Result;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public object RunSQLString(string sqlString)
        {
            // run sql string in to db.
            var Result = new DbObject().RunQuery(sqlString, "Table1");
            return Result;
        }
    }
}