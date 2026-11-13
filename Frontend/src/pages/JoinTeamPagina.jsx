import "./startscherm.css";
import { useState } from "react";
import { useNavigate } from "react-router";

export default function JoinTeamPagina() {
  const [gamesessie, setGamesessie] = useState("");
  const [teamcode, setTeamcode] = useState("");
  const [foutmelding, setFoutmelding] = useState("");
  const navigate = useNavigate();

  async function joinTeam(event) {
    //zorgt ervoor dat de pagina niet herlaadt bij het submitten van het form
    event.preventDefault();

    if (!Number.isInteger(Number(gamesessie)) || teamcode.length !== 4) {
      return;
    }

    //versturen van data naar de backend
    try {
      const response = await fetch("http://localhost:5104/teams/join", {
        method: "POST",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          teamcode: teamcode,
          gamecode: Number(gamesessie),
        }),
      });

      if (!response.ok) {
        setFoutmelding("Gamesessie of teamcode klopt niet.");
      }
      navigate("/test");


    } catch (error) {
      setFoutmelding(error.message);
    }
  } 

  return (
    <div className="vakje">
      <div className="logo">
        <h1 className="gamenaam">
          Panda<br />
          <span className="titeldeel">Restaurant Sim</span>
        </h1>
      </div>
      <form onSubmit={joinTeam}>
        <div className="buttons">
          <div className="invoercontainer">
            <input
              type="text"
              className="invoer"
              placeholder="gamesessie"
              value={gamesessie}
              onChange={(event) => setGamesessie(event.target.value)}
              maxLength={5}
            />
            <input
              type="text"
              className="invoer"
              placeholder="teamcode"
              value={teamcode}
              onChange={(event) => setTeamcode(event.target.value)}
              maxLength={4}
            />
            {foutmelding && (<p style={{ color: "#ff4d4d", fontSize: "13px", textAlign: "center", margin: 0 }}>{foutmelding}</p>)}
          </div>
          <div className="verderbutton">
            <button type="submit">Join</button>
            <button type="button">Uitloggen</button>
          </div>
        </div>
      </form>
    </div>
  );
}
