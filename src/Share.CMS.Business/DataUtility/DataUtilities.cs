using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;

namespace IraqCars.Business.DataUtility
{
    public static class DataUtilities
    {
        /// <summary>
        /// Convert DataTable to generic list.
        /// </summary>
        /// <param name="DataTable">dataTable</param>
        /// <returns>List</returns>
        public static List<Dictionary<string, object>> ConvertDTToList(DataTable dataTable)
        {
            var rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }

        public static string ConvertDTToJson(DataTable table)
        {
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }

        /// <summary>
        /// Convert DataSet to generic list.
        /// </summary>
        /// <param name="DataTable">dataTable</param>
        /// <returns>List</returns>
        public static List<Dictionary<string, object>> ConvertDSToList(DataSet dataSet)
        {
            var rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                var dt = dataSet.Tables[i];
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }
            return rows;
        }

        /// <summary>
        /// Convert DataSet to generic list.
        /// </summary>
        /// <param name="DataTable">dataTable</param>
        /// <returns>List</returns>
        public static string[] ConvertDSToArray(DataSet dataSet)
        {
            string row = "";
            string[] resultes = new string[dataSet.Tables.Count];

            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                var dt = dataSet.Tables[i];
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    row += dt.Rows[j][0].ToString() + ":";
                    row += dt.Rows[j][1].ToString();
                    row += ",";
                }
                if (row.Contains(","))
                {
                    row = row.Remove(row.LastIndexOf(','));
                }
                resultes[i] = row;
                row = "";
            }
            return resultes;
        }

        /// <summary>
        /// Conerts source to 2D array.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item that must exist in the source.
        /// </typeparam>
        /// <param name="source">
        /// The source to convert.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if source is null.
        /// </exception>
        /// <returns>
        /// The 2D array of source items.
        /// </returns>
        public static T[,] To2DArray<T>(this IList<IList<T>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            int max = source.Select(l => l).Max(l => l.Count());

            var result = new T[source.Count, max];

            for (int i = 0; i < source.Count; i++)
            {
                for (int j = 0; j < source[i].Count(); j++)
                {
                    result[i, j] = source[i][j];
                }
            }

            return result;
        }

        ///// <summary>
        ///// Convert DataTable to generic list.
        ///// </summary>
        ///// <param name="DataTable">dataTable</param>
        ///// <returns>List</returns>
        //public static List<Dictionary<string, object>> ConvertDTToList(DataTable dataTable)
        //{
        //    var rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    foreach (DataRow dr in dataTable.Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in dataTable.Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rows.Add(row);
        //    }
        //    return rows;
        //}
    }
}