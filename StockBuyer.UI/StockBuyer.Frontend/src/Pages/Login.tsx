import { Alert } from "solid-bootstrap";
import { Component, createSignal } from "solid-js";
import * as zod from "zod";
import { AuthenticationRequest } from "../apiclient/stockapiclient";
import { mockedApiClient, api } from "../App";
interface LoginRequest {
  username: string;
  password: string;
}
import "../assets/Login.css";

const [token, settoken] = createSignal<string>("");
const [username, setUsername] = createSignal<string>("");
const [password, setPassword] = createSignal<string>("");
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
  const actionHandler = async (login: LoginRequest) => {
    console.warn(login);
    try {
      usernameSchema.parse(login.username);
      passwordSchema.parse(login.password);
    } catch (e) {
      if (e instanceof zod.ZodError) {
        /* map zod errors to the appropriate form fields */
        setErrorMsg(e.message);
        return;
      }
    }
    console.warn("username" + login.username);
    let authRequest: api.AuthenticationRequest =
      new api.AuthenticationRequest();
    authRequest.username = login.username;
    authRequest.password = login.password;

    let response: api.AuthenticationResponse =
      await mockedApiClient.authenticate(authRequest);
    if (response.token) {
      console.warn(response.token);
      settoken(response.token);
    }
  };
  return (
    <div>
      <h2>Login</h2>
      <input
        type="text"
        placeholder="username"
        onInput={(e) => {
          setUsername(e.currentTarget.value);
        }}
        required
      />
      <input
        type="password"
        placeholder="password"
        onInput={(e) => {
          setPassword(e.currentTarget.value);
        }}
        required
      />

      <Alert variant="warning">{errorMsg()}</Alert>
      <button
        onclick={[
          actionHandler,
          { username: username(), password: password() },
        ]}
      >
        LogIn
      </button>
    </div>
  );
};

export { token };
export default Login;
