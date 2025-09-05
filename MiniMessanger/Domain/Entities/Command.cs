using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MiniMessenger.Domain.Entities
{
    public class Command : BaseEntity
    {
        
        public string Instruction { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }

    }
}
