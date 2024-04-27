DROP TABLE IF EXISTS insurances;

CREATE TABLE IF NOT EXISTS insurances
(
    id serial NOT null,
    biz_date date NOT NULL,
    cp_master_id varchar(100),
    cp_name varchar(100),
    limit_c_usd double precision,
    pd_rate double precision,
    insurance_rate double precision,
    CONSTRAINT "Insurances_pkey" PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."insurances"
    OWNER to postgres;


DROP TABLE IF EXISTS counter_parties;
CREATE TABLE IF NOT EXISTS counter_parties
(
    id serial NOT null,
    abcode_number varchar(100),
    sales_force_name varchar(100),
	jde_cp_name varchar(100),
	pd_rate double precision
);


DROP TABLE IF EXISTS arap_jdes;
CREATE TABLE IF NOT EXISTS arap_jdes
(
    id serial NOT null,
	ac_code varchar(100),
	description varchar(100),
	supplier_code varchar(100),
    supplier_name varchar(100),
	contract_no varchar(100),
	due_date date,
	amount_in_ctrm double precision,
	amount_in_jde double precision
);


DROP TABLE IF EXISTS reconciles;
CREATE TABLE IF NOT EXISTS reconciles
(
	id serial NOT null,
	ac_code varchar(100),
	description varchar(100),
	supplier_code varchar(100),
    supplier_name varchar(100),
	contract_no varchar(100),
	due_date date,
	amount_in_ctrm double precision,
	amount_in_jde double precision,
	pd_rate double precision,
	expected_loss double precision,
	sf_acct_title varchar(100),
	insurance bool,
	insurance_rate double precision,
	insurance_limit_usd double precision,
    net_exposure double precision
);
    