import "./gamesessie.css";
import { useState } from "react";
import { apiUrl } from "../account";

export default function GameSessieAanmakenPagina() {
    const [aantalRondes, setAantalRondes] = useState(1);
    const [foutmelding, setFoutmelding] = useState("");
    const [succesmelding, setSuccesmelding] = useState("");

    async function maakGameSessie(event) {
        event.preventDefault();

        setFoutmelding("");
        setSuccesmelding("");

        const formulier = new FormData(event.currentTarget);

        try {
            const startdatums = formulier
                .getAll("startdatum")
                .map(datum => new Date(datum).toISOString());

            const gegevens = {
                startSituatieTijdInMinuten: Number(
                    formulier.get("startSituatieTijd")
                ),
                aantalIngesteldeRondes: aantalRondes,
                tijdVoorRondesInMinuten: Number(
                    formulier.get("tijdVoorRondes")
                ),
                ingesteldeAantalTeams: Number(
                    formulier.get("aantalTeams")
                ),
                ingesteldeAantalSpelersPerTeam: Number(
                    formulier.get("aantalSpelers")
                ),
                startdatums: startdatums
            };

            const antwoord = await fetch(apiUrl + "/gamesessies", {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(gegevens)
            });

            if (!antwoord.ok) {
                const fout = await antwoord.json().catch(() => null);

                setFoutmelding(
                    fout?.message ||
                    Object.values(fout?.errors ?? {}).flat().join(" ") ||
                    "De gamesessie kon niet worden aangemaakt."
                );

                return;
            }

            setSuccesmelding("De gamesessie is aangemaakt.");

        } catch (fout) {
            if (fout instanceof RangeError) {
                setFoutmelding("Een van de startdatums is ongeldig.");
            } else {
                setFoutmelding("Verbinding met de server mislukt.");
            }
        }
    }

    return (
        <main className="gamesessie-pagina">
            <h1>Gamesessie aanmaken</h1>

            <form
                className="gamesessie-formulier"
                onSubmit={maakGameSessie}
            >
                <div>
                    <label htmlFor="startSituatieTijd">
                        Startsituatietijd (minuten)
                    </label>

                    <input
                        id="startSituatieTijd"
                        name="startSituatieTijd"
                        type="number"
                        min="0"
                        max="60"
                        defaultValue="10"
                        required
                    />
                </div>

                <div>
                    <label htmlFor="aantalRondes">
                        Aantal rondes
                    </label>

                    <select
                        id="aantalRondes"
                        value={aantalRondes}
                        onChange={event =>
                            setAantalRondes(Number(event.target.value))
                        }
                    >
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                    </select>
                </div>

                <div>
                    <label htmlFor="tijdVoorRondes">
                        Tijd per ronde (minuten)
                    </label>

                    <input
                        id="tijdVoorRondes"
                        name="tijdVoorRondes"
                        type="number"
                        min="0"
                        max="60"
                        defaultValue="30"
                        required
                    />
                </div>

                <div>
                    <label htmlFor="aantalTeams">
                        Aantal teams
                    </label>

                    <input
                        id="aantalTeams"
                        name="aantalTeams"
                        type="number"
                        min="1"
                        defaultValue="2"
                        required
                    />
                </div>

                <div>
                    <label htmlFor="aantalSpelers">
                        Aantal spelers per team
                    </label>

                    <input
                        id="aantalSpelers"
                        name="aantalSpelers"
                        type="number"
                        min="1"
                        defaultValue="3"
                        required
                    />
                </div>

                <h2>Startmomenten per ronde</h2>

                {Array.from({ length: aantalRondes }, (_, index) => (
                    <div
                        key={index}
                        className="ronde-instellingen"
                    >
                        <label htmlFor={`ronde-${index + 1}`}>
                            Startmoment ronde {index + 1}
                        </label>

                        <input
                            id={`ronde-${index + 1}`}
                            name="startdatum"
                            type="datetime-local"
                            required
                        />
                    </div>
                ))}

                <button type="submit">
                    Gamesessie aanmaken
                </button>

                {foutmelding && (
                    <p className="foutmelding" role="alert">
                        {foutmelding}
                    </p>
                )}

                {succesmelding && (
                    <p className="succesmelding" role="status">
                        {succesmelding}
                    </p>
                )}
            </form>
        </main>
    );
}