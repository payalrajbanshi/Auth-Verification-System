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

function UserDetails() {
  const { userId } = useParams();
  const navigate = useNavigate();
  const token = localStorage.getItem("jwtToken");

  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadUser();
  }, [userId]);

  async function loadUser() {
    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/User/getAll",
        { headers: { Authorization: `Bearer ${token}` } }
      );

      if (!res.ok) throw new Error();

      const users = await res.json();
      const u = users.find(x => String(x.userId || x.UserId) === String(userId));

      if (!u) {
        Swal.fire("Error", "User not found", "error");
        return navigate(-1);
      }

      const mappedUser = {
        userId: u.userId || u.UserId,
        name: u.name || u.Name,
        username: u.username || u.UserName,
        email: u.email || u.Email,
        mobileNo: u.mobileNo || u.MobileNo,
        status: u.status === true || u.status === 1 || u.Status === 1,
        userType: roleMap[u.userType || u.UserType] || "-"
      };

      setUser(mappedUser);
    } catch {
      Swal.fire("Error", "Unable to load user details", "error");
    } finally {
      setLoading(false);
    }
  }

  if (loading) return <p>Loading...</p>;
  if (!user) return null;

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between mb-3">
        <h1>User Details</h1>
        <button className="e-btn e-primary" onClick={() => navigate(-1)}>
          Back
        </button>
      </div>

      <div className="card shadow-sm">
        <div className="card-body">
          <p><b>Name:</b> {user.name}</p>
          <p><b>Username:</b> {user.username}</p>
          <p><b>Email:</b> {user.email || "-"}</p>
          <p><b>Mobile:</b> {user.mobileNo || "-"}</p>

          <p>
            <b>Status:</b>{" "}
            <span style={{ color: user.status ? "green" : "red" }}>
              {user.status ? "Active" : "Inactive"}
            </span>
          </p>

          <p><b>User Type:</b> {user.userType}</p>

          <button
            className="e-btn e-primary mt-3"
            onClick={() => navigate(`/admin/manage-users/${userId}/edit`)}
          >
            Edit User
          </button>
        </div>
      </div>
    </div>
  );
}

export default UserDetails;
