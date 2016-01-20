using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramAdmin.Models.DTOs
{
    public class ProgramDTO
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Language { get; set; }
        public string Path { get; set; }
        public string Comments { get; set; }
        [DisplayName("Primary User(s)")]
        public string PrimaryUser { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        [DisplayName("Developer(s)")]
        public string Developer { get; set; }
        public bool InProduction { get; set; }
    }
}
