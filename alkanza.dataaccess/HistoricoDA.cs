using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alkanza.dataaccess;
using System.Data.SqlClient;
using System.Data;
using alkanza.businessobjects;


namespace alkanza.dataaccess
{
    public class HistoricoDA
    {



        /// <summary>
        /// Funcionalidad para crear un historico
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la creacion</param>
        /// <returns>True si pudo crear el historico, false en caso contrario</returns>
        public bool crearHistorico(Historico historico)
        {
            bool bExito;
            string sqlQuery;

            try
            {
                bExito = false;
                sqlQuery = string.Empty;
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionDB.CadenaConexion()))
                {
                    sqlconnection.Open();
                    using (SqlTransaction sqlTran = sqlconnection.BeginTransaction())
                    {
                        sqlQuery = "INSERT INTO Historico(id, radio, latitud, longitud, resultado, fecha) VALUES(@id, @radio, @latitud, @longitud, @resultado, @fecha)";

                        using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlconnection, sqlTran))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = historico.id;
                            sqlCmd.Parameters.Add("@radio", SqlDbType.Int).Value = historico.radio;
                            sqlCmd.Parameters.Add("@latitud", SqlDbType.Decimal).Value = historico.latitud;
                            sqlCmd.Parameters["@latitud"].Precision = 9;
                            sqlCmd.Parameters["@latitud"].Scale = 6;
                            sqlCmd.Parameters.Add("@longitud", SqlDbType.Decimal).Value = historico.longitud;
                            sqlCmd.Parameters["@longitud"].Precision = 9;
                            sqlCmd.Parameters["@longitud"].Scale = 6;
                            sqlCmd.Parameters.Add("@resultado", SqlDbType.Decimal).Value = historico.resultado;
                            sqlCmd.Parameters["@resultado"].Precision = 18;
                            sqlCmd.Parameters["@resultado"].Scale = 0;
                            sqlCmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                            sqlCmd.Prepare();
                            sqlCmd.ExecuteNonQuery();
                            sqlTran.Commit();
                            bExito = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bExito = false;
            }

