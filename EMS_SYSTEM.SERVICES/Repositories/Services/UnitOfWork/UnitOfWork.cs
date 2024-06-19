using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.GenericRepository;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly UnvcenteralDataBaseContext _context;
        public IStudentService Students { get; private set; }
        public IGenericRepository<GlobalService> Global { get; private set; }

        public IGenericRepository<Faculty> Faculty { get; private set; }
        public IGenericRepository<Subject> Subject { get; private set; }

        public IGenericRepository<Committee> Committees { get; private set; }
        public IGenericRepository<SubjectCommittee> SubjectCommittees { get; private set; }
        public IGenericRepository<Staff> Staff {  get; private set; }
        public UnitOfWork(UnvcenteralDataBaseContext _context)
        {
            this._context = _context;
            Faculty = new GenericRepository<Faculty>(_context);
            Subject = new GenericRepository<Subject>(_context);
            Students = new StudentService(_context);
            Staff = new GenericRepository<Staff>(_context);
            Committees= new GenericRepository<Committee>(_context);
            SubjectCommittees = new GenericRepository<SubjectCommittee>(_context);
            Global=new GenericRepository<GlobalService>(_context);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
