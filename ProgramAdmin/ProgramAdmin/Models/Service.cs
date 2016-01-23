using LoggingTools;
using ProgramAdmin.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramAdmin.Models
{
    public class Service
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["LoggingToolsDB"].ConnectionString;
        public Service()
        {

        }
        public List<ProgramDTO> GetPrograms()
        {
            var programList = new List<ProgramDTO>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"SELECT * FROM Programs ORDER BY ProgramName"
                };
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ProgramDTO()
                    {
                        ProgramId = Convert.ToInt32(reader["ProgramId"]),
                        ProgramName = reader["ProgramName"].ToString(),
                        Language = reader["Language"].ToString(),
                        Path = reader["Path"].ToString(),
                        Comments = reader["Comments"].ToString(),
                        PrimaryUser = reader["PrimaryUser"].ToString(),
                        Status = reader["Status"].ToString(),
                        Notes = reader["Notes"].ToString(),
                        Developer = reader["Developer"].ToString(),
                        InProduction = reader["InProduction"] != DBNull.Value && Convert.ToBoolean(reader["InProduction"]),
                    };
                    programList.Add(dto);
                }
            }
            return programList;
        }
        public List<ProgramErrorDTO> GetErrors()
        {
            List<ProgramErrorDTO> errorList = new List<ProgramErrorDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"SELECT *
                                            FROM Errors e
                                            JOIN Programs p
                                            ON e.ProgramId = p.ProgramId
                                            ORDER BY TimeStamp DESC";
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ProgramErrorDTO();
                    dto.ErrorId = reader["ErrorId"] != DBNull.Value ? Convert.ToInt32(reader["ErrorId"]) : 0;
                    dto.ProgramId = reader["ProgramId"] != DBNull.Value ? Convert.ToInt32(reader["ProgramId"]) : 0;
                    dto.ProgramName = reader["ProgramName"].ToString();
                    dto.TimeStamp = reader["TimeStamp"] != DBNull.Value ? Convert.ToDateTime(reader["TimeStamp"]) : default(DateTime);
                    dto.ComputerName = reader["ComputerName"].ToString();
                    dto.UserName = reader["UserName"].ToString();
                    dto.UserDomainName = reader["UserDomainName"].ToString();
                    dto.Data = reader["Data"].ToString();
                    dto.HelpLink = reader["HelpLink"].ToString();
                    dto.HResult = reader["HResult"].ToString();
                    dto.InnerException = reader["InnerException"].ToString();
                    dto.Message = reader["Message"].ToString();
                    dto.Source = reader["Source"].ToString();
                    dto.StackTrace = reader["StackTrace"].ToString();
                    dto.TargetSite = reader["TargetSite"].ToString();
                    errorList.Add(dto);
                }
            }
            return errorList;
        }
        public List<ProgramLogDTO> GetLogs()
        {
            List<ProgramLogDTO> logList = new List<ProgramLogDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"SELECT *
                                            FROM Logs l
                                            JOIN Programs p
                                            ON l.ProgramId = p.ProgramId
                                            ORDER BY TimeStamp DESC";
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ProgramLogDTO()
                    {
                        LogId = reader["LogId"] != DBNull.Value ? Convert.ToInt32(reader["LogId"]) : 0,
                        ProgramId = reader["ProgramId"] != DBNull.Value ? Convert.ToInt32(reader["ProgramId"]) : 0,
                        ProgramName = reader["ProgramName"].ToString(),
                        TimeStamp = reader["TimeStamp"] != DBNull.Value ? Convert.ToDateTime(reader["TimeStamp"]) : DateTime.MinValue,
                        ComputerName = reader["ComputerName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserDomainName = reader["UserDomainName"].ToString(),
                        UserAction = reader["UserAction"].ToString(),
                        Information = reader["Information"].ToString(),
                        SQLCommand = reader["SQLCommand"].ToString()
                    };
                    logList.Add(dto);
                }
            }
            return logList;
        }
        public void AddProgram(ProgramDTO dto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(@"INSERT INTO Programs
                                                            (ProgramName, Language, Path, Comments, PrimaryUser, Status, Notes, Developer, InProduction)
                                                          VALUES
                                                            (@ProgramName, @Language, @Path, @Comments, @PrimaryUser, @Status, @Notes, @Developer, @InProduction)");
                conn.Open();
                command.CommandType = CommandType.Text;
                command.Connection = conn;
                command.Parameters.AddWithValue("@ProgramName", dto.ProgramName);
                command.Parameters.AddWithValue("@Language", dto.Language);
                command.Parameters.AddWithValue("@Path", dto.Path);
                command.Parameters.AddWithValue("@Comments", dto.Comments);
                command.Parameters.AddWithValue("@PrimaryUser", dto.PrimaryUser);
                command.Parameters.AddWithValue("@Status", dto.Status);
                command.Parameters.AddWithValue("@Notes", dto.Notes);
                command.Parameters.AddWithValue("@Developer", dto.Developer);
                command.Parameters.AddWithValue("@InProduction", dto.InProduction);
                command.ExecuteNonQuery();

//                UsageLogger.LogThis(command);
            }
        }
        public void EditProgramInformation(ProgramDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE Programs SET Comments = @Comments, PrimaryUser = @PrimaryUser, Status = @Status, Notes = @Notes,
                                                    Developer = @Developer, InProduction = @InProduction WHERE ProgramId = @ProgramId");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Comments", dto.Comments);
                cmd.Parameters.AddWithValue("@PrimaryUser", dto.PrimaryUser);
                cmd.Parameters.AddWithValue("@Status", dto.Status);
                cmd.Parameters.AddWithValue("@Notes", dto.Notes);
                cmd.Parameters.AddWithValue("@Developer", dto.Developer);
                cmd.Parameters.AddWithValue("@InProduction", dto.InProduction);
                cmd.Parameters.AddWithValue("@ProgramId", dto.ProgramId);
                connection.Open();
                cmd.ExecuteNonQuery();

                UsageLogger.LogThis(cmd);
            }
        }
        public List<ProgramLogDTO> GetLogsByDate(DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            var logList = new List<ProgramLogDTO>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"SELECT *
                                            FROM ProgramLogs pl
                                            JOIN Programs p
                                            ON pl.ProgramId = p.ProgramId
                                            WHERE pl.TimeStamp BETWEEN @StartDate AND @EndDate
                                            ORDER BY TimeStamp DESC"
                };
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ProgramLogDTO()
                    {
                        LogId = reader["LogId"] != DBNull.Value ? Convert.ToInt32(reader["LogId"]) : 0,
                        ProgramId = reader["ProgramId"] != DBNull.Value ? Convert.ToInt32(reader["ProgramId"]) : 0,
                        ProgramName = reader["ProgramName"].ToString(),
                        TimeStamp = reader["TimeStamp"] != DBNull.Value ? Convert.ToDateTime(reader["TimeStamp"]) : DateTime.MinValue,
                        ComputerName = reader["ComputerName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserDomainName = reader["UserDomainName"].ToString(),
                        UserAction = reader["UserAction"].ToString(),
                        Information = reader["Information"].ToString(),
                        SQLCommand = reader["SQLCommand"].ToString()
                    };
                    logList.Add(dto);
                }
            }
            return logList;
        }
        public List<ProgramLogDTO> GetLogsByUser(string userName)
        {
            var logList = new List<ProgramLogDTO>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"SELECT *
                                            FROM Logs l
                                            JOIN Programs p
                                            ON l.ProgramId = p.ProgramId
                                            WHERE l.UserName = @UserName
                                            ORDER BY TimeStamp DESC"
                };
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ProgramLogDTO()
                    {
                        LogId = reader["LogId"] != DBNull.Value ? Convert.ToInt32(reader["LogId"]) : 0,
                        ProgramId = reader["ProgramId"] != DBNull.Value ? Convert.ToInt32(reader["ProgramId"]) : 0,
                        ProgramName = reader["ProgramName"].ToString(),
                        TimeStamp = reader["TimeStamp"] != DBNull.Value ? Convert.ToDateTime(reader["TimeStamp"]) : DateTime.MinValue,
                        ComputerName = reader["ComputerName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserDomainName = reader["UserDomainName"].ToString(),
                        UserAction = reader["UserAction"].ToString(),
                        Information = reader["Information"].ToString(),
                        SQLCommand = reader["SQLCommand"].ToString()
                    };
                    logList.Add(dto);
                }
            }
            return logList;
        }
        public List<ProgramLogDTO> GetLogsByProgram(int programId)
        {
            var logList = new List<ProgramLogDTO>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"SELECT *
                                            FROM ProgramLogs pl
                                            JOIN Programs p
                                            ON pl.ProgramId = p.ProgramId
                                            WHERE pl.ProgramId = @ProgramId
                                            ORDER BY TimeStamp DESC"
                };
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ProgramId", programId);
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ProgramLogDTO()
                    {
                        LogId = reader["LogId"] != DBNull.Value ? Convert.ToInt32(reader["LogId"]) : 0,
                        ProgramId = reader["ProgramId"] != DBNull.Value ? Convert.ToInt32(reader["ProgramId"]) : 0,
                        ProgramName = reader["ProgramName"].ToString(),
                        TimeStamp = reader["TimeStamp"] != DBNull.Value ? Convert.ToDateTime(reader["TimeStamp"]) : DateTime.MinValue,
                        ComputerName = reader["ComputerName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserDomainName = reader["UserDomainName"].ToString(),
                        UserAction = reader["UserAction"].ToString(),
                        Information = reader["Information"].ToString(),
                        SQLCommand = reader["SQLCommand"].ToString()
                    };
                    logList.Add(dto);
                }
            }
            return logList;
        }
        public List<string> GetUsers()
        {
            var userList = new List<string>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"SELECT DISTINCT(UserName)
                                    FROM Logs
                                    ORDER BY UserName"
                };
                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(reader["UserName"].ToString());
                }
            }
            return userList;
        }
        public void AddReleaseNote(ReleaseNoteDTO dto)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"INSERT INTO ReleaseNotes
                                    (ProgramId, Major, Minor, Build, Revision, Notes, Date)
                                    VALUES
                                    (@ProgramId, @Major, @Minor, @Build, @Revision, @Notes, GETDATE())"
                };
                sqlCommand.Parameters.AddWithValue("@ProgramId", dto.ProgramId);
                sqlCommand.Parameters.AddWithValue("@Major", dto.Major);
                sqlCommand.Parameters.AddWithValue("@Minor", dto.Minor);
                sqlCommand.Parameters.AddWithValue("@Build", dto.Build);
                sqlCommand.Parameters.AddWithValue("@Revision", dto.Revision);
                sqlCommand.Parameters.AddWithValue("@Notes", dto.Notes);

                UsageLogger.LogThis(sqlCommand);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public List<ReleaseNoteDTO> GetReleaseNotes()
        {
            var noteList = new List<ReleaseNoteDTO>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"SELECT r.ProgramId, p.ProgramName, Major, Minor, Build, Revision, r.Notes, [Date]
                                    FROM ReleaseNotes r
                                    JOIN Programs p
                                    ON r.ProgramId = p.ProgramId
                                    ORDER BY [Date] DESC"
                };

                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new ReleaseNoteDTO()
                    {
                        ProgramId = reader["ProgramId"] != DBNull.Value ? Convert.ToInt32(reader["ProgramId"]) : 0,
                        ProgramName = reader["ProgramName"].ToString(),
                        Major = reader["Major"] != DBNull.Value ? Convert.ToInt32(reader["Major"]) : 0,
                        Minor = reader["Minor"] != DBNull.Value ? Convert.ToInt32(reader["Minor"]) : 0,
                        Build = reader["Build"] != DBNull.Value ? Convert.ToInt32(reader["Build"]) : 0,
                        Revision = reader["Revision"] != DBNull.Value ? Convert.ToInt32(reader["Revision"]) : 0,
                        Notes = reader["Notes"].ToString(),
                        Date = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"]) : DateTime.MinValue
                    };
                    noteList.Add(dto);
                }
            }
            return noteList;
        }
    }
}
