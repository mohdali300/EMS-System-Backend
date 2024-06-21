using EMS_SYSTEM.APPLICATION.Repositories.Interfaces;
using EMS_SYSTEM.APPLICATION.Repositories.Interfaces.IUnitOfWork;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.Committee;
using EMS_SYSTEM.DOMAIN.DTO.Faculty;
using EMS_SYSTEM.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net.Security;

namespace EMS_SYSTEM.APPLICATION.Repositories.Services
{
    public class FacultyService : GenericRepository<Faculty>, IFacultyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public FacultyService(UnvcenteralDataBaseContext Db, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(Db)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResponseDTO> GetFacultyDataByID(int Id)
        {

            var departmentnames = await _context.FacultyNodes.Where(d => d.FacultyId == Id)
                .Select(d => new facultyNodeDTO { Id = d.FacultyNodeId, Name = d.Name })
                .ToListAsync()
                ;
            var facultyphases = await _context.FacultyPhases.Where(p => p.FacultyId == Id)
                .Select(p => new facultyPhaseDTO { Id = p.Id, Name = p.Name })
                .ToListAsync();


            var facultyBylaws = await _context.Bylaws
                                               .Where(l => l.FacultyId == Id)
                                               .Select(l => new BylawDTO { Id = l.Id, Name = l.Name })
                                               .ToListAsync();


            var studymethod = await _context.Bylaws
                .Where(s => s.FacultyId == Id).Select(s => new StudyMethodDTO { Id = (int)s.CodeStudyMethodId!, Name = s.CodeStudyMethod!.Name })
                .ToListAsync();

            var facultysemster = await _context.FacultySemesters
                .Where(f => f.FacultyId == Id).Select(f => new facultysemsterDTO { Id = f.Id, Name = f.Name })
                .ToListAsync();

            var faculty = new FacultyDTO
            {
                BYlaw = facultyBylaws,
                StudyMethod = studymethod,
                facultyNode = departmentnames,
                facultyPhase = facultyphases,
                facultysemster = facultysemster,

            };
            if (faculty != null)
            {
                return new ResponseDTO
                {
                    Model = faculty,
                    StatusCode = 200,
                    IsDone = true
                };
            }
            else
            {
                return new ResponseDTO
                {
                    StatusCode = 400,
                    IsDone = false,
                    Message = "Couldn't Find The Faculty"
                };
            }
        }

        public async Task<ResponseDTO> GetSubjects([FromBody] FacultyHieryicalDTO hieryicalDTO)
        {
            var Hierarchical = await _context.FacultyHieryicals.Include(h => h.Subjects)
                .Where
                (h =>
                h.BylawId == hieryicalDTO.BylawId &&
                h.PhaseId == hieryicalDTO.PhaseId &&
                h.SemeterId == hieryicalDTO.FacultySemesterId
                ).FirstOrDefaultAsync();

            if (Hierarchical == null)
            {

                return new ResponseDTO
                {
                    StatusCode = 400,
                    IsDone = false,
                    Message = "Faculty hierarchical record not found"
                };
            }
            var Subjects = Hierarchical.Subjects
                .Where
                (s =>
                s.FacultyNodeId == hieryicalDTO.FacultyNodeId &&
                s.FacultyHieryricalId == Hierarchical.Id
                )
             .Select(s => new SubjectDTO
             {
                 Id = s.Id,
                 Name = s.Name

             }).ToList();
            return new ResponseDTO
            {
                Model = Subjects,
                StatusCode = 200,
                IsDone = true
            };

        }

        public async Task<ResponseDTO> GetFacultyCommitteesDetails(int id)
        {
            var CommitteesDetails = await _context.Committees
                    .Join(_context.SubjectCommittees, c => c.Id, sc => sc.CommitteeId, (c, sc) => new { Committee = c, SubjectCommittee = sc })
                    .Join(_context.Subjects, sc => sc.SubjectCommittee.SubjectId, s => s.Id, (sc, s) => new { sc, Subject = s })
                    .Join(_context.FacultyHieryicals, s => s.Subject.FacultyHieryricalId, fh => fh.Id, (s, fh) => new { s, FacultyHieryical = fh })
                    .Join(_context.FacultyPhases, fh => fh.FacultyHieryical.PhaseId, fp => fp.Id, (fh, fp) => new { fh, FacultyPhase = fp })
                    .Where(fp => fp.FacultyPhase.FacultyId == id)
                    .GroupBy(fp => new { fp.FacultyPhase.Name, fp.FacultyPhase.Id })
                    .Select(s => new
                    {
                        name = s.Key.Name,
                        value = s.Count(),
                    })
                    .AsNoTracking()
                    .ToListAsync();
            if (CommitteesDetails.Count > 0)
            {
                return new ResponseDTO
                {
                    IsDone = true,
                    StatusCode = 200,
                    Model = CommitteesDetails.ToList(),
                    Message = $"CommitteesDetails For Faculty :  {id}"
                };
            }
            return new ResponseDTO
            {
                IsDone = false,
                StatusCode = 400,
                Model = null,
                Message = $"No Committees Created Until Now for This Faculty {id}"
            };

        }
        public async Task<ResponseDTO> GetFacultyCommitteesForCurrentDay(int id)
        {
            var CommitteesDetails = await _context.Committees
                    .Join(_context.SubjectCommittees, c => c.Id, sc => sc.CommitteeId, (c, sc) => new { Committee = c, SubjectCommittee = sc })
                    .Join(_context.Subjects, sc => sc.SubjectCommittee.SubjectId, s => s.Id, (sc, s) => new { sc, Subject = s })
                    .Join(_context.FacultyHieryicals, s => s.Subject.FacultyHieryricalId, fh => fh.Id, (s, fh) => new { s, FacultyHieryical = fh })
                    .Join(_context.FacultyPhases, fh => fh.FacultyHieryical.PhaseId, fp => fp.Id, (fh, fp) => new { fh, FacultyPhase = fp })
                    .Where(fp => fp.FacultyPhase.FacultyId == id && fp.fh.s.sc.Committee.Date == DateTime.Now.Date)
                    .GroupBy(fp => new { fp.FacultyPhase.Name, fp.FacultyPhase.Id })
                    .Select(s => new
                    {
                        name = s.Key.Name,
                        value = s.Count(),
                    })
                    .AsNoTracking()
                    .ToListAsync();
            if (CommitteesDetails.Count > 0)
            {
                return new ResponseDTO
                {
                    IsDone = true,
                    StatusCode = 200,
                    Model = CommitteesDetails.ToList(),
                    Message = $"CommitteesDetails For Faculty :  {id}"
                };
            }
            return new ResponseDTO
            {
                IsDone = false,
                StatusCode = 400,
                Model = null,
                Message = $"No Committees Created Until Now for This Faculty {id}"
            };

        }


        public async Task<ResponseDTO> GetPlaces()
        {
            var Places = _context.Palces.ToList();

            return new ResponseDTO
            {
                IsDone = true,
                StatusCode = 400,
                Model = Places,
            };

        }

        //////////////
        ///helper
        public async Task<int> GetStudentCountForCommittee(int committeeId, int facultyId)
        {
            var committee = await _context.Committees
                .Include(c => c.StudentsCommittees)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.Id == committeeId && c.Date == DateTime.Today);

            if (committee == null)
            {
                return -1;
            }
            return committee.StudentsCommittees.Count(sc => sc.Student?.Facultyid == facultyId);
        }
        public async Task<ResponseDTO> GetStudentCountInActiveCommitteesForFacultyToday(int facultyId)
        {
            var activeCommittees = await _context.Committees
                .Where(c => c.Date == DateTime.Today)
                .Select(c => c.Id)
                .ToListAsync();

            if (!activeCommittees.Any())
            {
                return new ResponseDTO
                {
                    StatusCode = 404,
                    IsDone = false,
                    Message = "No active committees found for today."
                };
            }


            var totalStudentCount = 0;
            foreach (var committeeId in activeCommittees)
            {
                var studentCount = await GetStudentCountForCommittee(committeeId, facultyId);
                if (studentCount != -1)
                {
                    totalStudentCount += studentCount;
                }
            }

            return new ResponseDTO
            {
                StatusCode = 200,
                IsDone = true,
                Model = new { TotalStudentCount = totalStudentCount }
            };
        }
        //done

