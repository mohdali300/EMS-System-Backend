using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using EMS_SYSTEM.APPLICATION.Repositories.Services;
using EMS_SYSTEM.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IStudentService Students { get; }
        public IGlobalService Global { get; }
        public IGenericRepository<Faculty> Faculty { get; }
        public IGenericRepository<Subject> Subject { get; }
        public IGenericRepository<Staff> Staff {  get; }
        public IGenericRepository<Committee> Committees { get;}
        public IGenericRepository<SubjectCommittee> SubjectCommittees { get; }
        public int Save();
        public Task<int> SaveAsync();
    }
}
