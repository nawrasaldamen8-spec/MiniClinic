using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniClinicManagementSystem.Services
{

    public class PatientSerivce : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientSerivce(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
    }
}
