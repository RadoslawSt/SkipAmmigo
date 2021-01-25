using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace JumpApp.Services
{
    public interface IDatabaseConnection
    {
        SQLiteConnection GetDbConnection();
    }
}
