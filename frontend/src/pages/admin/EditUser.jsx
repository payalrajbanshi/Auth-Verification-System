import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Swal from "sweetalert2";


const roleMap = {
  1: "Reseller",
  2: "Organization",
  3: "Branch",
  Reseller: "Reseller",
  Organization: "Organization",
  Branch: "Branch"
};

function EditUser() {
  const { userId } = useParams();
  const navigate = useNavigate();
  const token = localStorage.getItem("jwtToken");

  const [form, setForm] = useState({
    name: "",
    username: "",
    email: "",
    mobileNo: "",
    userType: ""
  });

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (userId) loadUser();
  }, [userId]);

  async function loadUser() {
    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/User/getAll",
        { headers: { Authorization: `Bearer ${token}` } }
      );

      if (!res.ok) throw new Error("Failed to load users");

      const data = await res.json();
      const user = data.find(
        u => String(u.userId || u.UserId) === String(userId)
      );

      if (!user) {
        Swal.fire("Error", "User not found", "error");
        navigate(-1);
        return;
      }

      setForm({
        name: user.name || user.Name || "",
        username: user.username || user.UserName || "",
        email: user.email || user.Email || "",
        mobileNo: user.mobileNo || user.MobileNo || "",
        userType: roleMap[user.userType || user.UserType] || ""
      });
    } catch (err) {
      Swal.fire("Error", "Unable to load user", "error");
    } finally {
      setLoading(false);
    }
  }

  async function submit() {
    if (!form.name || !form.username || !form.userType) {
      return Swal.fire("Error", "Required fields missing", "error");
    }

    const payload = {
      name: form.name,
      username: form.username,
      email: form.email || null,
      mobileNo: form.mobileNo || null,
      userType: form.userType // ✅ STRING (same as plain JS)
    };

    try {
      const res = await fetch(
        `https://localhost:7113/api/SystemCore/User/update?userId=${userId}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
          },
          body: JSON.stringify(payload)
        }
      );

      if (res.ok) {
        Swal.fire("Success", "User updated", "success");
        navigate(`/admin/manage-users/${userId}`);
      } else {
        Swal.fire("Error", "Update failed", "error");
      }
    } catch {
      Swal.fire("Error", "Update failed", "error");
    }
  }

  if (loading) return <p>Loading...</p>;

  return (
    <div className="container-fluid">
      <h1 className="mb-3">Edit User</h1>

      <div className="card shadow-sm">
        <div className="card-body">
          <div className="mb-3">
            <label>Name *</label>
            <input
              className="e-input"
              value={form.name}
              onChange={e =>
                setForm({ ...form, name: e.target.value })
              }
            />
          </div>

          <div className="mb-3">
            <label>Username *</label>
            <input
              className="e-input"
              value={form.username}
              onChange={e =>
                setForm({ ...form, username: e.target.value })
              }
            />
          </div>

          <div className="mb-3">
            <label>Email</label>
            <input
              className="e-input"
              value={form.email}
              onChange={e =>
                setForm({ ...form, email: e.target.value })
              }
            />
          </div>

          <div className="mb-3">
            <label>Mobile</label>
            <input
              className="e-input"
              value={form.mobileNo}
              onChange={e =>
                setForm({ ...form, mobileNo: e.target.value })
              }
            />
          </div>

          <div className="mb-3">
            <label>User Type *</label>
            <select
              className="e-input"
              value={form.userType}
              onChange={e =>
                setForm({ ...form, userType: e.target.value })
              }
            >
              <option value="">Select</option>
              <option value="Reseller">Reseller</option>
              <option value="Organization">Organization</option>
              <option value="Branch">Branch</option>
            </select>
          </div>

          <div className="d-flex gap-2">
            <button className="e-btn e-primary" onClick={submit}>
              Save
            </button>
            <button
              className="e-btn e-flat"
              onClick={() => navigate(-1)}
            >
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default EditUser;
