using Mundial.Entidades;
using System.Collections.Generic;

namespace Mundial.Web.Services
{
    public interface IRespuestaSimpleService
    {

        Task<bool> Insert(RespuestaSimple respuestaNueva);

        Task<RespuestaSimple> GetById(int idPregunta, int idUsuario);

        Task<List<RespuestaSimple>> GetAllByIdUser(int idUsuario);

        Task<IEnumerable<RespuestaSimple>> GetAll();

        Task<bool> DeleteAllByUser(int idUser, bool exception);
        
        Task<int?> InsertMasivo(List<RespuestaSimple> listaRespuestas);

        Task<List<RespuestaSimple>> GetAllByOrdenAndIdUser(int idUser, int orden);


    }
}
