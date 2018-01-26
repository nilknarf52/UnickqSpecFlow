using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowApresentacao.Helpers
{

    public class SQLRepository<T> where T : class// : ISQLRepository<T> where T : class
    {
        private readonly string _tableName;
        private readonly string _connectionString;

        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="tableName">tabela ou procedure</param>
        /// <param name="connectionString">connection string (por default é DB</param>
        public SQLRepository(string tableName, string connectionString = "")
        {
            _tableName = tableName;
            _connectionString = (string.IsNullOrEmpty(connectionString) ? ConfigurationManager.ConnectionStrings["BD"].ConnectionString : ConfigurationManager.ConnectionStrings[connectionString].ConnectionString);
        }


        public virtual IEnumerable<T> Exec(Dictionary<string, object> parameters)
        {
            try
            {
                IEnumerable<T> item = null;
                List<string> lstQuery = new List<string>();
                string strQuery = "";

                if (parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        if (parameter.Value.GetType() == typeof(bool) || parameter.Value.GetType() == typeof(int))
                            lstQuery.Add(string.Format("@{0}={1}", parameter.Key, parameter.Value));
                        else
                            lstQuery.Add(string.Format("@{0}='{1}'", parameter.Key, parameter.Value));
                    }

                    strQuery = string.Join(",", lstQuery);
                }


                using (IDbConnection cn = Connection)
                {
                    cn.Open();
                    item = cn.Query<T>(string.Format("{0} {1}", _tableName, strQuery));
                }

                return item;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
    }
}

