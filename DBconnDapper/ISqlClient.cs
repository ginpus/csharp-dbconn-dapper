using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBconnDapper
{
    internal interface ISqlClient
    {
        /*        IEnumerable<T> ReadAll<T>(string fileName);

                void Append<T>(string fileName, T item);

                void WriteAll<T>(string fileName, IEnumerable<T> items);

                void DeleteFileContents(string fileName);*/

        int Execute(string sql, object param = null);

        IEnumerable<T> Query<T>(string sql, object param = null);
    }
}