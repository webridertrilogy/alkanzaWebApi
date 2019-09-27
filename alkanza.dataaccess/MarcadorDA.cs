using alkanza.businessobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using alkanza.dataaccess;

namespace alkanza.dataaccess
{
    public class MarcadorDA
    {
        /// <summary>
        /// Funcionalidad para crear un marcador
        /// </summary>
        /// <param name="marcador">Objeto marcador con los atributos necesarios para realizar la creacion</param>
        /// <returns>True si pudo crear el marcador, false en caso contrario</returns>
        public bool crearMarcador(Marcador marcador, Historico historico)
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
                        sqlQuery = "INSERT INTO Marcador(id, nombre, descripcion, latitud, longitud, distancia, esCentroMedico, id_historico) VALUES(@id, @nombre, @descripcion, @latitud, @longitud, @distancia, @esCentroMedico, @id_historico)";

                        using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlconnection, sqlTran))
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = marcador.id;
                            sqlCmd.Parameters.Add("@nombre", SqlDbType.VarChar, 300).Value = marcador.nombre;
                            sqlCmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 500).Value = marcador.descripcion;
                            sqlCmd.Parameters.Add("@latitud", SqlDbType.Decimal).Value = marcador.latitud;
                            sqlCmd.Parameters["@latitud"].Precision = 9;
                            sqlCmd.Parameters["@latitud"].Scale = 6;
                            sqlCmd.Parameters.Add("@longitud", SqlDbType.Decimal).Value = marcador.longitud;
                            sqlCmd.Parameters["@longitud"].Precision = 9;
                            sqlCmd.Parameters["@longitud"].Scale = 6;
                            sqlCmd.Parameters.Add("@distancia", SqlDbType.Int).Value = marcador.distancia;
                            sqlCmd.Parameters.Add("@esCentroMedico", SqlDbType.TinyInt).Value = marcador.esCentroMedico;
                            sqlCmd.Parameters.Add("@id_historico", SqlDbType.UniqueIdentifier).Value = historico.id;
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
    }
}
