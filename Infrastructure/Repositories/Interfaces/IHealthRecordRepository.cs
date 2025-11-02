using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IHealthRecordRepository
    {
        HealthRecord GetByID(int id);
        IEnumerable<HealthRecord> GetAll();
        int Add(HealthRecord healthRecord);
        int Update(HealthRecord healthRecord);
        int Delete(int id);
    }
}
