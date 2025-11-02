using Infrastructure.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{

    public class Member : GymUser
    {

        public string Photo { get; set; } = null!;
        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<Booking> MemberSessions { get; set; }
        public ICollection<MemberShip> MemberPlans { get; set; } 

    }
}
