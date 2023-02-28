import { Alert } from "solid-bootstrap";
import {
  Component,
  Show,
  createSignal,
  createMemo,
  createEffect,
} from "solid-js";
import * as zod from "zod";
import { mockedApiClient, api } from "../App";

import "../assets/Login.css";
import { createStore } from "solid-js/store";
import Home from "../Components/Home";
import { rowClick, setRowClick } from "../Components/simpleTable";
import Details from "./Details";
import { Dynamic } from "solid-js/web";

const [loggedinUser, setLoggedinUser] = createStore({
  username: "",
  token: "",
});
const [token, settoken] = createSignal<string>("");
const usernameSchema = zod
  .string({
    required_error: "Name is required",
    invalid_type_error: "Name must be a string",
  })
  .max(10, { message: "Must be 10 or fewer characters" });
const passwordSchema = zod
  .string({
    required_error: "Password is required",
    invalid_type_error: "Password must be a string",
  })
  .max(20, { message: "Must be 20 or fewer characters" });
const [errorMsg, setErrorMsg] = createSignal<string>("");
const [selectedStock, setSelectedStock] = createSignal<api.StockDto>();
const Login: Component = () => {
  const [fields, setFields] = createStore({ password: "", username: "" });

  const loginActionHandler = async () => {
    try {
      usernameSchema.parse(fields.username);
      passwordSchema.parse(fields.password);
    } catch (e) {
      if (e instanceof zod.ZodError) {
        /* set zod errors messages*/
        setErrorMsg(e.errors[0].message);
        return;
      }
    }
    let authRequest: api.AuthenticationRequest =
      new api.AuthenticationRequest();
    authRequest.username = fields.username;
    authRequest.password = fields.password;

    let response: api.AuthenticationResponse =
      await mockedApiClient.authenticate(authRequest);
    if (response.token) {
      setLoggedinUser("username", fields.username);
      setLoggedinUser("token", response.token);
    } else {
      return;
    }
  };

  createEffect(() => {
    try {
      usernameSchema.parse(fields.username);
      passwordSchema.parse(fields.password);
    } catch (e) {
      if (e instanceof zod.ZodError) {
        setErrorMsg(e.errors[0].message);
      }
    }
  });
  createMemo(async () => {
    if (rowClick() !== undefined) {
      let stockName: string = rowClick()?.stockName || "";
      let stockInfo: api.StockDto = await mockedApiClient.getStockByName(
        stockName
      );
      setSelectedStock(stockInfo);
      setRowClick();
    }
  });

  return (
    <div>
      <Show when={loggedinUser.username == ""}>
        <h2>Login</h2>
        <input
          type="text"
          placeholder="username"
          onInput={(e) => {
            setErrorMsg("");
            setFields("username", e.currentTarget.value);
          }}
          required
        />
        <input
          type="password"
          placeholder="password"
          minlength="6"
          onInput={(e) => {
            setErrorMsg("");
            setFields("password", e.currentTarget.value);
          }}
          required
        />
        <Show when={errorMsg() !== ""}>
          <Alert variant="warning">{errorMsg()}</Alert>
        </Show>
        <button onClick={[loginActionHandler, {}]}>LogIn</button>
      </Show>
      <Show when={loggedinUser.username !== "" && loggedinUser.token !== ""}>
        <Home user={loggedinUser.username} />
      </Show>
      <Show when={selectedStock()}>
        <Dynamic component={Details}></Dynamic>
      </Show>
    </div>
  );
};

export { token, loggedinUser, selectedStock };
export default Login;
