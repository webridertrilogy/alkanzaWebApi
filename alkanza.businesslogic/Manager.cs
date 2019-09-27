using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alkanza.businessobjects;
using alkanza.dataaccess;

namespace alkanza.businesslogic
{
    public class Manager
    {
        private static HistoricoDA objHistoricoDA = new HistoricoDA();
        private static MarcadorDA objMarcadorDA = new MarcadorDA();

        /// <summary>
        /// Funcionalidad para crear un Historico
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la creacion</param>
        /// <returns>True si pudo crear el historico, false en caso contrario</returns>
        public static bool crearHistorico(Historico historico)
        {
            bool exitoso = false;

            try
            {
                exitoso = objHistoricoDA.crearHistorico(historico);
                if (exitoso)
                {
                    foreach( Marcador marcador in historico.listMarcadores  )
                    {
                       exitoso = crearMarcador(marcador, historico);
                        if (!exitoso)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exitoso;
        }

        /// <summary>
        /// Funcionalidad para consultar un historico
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la consulta</param>
        /// <returns>Historico almacenado</returns>
        public static Historico consultarHistorico(Historico historico)
        {
            try
            {
                Historico historicoResult = new Historico();
                List<Marcador> listadoMarcadores = new List<Marcador>();
                historicoResult = objHistoricoDA.consultarHistorico(historico);
                listadoMarcadores = objHistoricoDA.consultarMarcadoresHistorico(historico);
                historicoResult.listMarcadores = listadoMarcadores;
                return historicoResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcionalidad para consultar datos historicos
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la consulta</param>
        /// <returns>Lista de Historicos</returns>
        public static List<Historico> consultarHistoricos()
        {
            try
            {
                return objHistoricoDA.consultarHistoricos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcionalidad para consultar los marcadores de un historico
        /// </summary>
        /// <param name="historico">Objeto historico con los atributos necesarios para realizar la consulta</param>
        /// <returns>Listado de todos los marcadores que tiene asociado un historico</returns>
        public static List<Marcador> consultarMarcadoresHistorico(Historico historico)
        {
            try
            {
                return objHistoricoDA.consultarMarcadoresHistorico(historico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcionalidad para crear un marcador
        /// </summary>
        /// <param name="marcador">Objeto marcador con los atributos necesarios para realizar la creacion</param>
        /// <returns>True si pudo crear el marcador, false en caso contrario</returns>
        public static bool crearMarcador(Marcador marcador, Historico historico)
        {
            try
            {
                return objMarcadorDA.crearMarcador(marcador, historico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
