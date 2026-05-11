import { useState } from "react";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import { ButtonComponent } from "@syncfusion/ej2-react-buttons";

import "@syncfusion/ej2-base/styles/material.css";
import "@syncfusion/ej2-inputs/styles/material.css";
import "@syncfusion/ej2-buttons/styles/material.css";
import { useNavigate } from "react-router-dom";

function ForgotPassword({ onNext }) {
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");
  const navigate=useNavigate();

  async function sendResetPin() {
    if (!email) {
      setMessage("Enter email");
      return;
    }

    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/password/forgot",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ email }),
        }
      );

      const data = await res.json();

      if (!res.ok) throw data;

      setMessage(data.message || "PIN sent");
    navigate(-1);
      onNext && onNext(data.message || "Verification code has been sent. Enter the code to reset your password.")
      
    } catch (err) {
      setMessage(err.message || "Failed");
    }
  }

  return (
    <div
      className="mx-auto"
      style={{ maxWidth: 400, padding: 24 }}
    >
      <h5 className="mb-3 text-center">Reset Password</h5>

      {message && (
        <div style={{ marginBottom: 12, color: "#d9534f" }}>
          {message}
        </div>
      )}

      <TextBoxComponent
        placeholder="Email"
        floatLabelType="Always"
        cssClass="e-outline"
        value={email}
        change={(e) => setEmail(e.value)}
      />

      <ButtonComponent
        cssClass="e-primary"
        style={{ width: "100%", marginTop: 16 }}
        onClick={sendResetPin}
      >
        Send Code
      </ButtonComponent>

      
    </div>
  );
}

export default ForgotPassword;
