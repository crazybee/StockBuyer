import { Component, Show } from "solid-js";
import * as api from "../src/apiclient/stockapiclient";
import { Route, Routes } from "@solidjs/router";
import Login from "./Pages/Login";
const mockedApiClient: api.Client = new api.Client("https://localhost:7111");

const App: Component = () => (
  <>
    <div class="container">

      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </div>
  </>
);

export { mockedApiClient, api };
export default App;
