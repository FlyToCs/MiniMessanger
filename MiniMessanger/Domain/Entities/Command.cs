using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMessenger.Domain.Entities
{
    public class Command : BaseEntity
    {
        public string? Instruction { get; set; }


    }
}
