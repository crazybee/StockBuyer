import { Alert } from "solid-bootstrap";
import { Component, Show, createSignal } from "solid-js";
import * as zod from "zod";
import { AuthenticationRequest } from "../apiclient/stockapiclient";
import { mockedApiClient, api } from "../App";
interface LoginRequest {
  username: string;
  password: string;
}
import "../assets/Login.css";
import { createStore } from "solid-js/store";

const [loggedinUser, setLoggedinUser] = createSignal<string>("");
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
const Login: Component = () => {
  const [fields, setFields] = createStore({"password":"", "username":""});
  const actionHandler = async () => {
    console.warn(fields);
    try {    
      usernameSchema.parse(fields.username);
      passwordSchema.parse(fields.password);
    } catch (e) {
      if (e instanceof zod.ZodError) {
        /* set zod errors messages*/
        setErrorMsg(e.message);
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
      console.warn(response.token);
      if (response.token) {
        setLoggedinUser(fields.username);
        settoken(response.token);
         window.location.href = "/home"
      }
      else return;
    }
  };
  return (
    <div>
      <h2>Login</h2>
      <input
        type="text"
        placeholder="username"
        onInput={(e) => {
          setFields("username", e.currentTarget.value)
        }}
        required
      />
      <input
        type="password"
        placeholder="password"
        minlength="6"
        onInput={(e) => {
          console.warn(e.currentTarget.value);
          setFields("password", e.currentTarget.value );
        }}
        required
      />
      <Show when={errorMsg() !== ""}>
        <Alert variant="warning">{errorMsg()}</Alert>
      </Show>     
      <button
        onClick={[
          actionHandler,
          {},
        ]}
      >
        LogIn
      </button>
    </div>
  );
};

export { token, loggedinUser };
export default Login;

