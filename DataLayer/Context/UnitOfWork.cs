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
        private MyCmsRepository<News_Groups> _newsGroupsRepository;
        public MyCmsRepository<News_Groups> NewsGroupsRepository
        {
            get
            {
                if (_newsGroupsRepository == null)
                {
                    _newsGroupsRepository = new MyCmsRepository<News_Groups>(db);
                }
                return _newsGroupsRepository;
            }
        }

        private MyCmsRepository<News> _newsRepository;
        public MyCmsRepository<News> NewsRepository
        {
            get
            {
                if (_newsRepository == null)
                {
                    _newsRepository = new MyCmsRepository<News>(db);
                }
                return _newsRepository;
            }
        }
        private MyCmsRepository<News_Selected_Groups> _newsSelectedGroupsRepository;
        public MyCmsRepository<News_Selected_Groups> NewsSelectedGroupsRepository
        {
            get
            {
                if (_newsSelectedGroupsRepository == null)
                {
                    _newsSelectedGroupsRepository = new MyCmsRepository<News_Selected_Groups>(db);
                }
                return _newsSelectedGroupsRepository;
            }
        }
        private MyCmsRepository<Tags> _tagsRepository;
        public MyCmsRepository<Tags> TagsRepository
        {
            get
            {
                if (_tagsRepository == null)
                {
                    _tagsRepository = new MyCmsRepository<Tags>(db);
                }
                return _tagsRepository;
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
