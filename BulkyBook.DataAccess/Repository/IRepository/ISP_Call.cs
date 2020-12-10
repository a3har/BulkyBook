using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        T Single<T>(string procedureName, DynamicParameters param);
        T OneRecord<T>(string procedureName, DynamicParameters param);
        void Execute(string procedureName, DynamicParameters param);
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param);
        Tuple<IEnumerable<T1>,IEnumerable<T2>> List<T1,T2>(string procedureName, DynamicParameters param);
    }
}
