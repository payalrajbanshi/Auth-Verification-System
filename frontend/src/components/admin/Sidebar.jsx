import React from "react";
import { NavLink, useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import {
  FaTachometerAlt,
  FaUsers,
  FaKey,
  FaBuilding,
  FaSignOutAlt
} from "react-icons/fa";

function Sidebar() {
  const navigate = useNavigate();

  const linkStyle = ({ isActive }) => ({
    display: "flex",
    alignItems: "center",
    gap: "10px",
    padding: "10px 12px",
    borderRadius: "6px",
    color: "#fff",
    textDecoration: "none",
    background: isActive ? "#1f2a40" : "transparent",
    fontWeight: isActive ? "600" : "400",
    marginBottom: "4px"
  });

  const handleLogout=async ()=>{
    const result=await Swal.fire({
      title: "Are you sure?",
      text: "Do you really want to logout?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, logout",
      cancelButtonText: "Cancel"
    });
     if (result.isConfirmed) {
      localStorage.clear();
      navigate("/");
    }
  }

  return (
    <div
      className="sidebar"
      style={{
        width: "250px",
        background: "#293852",
        color: "#fff",
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column"
      }}
    >

      <div style={{ padding: "20px", fontSize: "18px", fontWeight: "600" }}>
        Admin Panel
      </div>

      <nav style={{ flex: 1, padding: "0 10px" }}>
        <NavLink to="/admin/dashboard" style={linkStyle}>
          <FaTachometerAlt /> Dashboard
        </NavLink>

        <NavLink to="/admin/manage-users" style={linkStyle}>
          <FaUsers /> Users
        </NavLink>
        <NavLink to="/admin/manage-organizations" style={linkStyle}>
          <FaBuilding/> Organizations
        </NavLink>

        <NavLink to="/admin/admin-password" style={linkStyle}>
          <FaKey /> Change Password
        </NavLink>
      </nav>

      <div style={{ padding: "15px" }}>
        <button
          onClick={handleLogout}
          style={{
            width: "100%",
            background: "transparent",
            border: "1px solid #ff6b6b",
            color: "#ff6b6b",
            padding: "8px",
            borderRadius: "6px",
            cursor: "pointer",
            display: "flex",
            alignItems: "center",
            gap: "10px",
            justifyContent: "center"
          }}
        >
          <FaSignOutAlt /> Logout
        </button>
      </div>
    </div>
  );
}

export default Sidebar;
