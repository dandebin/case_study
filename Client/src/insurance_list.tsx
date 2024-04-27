import { List, Datagrid, TextField, SimpleList, Edit,SimpleForm,ReferenceInput,TextInput, DateField } from "react-admin";

export const insuranceList = () => (
    
    <List>
        <Datagrid>
            <TextField source="cp_name" />
            <TextField source="cp_master_id" />
            <DateField source="biz_date" />
            <TextField source="limit_c_usd" />
            <TextField source="pd_rate" />
            <TextField source="insurance_rate" />
        </Datagrid>
    </List>
);

export const insuranceEdit = () => (
    <Edit>
      <SimpleForm>
      <TextField source="cp_name" />
            <TextField source="cp_master_id" />
            <DateField source="biz_date" />
            <TextField source="limit_c_usd" />
            <TextField source="pd_rate" />
            <TextField source="insurance_rate" />
      </SimpleForm>
    </Edit>
  );