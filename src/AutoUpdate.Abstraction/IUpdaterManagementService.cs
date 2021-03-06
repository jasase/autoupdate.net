﻿using System.Threading.Tasks;

namespace AutoUpdate.Abstraction
{
    public interface IUpdaterManagementService
    {
        bool IsActive { get; }

        void Start();

        Task<IUpdateVersionHandle> SearchVersion();

    }
}
