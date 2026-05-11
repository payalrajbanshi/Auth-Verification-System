import { useEffect, useState } from "react";
import Swal from "sweetalert2";
import { useNavigate } from "react-router-dom";
import UserGrid from "../../components/admin/UserGrid";
import CreateUserDialog from "../../components/admin/CreateUserDialog";

import {
  getUsers,
  createUser,
  toggleUserStatus,
  adminResetPassword
} from "../../services/userService";

function ManageUsers() {
  const [users, setUsers] = useState([]);
  const [showCreate, setShowCreate] = useState(false);
  const navigate=useNavigate();

  useEffect(() => {
    loadUsers();
  }, []);

  async function loadUsers() {
    const data = await getUsers();

    const mapped = data.map(u => ({
      userId: u.userId || u.UserId,
      name: u.name || u.Name,
      email: u.email || u.Email,
      mobileNo: u.mobileNo || u.MobileNo,
      status: u.status === true || u.status === "Active" || u.status==="active"|| u.status===1|| u.status==="1",
      role: u.role || u.Role
    }));

    setUsers(mapped);
  }

  async function handleCreate(payload) {
    await createUser(payload);
    Swal.fire("Success", "User created", "success");
    setShowCreate(false);
    loadUsers();
  }

  async function handleToggle(user) {
    const action = user.status ? "deactivate" : "activate";

    const confirm = await Swal.fire({
      title: `${action} user?`,
      icon: "warning",
      showCancelButton: true
    });

    if (!confirm.isConfirmed) return;

    await toggleUserStatus(user.userId, action);
    Swal.fire("Success", "Status updated", "success");
    loadUsers();
  }

  async function handleResetPassword(user) {
    const { value } = await Swal.fire({
      title: "Reset Password",
      input: "password",
      showCancelButton: true
    });

    if (!value) return;

    await adminResetPassword(user.userId, value);
    Swal.fire("Success", "Password reset", "success");
  }
  function handleEdit(userId){
    navigate(`/admin/manage-users/${userId}/edit`);
  }
  function handleDetails(userId){
    navigate(`/admin/manage-users/${userId}`);
  }
  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between mb-3">
        <h1>Users</h1>
        <button
          className="e-btn e-primary"
          onClick={() => setShowCreate(true)}
        >
          Create User
        </button>
      </div>

      <UserGrid
        users={users}
        onToggle={handleToggle}
        onResetPassword={handleResetPassword}
        onEdit={handleEdit}
        onDetails={handleDetails}
      />

      <CreateUserDialog
        visible={showCreate}
        onClose={() => setShowCreate(false)}
        onCreate={handleCreate}
      />
    </div>
  );
}

export default ManageUsers;
