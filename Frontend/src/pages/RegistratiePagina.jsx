import { useRef, useState } from "react";
import { useNavigate } from "react-router";
import { apiUrl } from "../account";
import "./registratie.css";

export default function RegistratiePagina() {
  const navigate = useNavigate();

  const aanvraagBezig = useRef(false);

  const [bezig, setBezig] = useState(false);
  const [zichtbaar, setZichtbaar] = useState(false);
  const [fout, setFout] = useState("");
  const [succes, setSucces] = useState("");

  async function registreer(event) {
    event.preventDefault();

    if (aanvraagBezig.current) {
      return;
    }

    const formulier = event.currentTarget;
    const gegevens = new FormData(formulier);

    aanvraagBezig.current = true;

    setBezig(true);
    setFout("");
    setSucces("");

    try {
      const antwoord = await fetch(apiUrl + "/admin/spelers", {
        method: "POST",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: gegevens.get("email").trim(),
          password: gegevens.get("password"),
        }),
      });

      if (antwoord.status === 401) {
        navigate("/", { replace: true });
        return;
      }

      if (antwoord.status === 403) {
        setFout(
          "Alleen een beheerder mag spelersaccounts aanmaken."
        );
        return;
      }

      const resultaat = await antwoord.json();

      if (!antwoord.ok) {
        const meldingen = Object.values(
          resultaat.errors ?? {}
        ).flat();

        setFout(
          meldingen.join(" ") ||
          resultaat.detail ||
          "Account aanmaken mislukt."
        );

        return;
      }

      formulier.reset();
      setZichtbaar(false);
      setSucces(resultaat.bericht);
    } catch {
      setFout(
        "Geen bevestiging ontvangen. Controleer de verbinding. " +
        "Als het account al is opgeslagen, kun je met die gegevens inloggen."
      );
    } finally {
      aanvraagBezig.current = false;
      setBezig(false);
    }
  }

  return (
    <div className="registratie-pagina">
      <header className="registratie-kop">
        <h1>Panda Restaurant Sim</h1>
      </header>

      <main>
        <div className="registratie-container">
          <header className="registratie-intro">
            <h2>Nieuw spelersaccount</h2>

            <p>
              Maak een account aan waarmee een speler kan inloggen.
            </p>
          </header>

          <form onSubmit={registreer}>
            <div className="registratie-kaart">
              <fieldset disabled={bezig}>
                <legend>Inloggegevens</legend>

                <div className="registratie-veld">
                  <label htmlFor="speler-email">
                    E-mailadres <span>*</span>
                  </label>

                  <input
                    id="speler-email"
                    name="email"
                    type="email"
                    placeholder="naam@voorbeeld.nl"
                    autoComplete="off"
                    required
                  />
                </div>

                <div className="registratie-veld">
                  <label htmlFor="speler-wachtwoord">
                    Wachtwoord <span>*</span>
                  </label>

                  <div className="registratie-wachtwoord">
                    <input
                      id="speler-wachtwoord"
                      name="password"
                      type={zichtbaar ? "text" : "password"}
                      placeholder="Maak een wachtwoord aan"
                      autoComplete="new-password"
                      aria-describedby="wachtwoordhulp"
                      required
                    />

                    <button
                      className="registratie-tonen"
                      type="button"
                      aria-controls="speler-wachtwoord"
                      aria-pressed={zichtbaar}
                      aria-label={
                        zichtbaar
                          ? "Wachtwoord verbergen"
                          : "Wachtwoord tonen"
                      }
                      onClick={() => setZichtbaar(!zichtbaar)}
                    >
                      {zichtbaar ? "Verberg" : "Toon"}
                    </button>
                  </div>

                  <small id="wachtwoordhulp">
                    Gebruik een hoofdletter, kleine letter, cijfer
                    en speciaal teken. De server controleert of
                    het wachtwoord voldoet.
                  </small>
                </div>
              </fieldset>
            </div>

            {fout && (
              <p
                className="registratie-status registratie-fout"
                role="alert"
              >
                {fout}
              </p>
            )}

            {succes && (
              <p
                className="registratie-status registratie-succes"
                role="status"
              >
                {succes}
              </p>
            )}

            <div className="registratie-acties">
              <button
                className="registratie-opslaan"
                type="submit"
                disabled={bezig}
              >
                {bezig ? "Opslaan…" : "Account aanmaken"}
              </button>
            </div>
          </form>
        </div>
      </main>
    </div>
  );
}