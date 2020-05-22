﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Persistance.Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        void Save();
        Task SaveAsync();
    }

}
