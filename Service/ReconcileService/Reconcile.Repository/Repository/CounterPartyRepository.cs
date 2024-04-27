using System;
using Dapper;
using Reconcile.Entity;

namespace Reconcile.Repository
{
    

    public class CounterPartyRepository : ICounterPartyRepository
    {
        private DataContext _context;

        public CounterPartyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CounterParty>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, abcode_number as AbCodeNumber,sales_force_name as SalesForceCpName,jde_cp_name as JdeCPName,pd_rate as PdRate from counter_parties
        """;
            return await connection.QueryAsync<CounterParty>(sql);
        }

        public async Task<CounterParty> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, abcode_number as AbCodeNumber,sales_force_name as SalesForceCpName,jde_cp_name as JdeCPName,pd_rate as PdRate from counter_parties 
            WHERE Id = @id
        """;
            return await connection.QuerySingleOrDefaultAsync<CounterParty>(sql, new { id });
        }

        public async Task Create(CounterParty CounterParty)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            INSERT INTO counter_parties(abcode_number, sales_force_name, jde_cp_name, pd_rate)
            VALUES (@AbCodeNumber, @SalesForceCpName, @JdeCPName, @PdRate)
            """;
            await connection.ExecuteAsync(sql, CounterParty);
        }

        public async Task Update(CounterParty CounterParty)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            UPDATE counter_parties 
            SET abcode_number = @AbCodeNumber,
                sales_force_name = @SalesForceCpName,
                jde_cp_name = @JdeCPName, 
                pd_rate = @PdRate
            WHERE Id = @Id
        """;
            await connection.ExecuteAsync(sql, CounterParty);
        }

        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            DELETE FROM CounterPartys 
            WHERE Id = @id
        """;
            await connection.ExecuteAsync(sql, new { id });
        }


    }
}

