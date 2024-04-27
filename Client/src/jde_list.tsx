import { List, Datagrid, TextField, SimpleList, Edit,SimpleForm,ReferenceInput,TextInput, DateField } from "react-admin";

export const jdeList = () => (
    <List>
        <Datagrid>
            <TextField source="id" />
            <TextField source="ac_code" />
            <TextField source="description" />
            <TextField source="supplier_code" />
            <TextField source="supplier_name" />
            <TextField source="contract_no" />
            <DateField source="due_date" />
            <TextField source="amount_in_ctrm_usd" />
            <TextField source="amount_in_jde" />
        </Datagrid>
    </List>
);