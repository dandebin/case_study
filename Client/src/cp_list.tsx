import { List, Datagrid, TextField, SimpleList, Edit,SimpleForm,ReferenceInput,TextInput } from "react-admin";

export const CpList = () => (
    <List>
        <SimpleList
          primaryText={(record) => record.abcode_number}
          secondaryText={(record) => record.sales_force_name}
          tertiaryText={(record) => record.pd_rate}
          linkType={record => record.canEdit ? "edit" : "show"}   
        />
    </List>
);

export const CpEdit = () => (
  <Edit>
    <SimpleForm>
        <TextInput source="abcode_number" />
        <TextInput source="sales_force_name" />
        <TextInput source="pd_rate" />
    </SimpleForm>
  </Edit>
);