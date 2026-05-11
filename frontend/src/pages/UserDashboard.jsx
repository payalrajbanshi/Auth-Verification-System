import { useEffect, useState } from "react";
import ChangePassword from "../components/ChangePassword";
import ForgotPassword from "../components/ForgotPassword";
import ResetPassword from "../components/ResetPassword";

import { ButtonComponent } from "@syncfusion/ej2-react-buttons";
import "@syncfusion/ej2-buttons/styles/material.css";

function UserDashboard() {
  const [user, setUser] = useState(null);
  const [view, setView] = useState("dashboard");

  const token = localStorage.getItem("jwtToken");
  const tempEmail = localStorage.getItem("tempEmail");

  useEffect(() => {
    if (!token) {
      alert("Please login first");
      window.location.href = "/";
      return;
    }

   
    if (tempEmail) {
      window.location.href = "/verify-email";
      return;
    }

    loadDashboard();
  }, [token, tempEmail]);

  async function loadDashboard() {
    try {
      const dashboardResp = await fetch(
        "https://localhost:7113/api/SystemCore/dashboard/get",
        { headers: { Authorization: `Bearer ${token}` } }
      );

      if (!dashboardResp.ok) throw new Error("Failed to load dashboard");

      const dashboardData = await dashboardResp.json();
      setUser(dashboardData);
    } catch (err) {
      console.error(err);
      alert("Session expired. Please login again.");
      localStorage.clear();
      window.location.href = "/";
    }
  }

  function logout() {
    localStorage.clear();
    window.location.href = "/";
  }

  return (
    <div style={{ padding: 24, maxWidth: 1000, margin: "0 auto" }}>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: 24,
        }}
      >
        <h4 style={{ margin: 0 }}>Welcome, {user?.username || "User"}</h4>

        <div style={{ display: "flex", gap: 8 }}>
          <ButtonComponent
            cssClass="e-outline"
            onClick={() => setView("change")}
          >
            Change Password
          </ButtonComponent>
          <ButtonComponent
            cssClass="e-outline e-danger"
            onClick={logout}
          >
            Logout
          </ButtonComponent>
        </div>
      </div>

      {view === "dashboard" && user && (
        <div style={{ display: "flex", gap: 16, flexWrap: "wrap" }}>
          {[
            { label: "Username", value: user.username, color: "#e0f7fa" },
            { label: "Role", value: user.role, color: "#fff3e0" },
            { label: "Profile Status", value: "Active", color: "#e8f5e9" },
            {
              label: "Last Login",
              value: new Date().toLocaleDateString(),
              color: "#f3e5f5",
            },
          ].map((item, idx) => (
            <div
              key={idx}
              style={{
                flex: "1 1 200px",
                background: item.color,
                borderRadius: 8,
                boxShadow: "0 2px 6px rgba(0,0,0,0.1)",
                padding: 16,
                textAlign: "center",
              }}
            >
              <div style={{ fontSize: 12, fontWeight: 600, marginBottom: 8 }}>
                {item.label}
              </div>
              <div style={{ fontSize: 18, fontWeight: 700 }}>{item.value}</div>
            </div>
          ))}
        </div>
      )}

      <div style={{ marginTop: 32 }}>
        {view === "change" && (
          <ChangePassword
            token={token}
            onForgot={() => setView("forgot")}
            onBack={() => setView("dashboard")}
          />
        )}
        {view === "forgot" && <ForgotPassword onNext={() => setView("reset")} />}
        {view === "reset" && <ResetPassword />}
      </div>
    </div>
  );
}

export default UserDashboard;
