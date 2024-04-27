

SELECT biz_date as BizDate, cp_master_id  as CpMasterId, cp_name as CpName, limit_c_usd as LimitUsd, pd_rate as PdRate, insurance_rate as InsuranceRate FROM Insurances

	INSERT INTO insurances(
	 biz_date, cp_master_id, cp_name, limit_c_usd, pd_rate, insurance_rate)
	VALUES ('2023-11-30', 'CP00002', 'CRM Racking Test', '21286', 0.25, 0.9);

INSERT INTO insurances(
	 biz_date, cp_master_id, cp_name, limit_c_usd, pd_rate, insurance_rate)
	VALUES ('2023-11-30', 'CP00002', 'CHINA Global', '53215', 0.36, 0.9);

	
select * FROM insurances



INSERT INTO public.counter_parties(
	 abcode_number, sales_force_name, jde_cp_name, pd_rate)
	VALUES ('12356', 'Steel Limited', 'Steel Limited Test', 0.257);

INSERT INTO counter_parties(
	 abcode_number, sales_force_name, jde_cp_name, pd_rate)
	VALUES ('3003', 'CHINA Global', 'CHINA Global', 0.36);

--delete from counter_parties where id>2

select id as Id, abcode_number as AbCodeNumber,sales_force_name as SalesForceCpName,jde_cp_name as JdeCPName,pd_rate as PdRate from counter_parties


INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P0798', '2023/11/30', 0.0, 142088.19);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P0805', '2023/11/30', 0.0, 30596.33);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P0806', '2023/11/30', 0.0, 26015.09);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P070802', '2023/11/30', 0.0, 95599.50);

INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '12356', 'Steel Limited', 'S1169', '2023/12/20', 1205113.35, 12051.35);

INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '12356', 'Steel Limited', 'S1168', '2023/12/20', 928527.25, 9227.25);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '12356', 'Steel Limited', 'S1160', '2023/12/20', 1002061.9,	12061.9);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '12356', 'Steel Limited', 'S1269', '2023/12/20', 1032969.01,	10369.01);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '12356', 'Steel Limited', 'S1369', '2023/12/20', 946199.18,	9469.18);
INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '12356', 'Steel Limited', 'S4169', '2023/12/20', 760206.15,	7606.15);


INSERT INTO arap_jdes(
	ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde)
	VALUES ('6211189.1', 'A/R Trade-FLAT123213', '12356', 'Steel Limited12', 'S416923', '2023/12/20', 760206.15,	7606.15);



--delete from arap_jdes
select * from arap_jdes order by id desc offset 3 limit 10
	
select * from arap_jdes  
	order by 
		CASE WHEN 'id asc23' = 'id asc' THEN id END ASC,
		CASE WHEN 'id asc2' = 'id asc' THEN supplier_code END ASC,
		CASE WHEN '1'='1' THEN id END asc
	offset 2 fetch next 10 rows only 
	
            

	
select count(id) from arap_jdes 

	
 select id as Id, ac_code as AcCode, description as Description, supplier_code as SupplierCode, supplier_name as SupplierName, contract_no as ContractNo, due_date as DueDate, amount_in_ctrm as AmountInCtrm, amount_in_jde as AmountInJde from arap_jdes 

    
INSERT INTO reconciles(
	 ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde
	, pd_rate, expected_loss, sf_acct_title, insurance, insurance_rate, insurance_limit_usd, net_exposure)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P0798', '2023/11/30', 0.0, 142088.19
	,0.36, 51151.75, 'CHINA Global', true, 90, 13304, 13304);

INSERT INTO reconciles(
	 ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde
	, pd_rate, expected_loss, sf_acct_title, insurance, insurance_rate, insurance_limit_usd, net_exposure)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P0799', '2023/11/30', 0.0, 142088.19
	,0.36, 51151.75, 'CHINA Global', true, 90, 13304, 13304);


INSERT INTO reconciles(
	 ac_code, description, supplier_code, supplier_name, contract_no, due_date, amount_in_ctrm, amount_in_jde
	, pd_rate, expected_loss, sf_acct_title, insurance, insurance_rate, insurance_limit_usd, net_exposure)
	VALUES ('6211189.1', 'A/R Trade-FLAT', '3003', 'CHINA Global', 'P080', '2023/11/30', 0.0, 142088.19
	,0.36, 51151.75, 'CHINA Global', true, 90, 13304, 13304);


select * from reconciles

select ac_code as AcCode, description as Description, supplier_code as SupplierCode, supplier_name as SupplierName, contract_no as ContractNo, due_date as DueDate, amount_in_ctrm as AmountInCtrm, amount_in_jde as AmountInJde, pd_rate as PdRate, 
	
	