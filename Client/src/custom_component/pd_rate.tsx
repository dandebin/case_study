import { List, Datagrid, TextField, SimpleForm,TextInput,  NumberInput, Create, required} from "react-admin";


export default function PdRateInput() {
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
        <NumberInput source="pd_rate" validate={validatePdRate} label="PD Rate" defaultValue={0.36} fullWidth/>
    );
  }
  