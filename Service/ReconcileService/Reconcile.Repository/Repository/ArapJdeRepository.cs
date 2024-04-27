using System;
using Dapper;
using Reconcile.Entity;

namespace Reconcile.Repository
{
   

    public class ArapJdeRepository : IArapJdeRepository
    {
        private DataContext _context;

        public ArapJdeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArapJde>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, ac_code as AcCode, description as Description, supplier_code as SupplierCode, supplier_name as SupplierName, contract_no as ContractNo, due_date as DueDate, amount_in_ctrm as AmountInCtrm, amount_in_jde as AmountInJde from arap_jdes 
        """;
            return await connection.QueryAsync<ArapJde>(sql);
        }

        public async Task<ArapJde> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, ac_code as AcCode, description as Description, supplier_code as SupplierCode, supplier_name as SupplierName, contract_no as ContractNo, due_date as DueDate, amount_in_ctrm as AmountInCtrm, amount_in_jde as AmountInJde from arap_jdes  
            WHERE Id = @id
        """;
            return await connection.QuerySingleOrDefaultAsync<ArapJde>(sql, new { id });
        }

        public async Task Create(ArapJde ArapJde)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            INSERT INTO arap_jdes(ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
            VALUES (@AcCode, @Description, @SupplierCode, @SupplierName, @ContractNo, @DueDate, @AmountInCtrm, @AmountInJde)
            """;
            await connection.ExecuteAsync(sql, ArapJde);
        }

        public async Task Update(ArapJde ArapJde)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            UPDATE arap_jdes 
            SET ac_code = @AcCode,
                description = @Description,
                supplier_code = @SupplierCode, 
                supplier_name = @SupplierName, 
                contract_no = @ContractNo, 
                due_date = @DueDate,
                amount_in_ctrm=@AmountInCtrm,
                amount_in_jde=@AmountInJde
            WHERE Id = @Id
        """;
            await connection.ExecuteAsync(sql, ArapJde);
        }

        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
                    DELETE FROM arap_jdes 
                    WHERE Id = @id
                """;
            await connection.ExecuteAsync(sql, new { id });
        }

        public async Task<int> GetCount()
        {
            using var connection = _context.CreateConnection();
            var sql = """
                 select count(id) from arap_jdes  
                """;
            return await connection.QueryFirstAsync<int>(sql);
        }

        public async Task<IEnumerable<ArapJde>> GetList(string filter, string sort, int offset, int size)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, ac_code as AcCode, description as Description, supplier_code as SupplierCode,
            supplier_name as SupplierName, contract_no as ContractNo,
            due_date as DueDate, amount_in_ctrm as AmountInCtrm,
            amount_in_jde as AmountInJde from arap_jdes
            ORDER BY
                CASE WHEN @sort = 'id asc' THEN id END asc,
                CASE WHEN @sort = 'id desc' THEN id END desc,
                CASE WHEN @sort = 'ac_code asc' THEN ac_code END asc,
                CASE WHEN @sort = 'ac_code desc' THEN ac_code END DESC,
                CASE WHEN @sort = 'supplier_code asc' THEN supplier_code END asc,
                CASE WHEN @sort = 'supplier_code desc' THEN supplier_code END DESC,
                CASE WHEN @sort = 'description asc' THEN description END ASC,
                CASE WHEN @sort = 'supplier_name asc' THEN supplier_name END ASC,
                CASE WHEN @sort = 'supplier_name desc' THEN supplier_name END desc,
                CASE WHEN @sort = 'contract_no asc' THEN contract_no END asc,
                CASE WHEN @sort = 'contract_no desc' THEN contract_no END DESC,
                CASE WHEN @sort = 'due_date asc' THEN due_date END ASC,
                CASE WHEN @sort = 'due_date desc' THEN due_date END desc,
                CASE WHEN '1'='1' THEN id END asc
            offset @offset fetch next @size rows only
            """;
            return await connection.QueryAsync<ArapJde>(sql, new { sort, offset, size});
        }
    }
}

