using System;
using Dapper;
using Reconcile.Entity;

namespace Reconcile.Repository
{
    

    public class ReconcileRepository : IReconcileRepository
    {
        private DataContext _context;

        public ReconcileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReconcileItem>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, ac_code as AcCode, description as Description, supplier_code as SupplierCode, supplier_name as SupplierName, contract_no as ContractNo, due_date as DueDate, amount_in_ctrm as AmountInCtrm, amount_in_jde as AmountInJde, pd_rate as PdRate, expected_loss as ExpectedLoss, sf_acct_title as SfAcctTitle, insurance as Insurance, insurance_rate as InsuranceRate, insurance_limit_usd as InsuranceLimitUsd, net_exposure as NetExposure from reconciles 	
        """;
            return await connection.QueryAsync<ReconcileItem>(sql);
        }

        public async Task<ReconcileItem> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            select id as Id, ac_code as AcCode, description as Description, supplier_code as SupplierCode, supplier_name as SupplierName, contract_no as ContractNo, due_date as DueDate, amount_in_ctrm as AmountInCtrm, amount_in_jde as AmountInJde, pd_rate as PdRate, expected_loss as ExpectedLoss, sf_acct_title as SfAcctTitle, insurance as Insurance, insurance_rate as InsuranceRate, insurance_limit_usd as InsuranceLimitUsd, net_exposure as NetExposure from reconciles 	 
            WHERE Id = @id
        """;
            return await connection.QuerySingleOrDefaultAsync<ReconcileItem>(sql, new { id });
        }

        public async Task Create(ReconcileItem ReconcileItem)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            INSERT INTO reconciles(ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde
            , pd_rate, expected_loss, sf_acct_title, insurance, insurance_rate, insurance_limit_usd, net_exposure)
            VALUES (@AcCode, @Description, @SupplierCode, @SupplierName, @ContractNo, @DueDate,
            @AmountInCtrm, @AmountInJde, @PdRate, @ExpectedLoss, @SfAcctTitle, @Insurance,
            @InsuranceRate, @InsuranceLimitUsd, @NetExposure)
            """;
            await connection.ExecuteAsync(sql, ReconcileItem);
        }

        public async Task Update(ReconcileItem ReconcileItem)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            UPDATE reconciles
            SET ac_code = @AcCode,
                description = @Description,
                supplier_code = @SupplierCode, 
                supplier_name = @SupplierName, 
                contract_no = @ContractNo, 
                due_date = @DueDate,
                amount_in_ctrm=@AmountInCtrm,
                amount_in_jde=@AmountInJde,
                pd_rate = @PdRate,
                expected_loss = @ExpectedLoss, 
                sf_acct_title = @SfAcctTitle, 
                insurance = @Insurance,
                insurance_rate = @InsuranceRate,
                insurance_limit_usd = @InsuranceLimitUsd,
                net_exposure = @NetExposure
            WHERE Id = @Id
        """;
            await connection.ExecuteAsync(sql, ReconcileItem);
        }

        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = """
            DELETE FROM reconciles
            WHERE Id = @id
        """;
            await connection.ExecuteAsync(sql, new { id });
        }


    }
}

