using System;

namespace ProgramAdmin.Models.DTOs
{
    public class ReleaseNoteDTO
    {
        public string ProgramName { get; set; }
        public int ProgramId { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; } 
    }
}