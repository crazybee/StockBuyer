import { Component, Show } from "solid-js";

import Home from "./Pages/Home";
import * as api from "../src/apiclient/stockapiclient";
import { Route, Routes } from "@solidjs/router";
import Navigationtab from "./Components/NavigationTab";
import Details from "./Pages/Details";
import Login, { loggedinUser, token } from "./Pages/Login";
const mockedApiClient: api.Client = new api.Client("https://localhost:7111");

const App: Component = () => (
  <>
    <div class="container">
      <Show when={loggedinUser() !== "" && token() !== ""}>
              <Navigationtab />
      </Show>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
        <Route path="/home" element={<Home />} />
        <Route path="/details" element={<Details />} />
      </Routes>
    </div>
  </>
);

export { mockedApiClient, api };
export default App;
