import { Component, createSignal, lazy, onMount, Show } from "solid-js";
import { Dynamic } from "solid-js/web";
import { mockedApiClient, api } from "../App";
import { ColumnItem } from "../Components/simpleTable";
import { loggedinUser, token } from "./Login";
import Navigationtab from "../Components/NavigationTab";
const SimpleTable = lazy(() => import("../Components/simpleTable"));
const columnNames = ["StockId", "StockName", "StockDescription", "Price"]
const Home: Component = () => {
  const [tableColummns, setTableColumns] = createSignal<ColumnItem[]>([]);
  const [tableData, setTableData] = createSignal<api.StockDto[]>([]);
  onMount(async () => {
    let mockedItems = await mockedApiClient.allstocks();
    if (mockedItems.length) {
      setTableData(mockedItems);
    }
    let internalColumns: ColumnItem[] = [];
    columnNames.map(n=>{
      internalColumns.push({id: n, name: n, isHyperlink: n =="StockId" ? true : false})
    })
    setTableColumns(internalColumns);
  });

  return (
    <div>
      <Show when={loggedinUser() !== "" && token() !== ""}>
              <Navigationtab />
      </Show>
      <Show when={tableData().length} fallback={<p>loading...</p>}>
        {
          <Dynamic
            component={SimpleTable}
            columns={tableColummns()}
            data={tableData()}
          />
        }
      </Show>
    </div>
  );
};

export default Home;
