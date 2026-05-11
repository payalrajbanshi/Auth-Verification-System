import { useState } from "react";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import { ButtonComponent } from "@syncfusion/ej2-react-buttons";

import "@syncfusion/ej2-base/styles/material.css";
import "@syncfusion/ej2-inputs/styles/material.css";
import "@syncfusion/ej2-buttons/styles/material.css";

function ChangePassword({ token, onForgot, onBack}) {
  const [currentPassword, setCurrent] = useState("");
  const [newPassword, setNew] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [message, setMessage] = useState("");

  async function updatePassword() {
    if (!currentPassword || !newPassword || !confirmPassword) {
      setMessage("All fields are required");
      return;
    }

    if (newPassword !== confirmPassword) {
      setMessage("Passwords do not match");
      return;
    }

    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/user/change",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify({
            currentPassword,
            newPassword,
            confirmNewPassword: confirmPassword,
          }),
        }
      );

      const data = await res.json();
      if (!res.ok) throw data;

      setMessage("Password updated successfully");
      setCurrent("");
      setNew("");
      setConfirmPassword("");
    } catch (err) {
      setMessage(err.message || "Failed to change password.");
    }
  }

  return (
    <div
      className="mx-auto"
      style={{ maxWidth: 400, padding: 24 }}
    >
      <h5 className="text-center mb-3">Change Password</h5>

      {message && (
        <div style={{ marginBottom: 12, color: "#d9534f" }}>
          {message}
        </div>
      )}

      <TextBoxComponent
        type="password"
        placeholder="Current Password"
        floatLabelType="Always"
        cssClass="e-outline"
        value={currentPassword}
        change={(e) => setCurrent(e.value)}
      />

      <br />

      <TextBoxComponent
        type="password"
        placeholder="New Password"
        floatLabelType="Always"
        cssClass="e-outline"
        value={newPassword}
        change={(e) => setNew(e.value)}
      />

      <br />

      <TextBoxComponent
        type="password"
        placeholder="Confirm Password"
        floatLabelType="Always"
        cssClass="e-outline"
        value={confirmPassword}
        change={(e) => setConfirmPassword(e.value)}
      />

      <br />

      <ButtonComponent
        cssClass="e-primary"
        style={{ width: "100%", marginTop: 12 }}
        onClick={updatePassword}
      >
        Update Password
      </ButtonComponent>
      

      <div className="text-center" style={{ marginTop: 12 }}>
        <a href="#" onClick={onForgot}>
          Forgot Password?
        </a>
      </div>
      <ButtonComponent
       cssClass="e-outline"
       style={{ marginTop:10 }}
       onClick={onBack}
       >
        Back
      </ButtonComponent>
    </div>
  );
}

export default ChangePassword;
