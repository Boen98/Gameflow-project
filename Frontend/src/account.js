export const apiUrl = "http://localhost:5104";

export async function haalBestemmingOp() {
  const antwoord = await fetch(apiUrl + "/auth/me", {
    credentials: "include",
    cache: "no-store",
  });

  if (antwoord.status === 401) return "/";
  if (!antwoord.ok) throw new Error("Accountgegevens ophalen mislukt.");

  const { isAdmin, isSpeler } = await antwoord.json();

  if (isAdmin) return "/admin";
  return isSpeler ? "/speler" : "/join";
}