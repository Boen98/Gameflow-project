import { useState } from "react";
import { useNavigate } from "react-router";
import { apiUrl, haalBestemmingOp } from "../account";
import "./login.css";

export default function LoginPagina() {
  const navigate = useNavigate();
  const [zichtbaar, setZichtbaar] = useState(false);
  const [bezig, setBezig] = useState(false);
  const [fout, setFout] = useState("");

  async function inloggen(event) {
    event.preventDefault();

    const formulier = new FormData(event.currentTarget);
    setFout("");
    setBezig(true);

    try {
      const antwoord = await fetch(apiUrl + "/auth/login?useCookies=true", {
        method: "POST",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: formulier.get("email").trim(),
          password: formulier.get("password"),
        }),
      });

      if (!antwoord.ok) {
        setFout("Inloggen lukt niet. Controleer je gegevens.");
        return;
      }

      const bestemming = await haalBestemmingOp();

      if (bestemming === "/") {
        setFout("De browser kon je aanmelding niet herstellen.");
        return;
      }

      navigate(bestemming, { replace: true });
    } catch {
      setFout("De aanmelding kon niet worden gecontroleerd. Probeer opnieuw.");
    } finally {
      setBezig(false);
    }
  }

  return (
    <main className="layout login-view">
      <section className="intro" aria-labelledby="intro-title">
        <div className="brand brand--light">
          <span className="brand__icon" aria-hidden="true">🐼</span>
          <span>Panda Restaurant</span>
        </div>

        <div className="intro__content">
          <p className="eyebrow">GAMEFLOW · BUSINESS SIMULATION</p>

          <h1 id="intro-title">
            Jij maakt de keuzes.
            <br />
            Het restaurant reageert.
          </h1>

          <p className="intro__description">
            Ontdek hoe prijs, personeel en marketing invloed hebben op
            de resultaten van jouw restaurant.
          </p>

          <div
            className="highlights"
            aria-label="Onderdelen van de simulatie"
          >
            <span>Strategische keuzes</span>
            <span>Meerdere rondes</span>
            <span>KPI's en resultaten</span>
          </div>
        </div>

        <p className="intro__footer">
          HBO-ICT · Website Project · Prototype
        </p>
      </section>

      <section className="login-area" aria-labelledby="login-title">
        <div className="login-card">
          <div className="brand brand--small">
            <span className="brand__icon" aria-hidden="true">🐼</span>
            <span>Panda Restaurant</span>
          </div>

          <p className="eyebrow eyebrow--green">WELKOM TERUG</p>
          <h2 id="login-title">Inloggen</h2>
          <p className="subtitle">
            Log in om jouw simulatie te bekijken.
          </p>

          <form onSubmit={inloggen}>
            <div className="field">
              <label htmlFor="email">E-mailadres</label>
              <input
                id="email"
                name="email"
                type="email"
                placeholder="naam@voorbeeld.nl"
                autoComplete="username"
                inputMode="email"
                required
              />
            </div>

            <div className="field">
              <label htmlFor="password">Wachtwoord</label>

              <div className="password-control">
                <input
                  id="password"
                  name="password"
                  type={zichtbaar ? "text" : "password"}
                  placeholder="Vul je wachtwoord in"
                  autoComplete="current-password"
                  required
                />

                <button
                  className="password-toggle"
                  type="button"
                  id="togglePassword"
                  aria-label={
                    zichtbaar
                      ? "Wachtwoord verbergen"
                      : "Wachtwoord tonen"
                  }
                  aria-pressed={zichtbaar}
                  onClick={() => setZichtbaar(!zichtbaar)}
                  aria-controls="password"
                >
                  {zichtbaar ? "Verbergen" : "Tonen"}
                </button>
              </div>
            </div>

            <button
              className="submit-button"
              type="submit"
              disabled={bezig}
            >
              {bezig ? "Bezig…" : "Inloggen"}
              <span aria-hidden="true">→</span>
            </button>

            {fout && (
              <div
                className="form-status form-status--error"
                role="alert"
              >
                {fout}
              </div>
            )}
          </form>

          <p className="login-note">
            Voor spelers en spelleiders.
          </p>
        </div>
      </section>
    </main>
  );
}