import { List, Datagrid, SimpleForm,TextInput,  NumberInput, Create, required,DateField, DateInput} from "react-admin";

export const jdeCreate = () => {
    return (
        <Create >
            <SimpleForm >
                
                <TextInput source="ac_code" fullWidth validate={[required()]}/>
                <TextInput source="description" fullWidth validate={[required()]}/>
                <TextInput source="supplier_name" fullWidth validate={[required()]}/>
                <TextInput source="supplier_code" fullWidth validate={[required()]}/>
                <TextInput source="contract_no" fullWidth validate={[required()]}/>
                <DateInput source="due_date" fullWidth validate={[required()]}/>
                <TextInput source="amount_in_ctrm_usd" fullWidth validate={[required()]}/>
                <TextInput source="amount_in_jde" fullWidth validate={[required()]}/>
            </SimpleForm>
        </Create>
    );
}