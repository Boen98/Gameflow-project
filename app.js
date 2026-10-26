// Panda Restaurant - frontend prototype, geen echte authenticatie.
// De backend moet later gebruikersgegevens veilig controleren.

const form = document.querySelector("#loginForm");
const emailInput = document.querySelector("#email");
const passwordInput = document.querySelector("#password");
const emailError = document.querySelector("#emailError");
const passwordError = document.querySelector("#passwordError");
const statusBox = document.querySelector("#formStatus");
const passwordToggle = document.querySelector("#togglePassword");

function setError(input, errorElement, message) {
  errorElement.textContent = message;
  input.setAttribute("aria-invalid", String(Boolean(message)));
}

function validateEmail() {
  const value = emailInput.value.trim();

  if (!value) {
    setError(emailInput, emailError, "Vul je e-mailadres in.");
    return false;
  }
  if (!emailInput.validity.valid) {
    setError(emailInput, emailError, "Vul een geldig e-mailadres in.");
    return false;
  }

  setError(emailInput, emailError, "");
  return true;
}

function validatePassword() {
  if (!passwordInput.value) {
    setError(passwordInput, passwordError, "Vul je wachtwoord in.");
    return false;
  }
  setError(passwordInput, passwordError, "");
  return true;
}

function showStatus(message, kind) {
  statusBox.hidden = false;
  statusBox.textContent = message;
  statusBox.className = `form-status form-status--${kind}`;
}

emailInput.addEventListener("input", () => {
  if (emailError.textContent) validateEmail();
  statusBox.hidden = true;
});
passwordInput.addEventListener("input", () => {
  if (passwordError.textContent) validatePassword();
  statusBox.hidden = true;
});

passwordToggle.addEventListener("click", () => {
  const isCurrentlyHidden = passwordInput.type === "password";
  passwordInput.type = isCurrentlyHidden ? "text" : "password";
  passwordToggle.textContent = isCurrentlyHidden ? "Verbergen" : "Tonen";
  passwordToggle.setAttribute("aria-label", isCurrentlyHidden ? "Wachtwoord verbergen" : "Wachtwoord tonen");
  passwordToggle.setAttribute("aria-pressed", String(isCurrentlyHidden));
});

form.addEventListener("submit", (event) => {
  event.preventDefault(); // Voorkom dat de browser de pagina opnieuw laadt.

  const validEmail = validateEmail();
  const validPassword = validatePassword();

  if (!validEmail || !validPassword) {
    showStatus("Controleer de invoervelden en probeer opnieuw.", "error");
    (validEmail ? passwordInput : emailInput).focus();
    return;
  }

  // TODO: Koppel dit later aan POST /api/auth/login.
  // Verwerk een echte login pas na een succesvolle backend-respons.
  showStatus(
    "Het formulier is correct ingevuld. Dit is een frontend-demo; inloggen via de API volgt later.",
    "success"
  );
});
