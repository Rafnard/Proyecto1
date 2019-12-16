using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenNomina.Models
{
    public class RolesRepository
    {
        private List<Roles> roles; // creo una lista
        public RolesRepository()
        {
            roles = new List<Roles> //lleno la lista de roles
            {
                new Roles{RolId= "A",Rol="Administrador"},
                new Roles{RolId= "U",Rol="Usuario"}
            };
        }
        public List<Roles> RetrieveAll() // metodo para regresar toda la lista
        {
            return roles;
        }
        public Roles RetrieveByID(string id) //regresar lista por id 
        {
            return roles.Find(p => p.RolId == id);
        }
    }
}