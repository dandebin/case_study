import { Admin, Resource,EditGuesser } from 'react-admin';

import {CpList} from "./cp_list";
import { insuranceList } from './insurance_list';
import { jdeList } from './jde_list';
import { reconList } from './recon_list';
import { cpDataProvider } from './cpDataProvider';
import {CpCreate} from "./cp_add";
import { insuranceCreate } from './insurance_add';
import { jdeCreate } from './jde_add';

export const App = () => (
    <Admin dataProvider={cpDataProvider} >
        <Resource name="counterparty/list" list={CpList} edit={EditGuesser}/>
        <Resource name="counterparty/create" list={CpCreate}/>
        <Resource name="insurance/list" list={insuranceList} edit={EditGuesser}/>
        <Resource name="insurance/create" list={insuranceCreate}/>
        <Resource name="arapjde/list" list={jdeList} edit={EditGuesser}/>
        <Resource name="arapjde/create" list={jdeCreate} />
        <Resource name="reconcile/list" list={reconList} edit={EditGuesser}/>
    </Admin>
);


/*
import {
  Admin,
  Resource,
  ListGuesser,
  EditGuesser,
  ShowGuesser,
} from "react-admin";
import { dataProvider } from "./dataProvider";
import { authProvider } from "./authProvider";

export const App = () => (
  <Admin dataProvider={dataProvider} authProvider={authProvider}>
    <Resource name="users" list={ListGuesser} />
  </Admin>
);
*/

/*
import { Admin, Resource } from "react-admin";
import simpleRestProvider from 'ra-data-simple-rest';
//import { dataProvider } from './dataProvider';
import { UserList } from "./users";
import { PostList } from "./posts";

import { fetchUtils } from 'react-admin';

const httpClient = (url: string, options: fetchUtils.Options = {}) => {
    const customHeaders = (options.headers ||
        new Headers({
            Accept: 'application/json',
        })) as Headers;
    // add your own headers here
    customHeaders.set('X-Custom-Header', 'foobar');
    customHeaders.set('Access-Control-Allow-Origin', '*');
    options.headers = customHeaders;
    return fetchUtils.fetchJson(url, options);
}

const dataProvider = simpleRestProvider('https://jsonplaceholder.typicode.com/users/2', httpClient);


export const App = () => (
  <Admin dataProvider={dataProvider}>
    <Resource name="posts" list={PostList} />
    <Resource name="users" list={UserList} recordRepresentation="name" />
  </Admin>
);
*/

/*
import { dataProvider } from './dataProvider';
import { serviceDataProvider } from './serviceDataProvider';
import { Admin, Resource } from 'react-admin';
import { UserList } from "./users";
import { PostList } from "./posts";
import {CpList} from "./cps";
import {CpCreate} from "./cps";

import {combineDataProviders } from 'react-admin';
const xDataProvider = combineDataProviders( (resource) => {
  switch (resource) {
      case 'posts':
      case 'comments':
          return dataProvider;
      default:
        return serviceDataProvider;
  }
});

export const App = () => (
    <Admin dataProvider={xDataProvider} >
        <Resource name="posts" list={PostList} />
        <Resource name="users" list={UserList} />
        <Resource name="cp_mappings" list={CpCreate}/>
    </Admin>
);

*/