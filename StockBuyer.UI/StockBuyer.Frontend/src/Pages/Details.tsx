import {
  Component,
  createEffect,
  createMemo,
  createSignal,
  onMount,
  Show,
} from "solid-js";
import { api, mockedApiClient } from "../App";
import { loggedinUser, selectedStock } from "./Login";
import * as zod from "zod";
import { Alert, Button } from "solid-bootstrap";

const Details: Component = () => {
  const [remainingCash, setRemainingCash] = createSignal<number | undefined>(0);
  const [buyInAmount, setBuyInAmount] = createSignal<number>(0);
  const [sellOutAmount, setSellOutAmount] = createSignal<number>(0);
  const [error, setError] = createSignal<string | undefined>("");

  onMount(async () => {
    let user: api.UserDto = await mockedApiClient.getUserByName(
      loggedinUser.username
    );
    if (user) {
      setRemainingCash(user.availableMoney);
    }
  });

  const stockAmountSchema = zod
    .number({
      required_error: "Amount is required",
      invalid_type_error: "Amount must be an integer number",
    })
    .int({ message: "must to be an integer" })
    .max(1000, { message: "single buy cannot exceed 1000" })
    .min(1, { message: "at least by 1 stock" });
  createEffect(() => {
    try {
      stockAmountSchema.parse(buyInAmount());
      stockAmountSchema.parse(sellOutAmount());
    } catch (error) {
      if (error instanceof zod.ZodError) {
        setError(error.errors[0].message);
      }
    }
  });
  const buyInHandler = async () => {
    mockedApiClient.token = loggedinUser.token;
    let buyResult: api.StockOperationResponse =
      await mockedApiClient.buyStockByName(
        selectedStock()?.stockName,
        buyInAmount()
      );
    let user: api.UserDto = await mockedApiClient.getUserByName(
      loggedinUser.username
    );
    if (user) {
      setRemainingCash(user.availableMoney);
    }

    setError(buyResult.isSuccess ? "successful" : buyResult.reason);
  };
  const sellOutHandler = async () => {
    mockedApiClient.token = loggedinUser.token;
    let sellResult: api.StockOperationResponse =
      await mockedApiClient.sellStockByName(
        selectedStock()?.stockName,
        sellOutAmount()
      );
    let user: api.UserDto = await mockedApiClient.getUserByName(
      loggedinUser.username
    );
    if (user) {
      setRemainingCash(user.availableMoney);
    }

    setError(sellResult.isSuccess ? "successful" : sellResult.reason);
  };
  return (
    <>
      <div>
        <h2>Details of {selectedStock()?.stockName}</h2>
        <label>Stock Id:</label>
        <div>{selectedStock()?.stockId} </div>
        <label>Stock Price:</label>
        <div>{selectedStock()?.price} $</div>
        <label>Details:</label>
        <div>{selectedStock()?.details} </div>
      </div>
      <h3>You have {Number(remainingCash()?.toFixed(4))} dollar</h3>
      <button class="btn btn-primary" onClick={[buyInHandler, {}]}>
        Buy
      </button>
      <input
        type="number"
        min="1"
        max="1000"
        onInput={(e) => {
          setError("");
          setBuyInAmount(parseInt(e.currentTarget.value, 10));
        }}
        required
      />
      <button class="btn btn-primary" onClick={[sellOutHandler, {}]}>
        Sell
      </button>
      <input
        type="number"
        min="1"
        max="1000"
        onInput={(e) => {
          setError("");
          setSellOutAmount(parseInt(e.currentTarget.value, 10));
        }}
        required
      />
      <Show when={error() !== ""}>
        <Alert variant="warning">{error()}</Alert>
      </Show>
    </>
  );
};

export default Details;
