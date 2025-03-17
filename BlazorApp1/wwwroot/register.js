document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("registerForm");
    console.log("Form loaded:", form);

    form.addEventListener("submit", handleFormSubmit);
});

function handleFormSubmit(event) {
    event.preventDefault();
    console.log("Form submission triggered");

    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirm-password").value;

    console.log("Username:", username);
    console.log("Email:", email);
    console.log("Password:", password);
    console.log("Confirm Password:", confirmPassword);

    let isValid = true;

    // Clear previous error messages
    document.getElementById("usernameError").textContent = "";
    document.getElementById("emailError").textContent = "";
    document.getElementById("passwordError").textContent = "";
    document.getElementById("confirmPasswordError").textContent = "";

    // Validate fields
    if (!username) {
        document.getElementById("usernameError").textContent = "Nome de Utilizador é obrigatório.";
        isValid = false;
    }

    if (!email) {
        document.getElementById("emailError").textContent = "E-mail é obrigatório.";
        isValid = false;
    }

    if (!password) {
        document.getElementById("passwordError").textContent = "Password é obrigatória.";
        isValid = false;
    }

    if (password !== confirmPassword) {
        document.getElementById("confirmPasswordError").textContent = "As senhas não coincidem.";
        isValid = false;
    }

    if (isValid) {
        console.log("✅ Formulário válido!");
        window.location.href = "/login";
    } else {
        console.log("❌ Erro: Campos obrigatórios estão vazios ou as senhas não coincidem.");
    }
}