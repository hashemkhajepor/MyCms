using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        MyCms_DBEntities db = new MyCms_DBEntities();
        private MyCmsRepository<Users> _usersRepository;
        public MyCmsRepository<Users> UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new MyCmsRepository<Users>(db);
                }
                return _usersRepository;
            }
        }
        private MyCmsRepository<Roles> _rolesRepository;
        public MyCmsRepository<Roles> RolesRepository
        {
            get
            {
                if (_rolesRepository == null)
                {
                    _rolesRepository = new MyCmsRepository<Roles>(db);
                }
                return _rolesRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        public void config()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
    }
}
