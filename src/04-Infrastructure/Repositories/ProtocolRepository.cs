using Dapper;
using Dapper.Contrib.Extensions;
using PosTech.Fase3.AddContact.Domain.Entities;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using System.Data;


namespace PosTech.Fase3.AddContact.Infrastructure.Repositories
{
    public class ProtocolRepository: IProtocolRepository
    {
        private readonly IDbConnection _connection;

        public ProtocolRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task InsertAsync(ProtocolEntity entity)
        {
            string sql = @"
                INSERT INTO ProtocolEntity (Id, CreatedIn, Status, Message)
                VALUES (@Id, @CreatedIn, @Status, null);";

            await _connection.ExecuteAsync(sql, new
            {
                Id = entity.Id,
                CreatedIn = entity.CreatedIn,
                status = entity.Status
            });
        }
    }
}
