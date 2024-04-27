import { List, Datagrid, TextField, SimpleForm,TextInput,  NumberInput, Create, required,DateField, DateInput} from "react-admin";

import PdRateInput from "./custom_component/pd_rate"
import InsuranceRateInput from "./custom_component/insurance_rate"

export const insuranceCreate = () => {
    return (
        <Create >
            <SimpleForm >
                <TextInput source="cp_name" fullWidth validate={[required()]} />
                <TextInput source="cp_master_id" fullWidth validate={[required()]} />
                <DateInput source="biz_date" fullWidth validate={[required()]} />
                <NumberInput source="limit_c_usd" fullWidth validate={[required()]} />
                <PdRateInput />
                <InsuranceRateInput/>
            </SimpleForm>
        </Create>
    );
}