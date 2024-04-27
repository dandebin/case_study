namespace Reconcile.Repository;

using System.Collections.Generic;
using Dapper;
using Reconcile.Entity;



public class InsuranceRepository : IInsuranceRepository
{
    private DataContext _context;

    public InsuranceRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Insurance>> GetAll()
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT id as Id, biz_date as BizDate, cp_master_id  as CpMasterId, cp_name as CpName, limit_c_usd as LimitUsd, pd_rate as PdRate, insurance_rate as InsuranceRate FROM Insurances
        """;
        return await connection.QueryAsync<Insurance>(sql);
    }

    public async Task<Insurance> GetById(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT id as Id, biz_date as BizDate, cp_master_id  as CpMasterId, cp_name as CpName, limit_c_usd as LimitUsd, pd_rate as PdRate, insurance_rate as InsuranceRate FROM Insurances 
            WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Insurance>(sql, new { id });
    }

    public async Task Create(Insurance insurance)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO insurances(biz_date, cp_master_id, cp_name, limit_c_usd, pd_rate, insurance_rate)
            VALUES (@BizDate, @CpMasterId, @CpName, @LimitUsd, @PdRate, @InsuranceRate)
            """;
        await connection.ExecuteAsync(sql, insurance);
    }

    public async Task Update(Insurance Insurance)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE Insurances 
            SET biz_date = @BizDate,
                cp_master_id = @CpMasterId,
                cp_name = @CpName, 
                limit_c_usd = @LimitUsd, 
                pd_rate = @PdRate, 
                insurance_rate = @InsuranceRate
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, Insurance);
    }

    public async Task Delete(int id)
    {
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM Insurances 
            WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}