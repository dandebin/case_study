import { List, Datagrid, TextField, SimpleList, Edit,SimpleForm,ReferenceInput,TextInput, DateField } from "react-admin";

export const reconList = () => (
    <List>
        <Datagrid>
            <TextField source="id" />
            <TextField source="ac_code" />
            <TextField source="description" />
            <TextField source="supplier_code" />
            <TextField source="supplier_name" />
            <TextField source="contract_no" />
            <DateField source="due_date" />
            <TextField source="amount_in_ctrm" />
            <TextField source="amount_in_jde" />
            <TextField source="pd_rate" />
            <TextField source="expected_loss" />
            <TextField source="sf_acct_title" />
            <TextField source="insurance" />
            <TextField source="insurance_rate" />
            <TextField source="insurance_limit_usd" />
            <TextField source="net_exposure" />
        </Datagrid>
    </List>
);


export const reconcileEdit = () => (
    <Edit>
      <SimpleForm>
      <TextField source="id" />
            <TextField source="ac_code" />
            <TextField source="description" />
            <TextField source="supplier_code" />
            <TextField source="supplier_name" />
            <TextField source="countact_no" />
            <DateField source="due_date" />
            <TextField source="amount_in_ctrm_usd" />
            <TextField source="amount_in_jde" />
            <TextField source="pd_rate" />
            <TextField source="expected_loss" />
            <TextField source="sf_acct_tittle" />
            <TextField source="insurance" />
            <TextField source="insurance_rate" />
            <TextField source="insurance_limit_usd" />
            <TextField source="net_exposure" />
      </SimpleForm>
    </Edit>
  );