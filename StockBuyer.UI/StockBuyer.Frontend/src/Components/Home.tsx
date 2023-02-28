import { Component, createSignal, lazy, onMount, Show } from "solid-js";
import { Dynamic } from "solid-js/web";
import { api, mockedApiClient } from "../App";
import { ColumnItem } from "./simpleTable";
import { loggedinUser, token } from "../Pages/Login";

import "../assets/Home.css";
const SimpleTable = lazy(() => import("./simpleTable"));
const columnNames = ["stockId", "stockName", "stockDescription", "price"];
interface Props {
  user: string;
}
const Home: Component<Props> = ({ user }) => {
  const [tableColummns, setTableColumns] = createSignal<ColumnItem[]>([]);
  const [tableData, setTableData] = createSignal<api.StockDto[]>([]);
  onMount(async () => {
    mockedApiClient.token = loggedinUser.token;
    let mockedItems = await mockedApiClient.allstocks();
    if (mockedItems.length) {
      setTableData(mockedItems);
    }
    console.log(mockedItems);
    let internalColumns: ColumnItem[] = [];
    columnNames.map((n) => {
      internalColumns.push({
        id: n,
        name: n,
        isHyperlink: n == "stockId" ? true : false,
      });
    });
    setTableColumns(internalColumns);
  });

  return (
    <div>
      <Show when={user !== ""}>
        <h2>Welcome {user}!</h2>
        <Show when={tableData().length} fallback={<p>loading...</p>}>
          {
            <Dynamic
              component={SimpleTable}
              columns={tableColummns()}
              data={tableData()}
            />
          }
        </Show>
      </Show>
    </div>
  );
};

export default Home;
