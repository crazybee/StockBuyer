import {
  ColumnDef,
  createSolidTable,
  flexRender,
  getCoreRowModel,
} from "@tanstack/solid-table";
import { Component, createSignal, For, Show } from "solid-js";
import { api } from "../App";

export interface TableRow {
  stockName: string;
  price?: number;
  stockDescription: string;
  stockId: string;
}
export interface ColumnItem {
  name: string;
  isHyperlink?: boolean | false;
  id: string;
}

interface Props {
  columns: ColumnItem[];
  data: api.StockDto[];
}

const [rowClick, setRowClick] = createSignal<TableRow>();
const rowActionHandler = (row: TableRow) => {
  setRowClick((action) => {
    action = row;
    return action;
  });
};

function convertToInternalRows(data: api.StockDto[]): TableRow[] {
  let rowItems: TableRow[] = [];
  if (data.length) {
    data.map((r) => {
      rowItems.push({
        stockName: r.stockName || "",
        stockDescription: r.stockDescription || "",
        price: r.price,
        stockId: r.stockId || "",
      });
    });
  }
  return rowItems;
}

const SimpleTable: Component<Props> = ({ columns, data }) => {
  const [internaldata, setInternaldata] = createSignal<TableRow[]>([]);
  const [internalColumnData, setInternalColumnData] = createSignal<
    ColumnDef<TableRow>[]
  >([]);
  let initialData: TableRow[] = convertToInternalRows(data);
  let initialColumns: ColumnDef<TableRow>[] = [];
  columns.forEach((columnitem) => {
    console.log("accessKey" + columnitem.name);
    initialColumns.push({
      accessorKey: columnitem.name,
      id: columnitem.name,
      enableGlobalFilter: true,
      header: () => (
        <span>
          {columnitem.name
            ? columnitem.name.charAt(0).toUpperCase() +
              columnitem.name.substr(1).toLowerCase()
            : ""}
        </span>
      ),
      cell: (info) => (
        <div>
          {columnitem.isHyperlink ? (
            <a
              href="#"
              style="cursor: pointer;"
              onClick={[rowActionHandler, info.row.original]}
            >
              {info.getValue<string>()}
            </a>
          ) : (
            <i>{info.getValue<string>()}</i>
          )}
        </div>
      ),
    });
  });
  setInternaldata(initialData);
  setInternalColumnData(initialColumns);

  const table = createSolidTable({
    get data() {
      return internaldata();
    },
    get columns() {
      return internalColumnData();
    },
    getCoreRowModel: getCoreRowModel(),
  });
  return (
    <>
      <div>
        <div class="table">
          <table>
            <thead>
              <For each={table.getHeaderGroups()}>
                {(headerGroup) => (
                  <tr>
                    <For each={headerGroup.headers}>
                      {(header) => (
                        <th colSpan={header.colSpan}>
                          <Show when={!header.isPlaceholder}>
                            <div>
                              {flexRender(
                                header.column.columnDef.header,
                                header.getContext()
                              )}
                            </div>
                          </Show>
                        </th>
                      )}
                    </For>
                  </tr>
                )}
              </For>
            </thead>
            <tbody>
              <For each={table.getRowModel().rows}>
                {(row) => (
                  <tr>
                    <For each={row.getAllCells()}>
                      {(cell) => (
                        <td>
                          {flexRender(
                            cell.column.columnDef.cell,
                            cell.getContext()
                          )}
                        </td>
                      )}
                    </For>
                  </tr>
                )}
              </For>
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
};

export { setRowClick, rowClick };
export default SimpleTable;
