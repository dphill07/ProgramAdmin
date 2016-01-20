using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramAdmin.Models.DTOs
{
    public class ProgramLogDTO
    {
        public int LogId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public string UserDomainName { get; set; }
        public string UserAction { get; set; }
        public string Information { get; set; }
        public string SQLCommand { get; set; }
    }
}
