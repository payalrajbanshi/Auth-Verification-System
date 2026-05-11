import { useState } from "react";
import { ButtonComponent } from "@syncfusion/ej2-react-buttons";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import '@syncfusion/ej2-base/styles/material.css';
import '@syncfusion/ej2-buttons/styles/material.css';
import '@syncfusion/ej2-inputs/styles/material.css';

function ChangePassword({ token, onForgot }) {
  const [currentPassword, setCurrent] = useState("");
  const [newPassword, setNew] = useState("");
  const [confirmPassword, setConfirm] = useState("");
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
      setConfirm("");
    } catch (err) {
      setMessage(err.message || "Failed to change password.");
    }
  }

  return (
    <div
      className="card p-4 mx-auto mt-5 shadow-sm"
      style={{ maxWidth: 400 }}
    >
      <h4 className="text-center mb-4">Change Password</h4>

      {message && (
        <div
          className={`alert ${
            message.includes("success") ? "alert-success" : "alert-danger"
          }`}
        >
          {message}
        </div>
      )}

      <TextBoxComponent
        type="password"
        placeholder="Current Password"
        floatLabelType="Always"
        value={currentPassword}
        change={(e) => setCurrent(e.value)}
        className="mb-3"
      />

      <TextBoxComponent
        type="password"
        placeholder="New Password"
        floatLabelType="Always"
        value={newPassword}
        change={(e) => setNew(e.value)}
        className="mb-3"
      />

      <TextBoxComponent
        type="password"
        placeholder="Confirm Password"
        floatLabelType="Always"
        value={confirmPassword}
        change={(e) => setConfirm(e.value)}
        className="mb-3"
      />

      <ButtonComponent
        cssClass="e-primary"
        onClick={updatePassword}
        className="w-100 mb-2"
      >
        Update Password
      </ButtonComponent>

      {/* <div className="text-center">
        <a href="#" onClick={onForgot}>
          Forgot Password?
        </a>
      </div> */}
    </div>
  );
}

export default ChangePassword;
