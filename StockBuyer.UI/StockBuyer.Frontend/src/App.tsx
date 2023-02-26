import type { Component } from 'solid-js';

import logo from './logo.svg';
import styles from './App.module.css';
import Home from './Pages/Home';
import { Route, Routes } from '@solidjs/router';
import Navigationtab from './Components/NavigationTab';
import Details from './Pages/Details';

const App: Component = () => {
  return (
    <>
      <div class="container">
      <Navigationtab />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/details" element={<Details />} />
        </Routes>
      </div>
    </>
  );
};

export default App;
