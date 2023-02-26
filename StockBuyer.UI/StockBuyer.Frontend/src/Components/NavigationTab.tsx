
import { NavLink } from "@solidjs/router";
import { Component } from "solid-js";
type TabOptions = "List" | "Details" | "ArticalGroup";

const Navigationtab: Component = () => {
  return (
    <nav class="mt-5 mb-3">
      <NavLink href="/" class="btn btn-primary" activeClass="btn-success">
        List
      </NavLink>
      <NavLink
        href="/details"
        class="btn btn-primary"
        activeClass="btn-success"
      >
        Details
      </NavLink>
    </nav>
  );
};

export default Navigationtab;