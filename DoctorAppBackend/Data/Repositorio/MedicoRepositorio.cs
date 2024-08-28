using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class MedicoRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MedicoRepositorio(ApplicationDbContext db):base(db) 
        {
            _db = db;

        }

        public void Actualizar(Medico medico)
        {
            var medicodb = _db.medicos.FirstOrDefault(e => e.Id == medico.Id);
            if (medicodb != null)
            {
                medicodb.Apellidos = medico.Apellidos;
                medicodb.Nombres = medico.Nombres;
                medicodb.Estado = medico.Estado;
                medicodb.FechaActualizacion= DateTime.Now;
                medicodb.Genero = medico.Genero;
                medicodb.Especialidad = medico.Especialidad;
                medicodb.Direccion = medico.Direccion;  

                _db.SaveChanges();

            }
        }
    }
}
