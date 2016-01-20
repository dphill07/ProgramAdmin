using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramAdmin.Models.DTOs
{
    public class ProgramErrorDTO
    {
        public int ErrorId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public string UserDomainName { get; set; }
        public string Data { get; set; }
        public string HelpLink { get; set; }
        public string HResult { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
    }
}
