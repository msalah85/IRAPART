using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Share.CMS.DataAccess;

namespace Share.CMS.Business
{
    [DataObject(true)]
    public class Select
    {
        // Select multiple lists with a dynamic filters.
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public DataSet SelectLists(string procedureName, string[,] procedureParamters)
        {
            var parameters = new SqlParameter[procedureParamters.GetLength(0)];

            // build stored procedure paramters.
            for (int i = 0; i < procedureParamters.GetLength(0); i++)
            {
                parameters[i] = new SqlParameter("@" + procedureParamters[i, 0], procedureParamters[i, 1]);
            }

            // get dataset from db.
            var ds = new DbObject().RunProcedure(procedureName, parameters, "list");
            return ds;
        }

        // Select multiple lists with a dynamic filters.
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public DataSet SelectLists(string procedureName, string[] paramters, string[] paramtersVal)
        {
            var parameters = new SqlParameter[paramtersVal.Length];

            // build stored procedure paramters.
            for (int i = 0; i < paramtersVal.Length; i++)
            {
                if (paramtersVal[i] == null || paramtersVal[i].Equals(""))
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], DBNull.Value);
                }
                else
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], paramtersVal[i]);
                }
            }

            // get dataset from db.
            var ds = new DbObject().RunProcedure(procedureName, parameters, "list");
            return ds;
        }
        
        // Select multiple lists without paramters.
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public DataSet SelectLists(string procedureName)
        {
            var parameters = new SqlParameter[] { };

            // get dataset from db.
            var ds = new DbObject().RunProcedure(procedureName, parameters, "list");
            return ds;
        }
        
        // Select multiple lists with a dynamic filters.
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public DataSet SelectPagedList(string procedureName, string[,] procedureParamters)
        {
            int prmCount = procedureParamters.GetLength(0);
            var parameters = new SqlParameter[prmCount + 1];

            // build stored procedure paramters.
            for (int i = 0; i < prmCount; i++)
            {
                parameters[i] = new SqlParameter("@" + procedureParamters[i, 0], procedureParamters[i, 1]);
            }
            // add output paramter
            var outParm = new SqlParameter("@PageCount", SqlDbType.Int);
            outParm.Direction = ParameterDirection.Output;
            parameters[prmCount] = outParm;

            // get dataset from db.
            var ds = new DbObject().RunProcedureOutParam(procedureName, parameters, "list");
            return ds;
        }

        // Select multiple lists with a dynamic filters.
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public DataSet SelectPagedList(string procedureName, string[] paramters, string[] paramtersVal)
        {
            var parameters = new SqlParameter[paramtersVal.Length + 1];

            // build stored procedure paramters.
            for (int i = 0; i < paramtersVal.Length; i++)
            {
                if (paramtersVal[i] == null || paramtersVal[i].Equals(""))
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], DBNull.Value);
                }
                else
                {
                    parameters[i] = new SqlParameter("@" + paramters[i], paramtersVal[i]);
                }
            }

            // add output paramter
            var outParm = new SqlParameter("@PageCount", SqlDbType.Int);
            outParm.Direction = ParameterDirection.Output;
            parameters[paramtersVal.Length] = outParm;

            // get dataset from db.
            var ds = new DbObject().RunProcedureOutParam(procedureName, parameters, "list");
            return ds;
        }

        // Select multiple lists with a dynamic filters.
        [DataObjectMethod(DataObjectMethodType.Fill)]
        public DataSet SelectPagedLists(string procedureName, string[,] procedureParamters)
        {
            int prmCount = procedureParamters.GetLength(0);
            var parameters = new SqlParameter[prmCount + 1];

            // build stored procedure paramters.
            for (int i = 0; i < prmCount; i++)
            {
                parameters[i] = new SqlParameter("@" + procedureParamters[i, 0], procedureParamters[i, 1]);
            }
            parameters[prmCount] = new SqlParameter("@PageCount", SqlDbType.Int, 4, ParameterDirection.Output, false, 1, 1, "PageCount", DataRowVersion.Default, 1);

            // get dataset from db.
            var ds = new DbObject().RunProcedureOutParam(procedureName, parameters, "list");
            return ds;
        }
    }
}