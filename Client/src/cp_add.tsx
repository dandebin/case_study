import { List, Datagrid, TextField, SimpleForm,TextInput,  NumberInput, Create, required} from "react-admin";

export const CpCreate = () => {
    const validatePdRate = (value: number) => {
        if (value < 0) {
            return 'Must be over 0';
        }else if(value>1){
            return 'Must be less 1'
        }

        required();
        return '';
    }

    return (
        <Create >
            <SimpleForm >
                <TextInput source="abcode_number" validate={[required()]} label="AB Code number" fullWidth  />
                <TextInput source="sales_force_name" validate={[required()]} label="SalesForce CP Name" fullWidth />
                <TextInput source="jde_cp_name" validate={[required()]} label="JDE CP NAME" fullWidth />
                <NumberInput source="pd_rate" validate={validatePdRate} label="PD Rate" defaultValue={0.36} fullWidth/>
            </SimpleForm>
        </Create>
    );
}