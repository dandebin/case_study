import { List, Datagrid, TextField, SimpleForm,TextInput,  NumberInput, Create, required} from "react-admin";


export default function InsuranceRateInput() {
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
        <NumberInput source="insurance_rate" validate={validatePdRate} label="Insurance Rate" defaultValue={0.9} fullWidth/>
    );
  }
  