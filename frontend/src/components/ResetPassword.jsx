import { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import { ButtonComponent } from "@syncfusion/ej2-react-buttons";

import "@syncfusion/ej2-base/styles/material.css";
import "@syncfusion/ej2-inputs/styles/material.css";
import "@syncfusion/ej2-buttons/styles/material.css";

function ResetPassword() {
  const [pin, setPin] = useState("");
  const [newPass, setNew] = useState("");
  const [confirm, setConfirm] = useState("");
  const [message, setMessage] = useState("");
  const navigate=useNavigate();

  async function resetPassword() {
    if (!pin || !newPass || !confirm) {
      setMessage("All fields are required");
      return;
    }

    if (newPass !== confirm) {
      setMessage("Passwords do not match");
      return;
    }

    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/password/reset",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            Pin: pin,
            NewPassword: newPass,
            ConfirmPassword: confirm,
          }),
        }
      );

      const data = await res.json();
      if (!res.ok) throw data;

      setMessage("Password reset successful");
      setTimeout(() => {
        navigate(-1);
        //window.location.href = "/login";
      }, 1200);
    } catch (err) {
      setMessage(err.message || "Reset failed");
    }
  }

  return (
    <div
      className="mx-auto"
      style={{ maxWidth: 400, padding: 24 }}
    >
      <h5 className="mb-3 text-center">Enter Reset Code</h5>

         {infoMessage && (
        <div
          style={{
            marginBottom: 12,
            color: "#084298",
            background: "#e7f3ff",
            padding: 10,
            borderRadius: 4,
            textAlign: "center",
          }}
        >
          {infoMessage}
        </div>
      )}
            {message && (
        <div
          style={{
            marginBottom: 12,
            color: message.includes("successful") ? "#155724" : "#d9534f",
            background: message.includes("successful") ? "#d4edda" : "#f8d7da",
            padding: 10,
            borderRadius: 4,
            textAlign: "center",
          }}
        >
          {message}
        </div>
      )}
      
      <TextBoxComponent
        placeholder="PIN"
        floatLabelType="Always"
        cssClass="e-outline"
        value={pin}
        change={(e) => setPin(e.value)}
      />

      <br />

      <TextBoxComponent
        type="password"
        placeholder="New Password"
        floatLabelType="Always"
        cssClass="e-outline"
        value={newPass}
        change={(e) => setNew(e.value)}
      />

      <br />

      <TextBoxComponent
        type="password"
        placeholder="Confirm Password"
        floatLabelType="Always"
        cssClass="e-outline"
        value={confirm}
        change={(e) => setConfirm(e.value)}
      />

      <ButtonComponent
        cssClass="e-success"
        style={{ width: "100%", marginTop: 16 }}
        onClick={resetPassword}
      >
        Reset Password
      </ButtonComponent>
    </div>
  );
}

export default ResetPassword;
