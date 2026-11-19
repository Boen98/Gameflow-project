import { useEffect, useState } from "react";
import { Navigate, Route, Routes } from "react-router";
import { haalBestemmingOp } from "./account";

import JoinTeamPagina from "./pages/JoinTeamPagina";
import LoginPagina from "./pages/LoginPagina";
import RegistratiePagina from "./pages/RegistratiePagina";
import Testpagina from "./pages/testpagina";

function BeschermdePagina({ pad, children }) {
  const [bestemming, setBestemming] = useState(null);
  const [fout, setFout] = useState(false);

  useEffect(() => {
    let actief = true;

    haalBestemmingOp()
      .then((route) => {
        if (actief) {
          setBestemming(route);
        }
      })
      .catch(() => {
        if (actief) {
          setFout(true);
        }
      });

    return () => {
      actief = false;
    };
  }, []);

  if (fout) {
    return (
      <p role="alert">
        Aanmelding controleren lukt niet.
        Vernieuw de pagina om het opnieuw te proberen.
      </p>
    );
  }

  if (bestemming === null) {
    return <p role="status">Aanmelding controleren…</p>;
  }

  if (bestemming !== pad) {
    return <Navigate to={bestemming} replace />;
  }

  return children;
}

export default function App() {
  return (
    <Routes>
      <Route
        path="/"
        element={<LoginPagina />}
      />

      <Route
        path="/join"
        element={
          <BeschermdePagina key="join" pad="/join">
            <JoinTeamPagina />
          </BeschermdePagina>
        }
      />

      <Route
        path="/admin"
        element={
          <BeschermdePagina key="admin" pad="/admin">
            <RegistratiePagina />
          </BeschermdePagina>
        }
      />

      <Route
        path="/speler"
        element={
          <BeschermdePagina key="speler" pad="/speler">
            <JoinTeamPagina />
          </BeschermdePagina>
        }
      />

      <Route
        path="/test"
        element={<Navigate to="/speler" replace />}
      />
    </Routes>
  );
}