using ProgramAdmin.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using LoggingTools;
using ProgramAdmin.Models;

namespace ProgramAdmin.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private List<ProgramDTO> _programs;
        private List<ProgramErrorDTO> _programErrors;
        private List<ProgramLogDTO> _programLogs;
        private DelegateCommand _getPrograms;
        private DelegateCommand _getErrors;
        private DelegateCommand _getLogs;
        private DelegateCommand _addProgram;
        private DelegateCommand _addReleaseNote;
        private DelegateCommand _programAdded;
        private DelegateCommand _programCancelled;
        private DelegateCommand _editProgramInformation;
        private DelegateCommand _getProgramLogs;
        private DelegateCommand _getUserLogs;
        private DelegateCommand _getLogsByDate;
        private bool _addingProgram;
        private ProgramDTO _program;
        private bool _showMessage;
        private bool _isBusy;
        private Service _service;
        private KeyValuePair<int, string> _editId;
        private KeyValuePair<int, string> _logProgramId;
        private bool _editing;
        private string _addProgramMessage;
        private Dictionary<int, string> _programList;
        private Dictionary<int, string> _logPrograms;
        private List<string> _userList;
        private string _selectedUser;
        private DateTime _startDate;
        private DateTime _endDate;
        private ReleaseNoteDTO _releaseNote;
        private List<ReleaseNoteDTO> _notes;
        private KeyValuePair<int, string> _releaseNoteId;

        public KeyValuePair<int, string> ReleaseNoteId
        {
            get { return _releaseNoteId; }
            set
            {
                _releaseNoteId = value;
                ReleaseNote.ProgramId = ReleaseNoteId.Key;
                OnPropertyChanged(() => ReleaseNoteId);
            }
        }
        public List<ReleaseNoteDTO> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged(() => Notes);
            }
        }
        public ReleaseNoteDTO ReleaseNote
        {
            get { return _releaseNote; }
            set
            {
                _releaseNote = value;
                OnPropertyChanged(() => ReleaseNote);
            }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }
        public string SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(() => SelectedUser);
            }
        }
        public List<string> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged(() => UserList);
            }
        }
        public Dictionary<int, string> LogPrograms
        {
            get { return _logPrograms; }
            set
            {
                _logPrograms = value;
                OnPropertyChanged(() => LogPrograms);
            }
        }
        public List<ProgramDTO> Programs
        {
            get { return _programs; }
            set 
            { 
                _programs = value;
                OnPropertyChanged(() => Programs);
            }
        }
        public List<ProgramErrorDTO> ProgramErrors
        {
            get { return _programErrors; }
            set 
            { 
                _programErrors = value;
                OnPropertyChanged(() => ProgramErrors);
            }
        }
        public List<ProgramLogDTO> ProgramLogs
        {
            get { return _programLogs; }
            set 
            { 
                _programLogs = value;
                OnPropertyChanged(() => ProgramLogs);
            }
        }
        public DelegateCommand AddReleaseNoteCommand
        {
            get { return _addReleaseNote; }
        }
        public DelegateCommand GetProgramsCommand
        {
            get { return _getPrograms; }
        }
        public DelegateCommand GetErrorsCommand
        {
            get { return _getErrors; }
        }
        public DelegateCommand GetLogsCommand
        {
            get { return _getLogs; }
        }
        public DelegateCommand AddProgramCommand
        {
            get { return _addProgram; }
        }
        public DelegateCommand ProgramAddedCommand
        {
            get { return _programAdded; }
        }
        public DelegateCommand ProgramCancelledCommand
        {
            get { return _programCancelled; }
        }
        public DelegateCommand EditProgramInformationCommand
        {
            get { return _editProgramInformation; }
        }
        public DelegateCommand GetProgramLogsCommand
        {
            get { return _getProgramLogs; }
        }
        public DelegateCommand GetUserLogsCommand
        {
            get { return _getUserLogs; }
        }
        public DelegateCommand GetLogsByDateCommand
        {
            get { return _getLogsByDate; }
        }
        public bool AddingProgram
        {
            get { return _addingProgram; }
            set
            {
                _addingProgram = value;
                OnPropertyChanged(() => AddingProgram);
            }
        }
        public ProgramDTO Program
        {
            get { return _program; }
            set
            {
                _program = value;
                OnPropertyChanged(() => Program);
            }
        }
        public bool ShowMessage
        {
            get { return _showMessage; }
            set
            {
                _showMessage = value;
                OnPropertyChanged(() => ShowMessage);
            }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(() => IsBusy);
            }
        }
        public KeyValuePair<int, string> EditId
        {
            get { return _editId; }
            set
            {
                _editId = value;
                OnPropertyChanged(() => EditId);
            }
        }
        public KeyValuePair<int, string> LogProgramId
        {
            get { return _logProgramId; }
            set
            {
                _logProgramId = value;
                OnPropertyChanged(() => LogProgramId);
            }
        }
        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                OnPropertyChanged(() => Editing);
            }
        }
        public string AddProgramMessage
        {
            get { return _addProgramMessage; }
            set
            {
                _addProgramMessage = value;
                OnPropertyChanged(() => AddProgramMessage);
            }
        }
        public Dictionary<int, string> ProgramList
        {
            get { return _programList; }
            set
            {
                _programList = value;
                OnPropertyChanged(() => ProgramList);
            }
        }

        public MainViewModel()
        {
            _startDate = new DateTime(2015, 1, 1);
            _endDate = DateTime.Now;
            _service = new Service();
            _getPrograms = new DelegateCommand(p => PopulateLists());
            _getErrors = new DelegateCommand(p => GetErrors());
            _getLogs = new DelegateCommand(p => GetLogs());
            _addProgram = new DelegateCommand(p => AddProgram());
            _programAdded = new DelegateCommand(p => ProgramAdded());
            _programCancelled = new DelegateCommand(p => ProgramCancelled());
            _editProgramInformation = new DelegateCommand(p => EditProgramInformation());
            _getProgramLogs = new DelegateCommand(p => GetProgramLogs());
            _getUserLogs = new DelegateCommand(p => GetUserLogs());
            _getLogsByDate = new DelegateCommand(p => GetLogsByDate());
            _addReleaseNote = new DelegateCommand(p => AddReleaseNote());
            AddProgramMessage = "Add Program";
            Notes = _service.GetReleaseNotes();
            Editing = false;
            PopulateLists();
            InitializeReleaseNote();
        }
        private void InitializeReleaseNote()
        {
            ReleaseNote = new ReleaseNoteDTO()
            {
                Major = 0,
                Minor = 0,
                Build = 0,
                Revision = 0
            };
            ReleaseNoteId = ProgramList.OrderBy(d => d.Value).FirstOrDefault();
        }
        private void AddReleaseNote()
        {
            try
            {
                _service.AddReleaseNote(ReleaseNote);
                InitializeReleaseNote();
                Notes = _service.GetReleaseNotes();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogThis(ex);
            }
        }
        private void GetLogsByDate()
        {
            ProgramLogs = _service.GetLogsByDate(_startDate, _endDate);
        }
        private void GetProgramLogs()
        {
            if (LogProgramId.Key == 0)
                return;
            ProgramLogs = _service.GetLogsByProgram(LogProgramId.Key);
        }
        private void PopulateLists()
        {
            Programs = _service.GetPrograms();
            ProgramList = new Dictionary<int, string>();
            LogPrograms = new Dictionary<int, string>();

            foreach (var program in Programs)
            {
                ProgramList.Add(program.ProgramId, program.ProgramName);
            }
            foreach (var program in Programs)
            {
                LogPrograms.Add(program.ProgramId, program.ProgramName);
            }
            UserList = _service.GetUsers();
        }
        private void GetUserLogs()
        {
            if (!string.IsNullOrWhiteSpace(_selectedUser))
            {
                ProgramLogs = _service.GetLogsByUser(_selectedUser);
            }
        }
        private void GetErrors()
        {
            ProgramErrors = _service.GetErrors();
        }
        private void GetLogs()
        {
            ProgramLogs = _service.GetLogs();
        }
        private void AddProgram()
        {
            AddingProgram = true;
            Program = new ProgramDTO()
            {
                Comments = "",
                Notes = "",
                Language = "C#",
                Developer = Environment.UserName
            };
        }
        private async void ProgramAdded()
        {
            if (string.IsNullOrWhiteSpace(Program.ProgramName) || string.IsNullOrWhiteSpace(Program.Language) || string.IsNullOrWhiteSpace(Program.Path) || string.IsNullOrWhiteSpace(Program.PrimaryUser)
                || string.IsNullOrWhiteSpace(Program.Status) || string.IsNullOrWhiteSpace(Program.Developer))
            {
                ShowMessage = true;
                return;
            }
            IsBusy = true;
            ShowMessage = false;
            AddingProgram = false;

            if (!Editing)
            {
                try
                {
                    await Task.Factory.StartNew(() =>
                    {
                        _service.AddProgram(Program);
                    });
                    Programs = _service.GetPrograms();
                    ProgramList = new Dictionary<int, string>();

                    foreach (var program in Programs)
                    {
                        ProgramList.Add(program.ProgramId, program.ProgramName);
                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogThis(ex);
                }
            }
            else
            {
                try
                {
                    await Task.Factory.StartNew(() =>
                    {
                        _service.EditProgramInformation(Program);
                    });
                    Programs = _service.GetPrograms();
                    Editing = false;
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogThis(ex);
                }
            }
        }
        private void EditProgramInformation()
        {
            if (EditId.Key == 0)
                return;
            Editing = true;
            AddingProgram = true;
            AddProgramMessage = "Save";
            Program = Programs.First(d => d.ProgramId == EditId.Key);
        }
        private void ProgramCancelled()
        {
            AddingProgram = false;
            ShowMessage = false;
            Editing = false;
        }
    }
}