            return bExito;
        }

        /// <summary>
        /// Funcionalidad para consultar datos historicos
        /// </summary>
        /// <returns>Historicos almacenados</returns>
        public List<Historico> consultarHistoricos()
        {
            string sqlQuery;
            Historico historico;
            List<Historico> listHistoricos = new List<Historico>();

            try
            {
                sqlQuery = string.Empty;
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionDB.CadenaConexion()))
                {
                    sqlconnection.Open();
                    sqlQuery = "SELECT TOP 40 historico.id, historico.radio, historico.latitud, historico.longitud, historico.resultado, historico.fecha ";
                    sqlQuery += "FROM historico historico WITH(NOLOCK) ";
                    sqlQuery += "ORDER BY historico.fecha DESC ";

                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlconnection))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Prepare();

                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                historico = new Historico();
                                if (sqlReader.GetOrdinal("id") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("id")))
                                    {
                                        historico.id = new Guid(Convert.ToString(sqlReader.GetValue(sqlReader.GetOrdinal("id"))));
                                    }
                                }
                                if (sqlReader.GetOrdinal("radio") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("radio")))
                                    {
                                        historico.radio = Convert.ToInt32(sqlReader.GetValue(sqlReader.GetOrdinal("radio")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("latitud") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("latitud")))
                                    {
                                        historico.latitud = Convert.ToDecimal(sqlReader.GetValue(sqlReader.GetOrdinal("latitud")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("longitud") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("longitud")))
                                    {
                                        historico.longitud = Convert.ToDecimal(sqlReader.GetValue(sqlReader.GetOrdinal("longitud")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("resultado") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("resultado")))
                                    {
                                        historico.resultado = Convert.ToInt32(sqlReader.GetValue(sqlReader.GetOrdinal("resultado")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("fecha") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("fecha")))
                                    {
                                        historico.fecha = Convert.ToDateTime(sqlReader.GetValue(sqlReader.GetOrdinal("fecha")));
                                    }
                                }
                                listHistoricos.Add(historico);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listHistoricos = new List<Historico>();
            }

            return listHistoricos;
        }

        /// <summary>
        /// Funcionalidad para consultar un historico
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la consulta</param>
        /// <returns>Historico almacenado</returns>
        public Historico consultarHistorico(Historico historico)
        {
            string sqlQuery;
            Historico historicoNew = new Historico();

            try
            {
                sqlQuery = string.Empty;
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionDB.CadenaConexion()))
                {
                    sqlconnection.Open();
                    sqlQuery = "SELECT historico.id, historico.radio, historico.latitud, historico.longitud, historico.resultado, historico.fecha ";
                    sqlQuery += "FROM historico historico WITH(NOLOCK) ";
                    sqlQuery += "WHERE historico.id = @idHistorico ";

                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlconnection))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add("@idHistorico", SqlDbType.UniqueIdentifier).Value = historico.id;
                        sqlCmd.Prepare();

                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                historicoNew = new Historico();
                                if (sqlReader.GetOrdinal("id") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("id")))
                                    {
                                        historicoNew.id = new Guid(Convert.ToString(sqlReader.GetValue(sqlReader.GetOrdinal("id"))));
                                    }
                                }
                                if (sqlReader.GetOrdinal("radio") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("radio")))
                                    {
                                        historicoNew.radio = Convert.ToInt32(sqlReader.GetValue(sqlReader.GetOrdinal("radio")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("latitud") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("latitud")))
                                    {
                                        historicoNew.latitud = Convert.ToDecimal(sqlReader.GetValue(sqlReader.GetOrdinal("latitud")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("longitud") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("longitud")))
                                    {
                                        historicoNew.longitud = Convert.ToDecimal(sqlReader.GetValue(sqlReader.GetOrdinal("longitud")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("resultado") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("resultado")))
                                    {
                                        historicoNew.resultado = Convert.ToInt32(sqlReader.GetValue(sqlReader.GetOrdinal("resultado")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("fecha") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("fecha")))
                                    {
                                        historicoNew.fecha = Convert.ToDateTime(sqlReader.GetValue(sqlReader.GetOrdinal("fecha")));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                historicoNew = new Historico();
            }

            return historicoNew;
        }

        /// <summary>
        /// Funcionalidad para consultar los marcadores de un historico
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la consulta</param>
        /// <returns>Listado de historicos</returns>
        public List<Marcador> consultarMarcadoresHistorico(Historico historico)
        {
            string sqlQuery;
            Marcador marcador;
            List<Marcador> listMarcadores;

            try
            {
                sqlQuery = string.Empty;
                listMarcadores = new List<Marcador>();
                using (SqlConnection sqlconnection = new SqlConnection(ConnectionDB.CadenaConexion()))
                {
                    sqlconnection.Open();
                    sqlQuery = "SELECT marcador.id, marcador.nombre, marcador.descripcion, marcador.latitud, marcador.longitud, marcador.distancia, marcador.esCentroMedico ";
                    sqlQuery += "FROM historico historico WITH(NOLOCK) ";
                    sqlQuery += "INNER JOIN marcador marcador WITH(NOLOCK) ON historico.id = marcador.id_historico ";
                    sqlQuery += "WHERE historico.id = @idHistorico ";

                    using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlconnection))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.Add("@idHistorico", SqlDbType.UniqueIdentifier).Value = historico.id;
                        sqlCmd.Prepare();

                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                marcador = new Marcador();
                                if (sqlReader.GetOrdinal("id") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("id")))
                                    {
                                        marcador.id = new Guid(Convert.ToString(sqlReader.GetValue(sqlReader.GetOrdinal("id"))));
                                    }
                                }
                                if (sqlReader.GetOrdinal("nombre") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("nombre")))
                                    {
                                        marcador.nombre = Convert.ToString(sqlReader.GetValue(sqlReader.GetOrdinal("nombre")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("descripcion") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("descripcion")))
                                    {
                                        marcador.descripcion = Convert.ToString(sqlReader.GetValue(sqlReader.GetOrdinal("descripcion")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("latitud") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("latitud")))
                                    {
                                        marcador.latitud = Convert.ToDecimal(sqlReader.GetValue(sqlReader.GetOrdinal("latitud")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("longitud") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("longitud")))
                                    {
                                        marcador.longitud = Convert.ToDecimal(sqlReader.GetValue(sqlReader.GetOrdinal("longitud")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("distancia") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("distancia")))
                                    {
                                        marcador.distancia = Convert.ToInt32(sqlReader.GetValue(sqlReader.GetOrdinal("distancia")));
                                    }
                                }
                                if (sqlReader.GetOrdinal("esCentroMedico") >= 0)
                                {
                                    if (!sqlReader.IsDBNull(sqlReader.GetOrdinal("esCentroMedico")))
                                    {
                                        marcador.esCentroMedico = Convert.ToBoolean(sqlReader.GetValue(sqlReader.GetOrdinal("esCentroMedico")));
                                    }
                                }
                                listMarcadores.Add(marcador);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listMarcadores = new List<Marcador>();
            }

            return listMarcadores;
        }
    }
}