        //all staff in faculty
        public async Task<ResponseDTO> GetAllStaffInFaculty(int facultyId)
        {
            var staffInFaculty = await _context.Staff
                .Where(s => s.FacultyId == facultyId)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Degree,
                    s.NID
                })
                .ToListAsync();

            if (staffInFaculty.Count == 0)
            {
                return new ResponseDTO
                {
                    StatusCode = 404,
                    IsDone = false,
                    Message = "No staff found for the specified faculty."
                };
            }

            var staffDTOs = new List<StaffDTO>();
            foreach (var staff in staffInFaculty)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NID == staff.NID);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault(r => r == "Invigilators" || r == "Observers");

                    if (role != null)
                    {
                        staffDTOs.Add(new StaffDTO
                        {
                            Id = staff.Id,
                            Name = staff.Name,
                            Degree = staff.Degree,
                            Role = role
                        });
                    }
                }
            }

            return new ResponseDTO
            {
                Model = staffDTOs,
                StatusCode = 200,
                IsDone = true
            };
        }
        //just assigned staff in faculty
        public async Task<ResponseDTO> GetStaffInCommitteesForFaculty(int facultyId)
        {
            var facultyCommittees = await _context.Committees
                .Where(c => c.SubjectCommittees.Any(sc => sc.Subject.FacultyNode.FacultyId == facultyId))
                .Include(c => c.StaffCommittees)
                .ThenInclude(sc => sc.Staff)
                .ToListAsync();

            if (!facultyCommittees.Any())
            {
                return new ResponseDTO
                {
                    StatusCode = 404,
                    IsDone = false,
                    Message = "No committees found for the specified faculty."
                };
            }

            var staffDetails = facultyCommittees
                .SelectMany(c => c.StaffCommittees)
                .Where(sc => sc.Staff != null && sc.Staff.FacultyId == facultyId)
                .Select(sc => new StaffDTO
                {
                    Id = sc.Staff.Id,
                    Name = sc.Staff.Name,
                    Degree = sc.Staff.Degree
                })
                .DistinctBy(s => s.Id)
                .ToList();

            return new ResponseDTO
            {
                StatusCode = 200,
                IsDone = true,
                Model = new { staff = staffDetails }
            };
        }






    }




}
