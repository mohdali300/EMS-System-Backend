﻿using Azure;
using EMS_SYSTEM.DOMAIN.DTO;
using EMS_SYSTEM.DOMAIN.DTO.ObserversAndInvigilators;
using EMS_SYSTEM.DOMAIN.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_SYSTEM.APPLICATION.Repositories.Interfaces
{
    public interface IObserversAndInvigilatorsService
    {
        Task<ResponseDTO>GetByNID(string id);
        Task<ResponseDTO> GetStaffCommittees(string nid);
    }
}
