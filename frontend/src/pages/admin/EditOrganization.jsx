import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Swal from "sweetalert2";

function EditOrganization() {
  const { organizationId } = useParams();
  const navigate = useNavigate();
  const token = localStorage.getItem("jwtToken");

  const [form, setForm] = useState({
    name: "",
    email: "",
    mobileNo: "",
    phoneNo: "",
    website: "",
    streetAddress: ""
  });

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (organizationId) loadOrganization();
  }, [organizationId]);

  async function loadOrganization() {
    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/Organization/getAll",
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );

      if (!res.ok) throw new Error("Failed to load organizations");

      const data = await res.json();

      const org = data.find(
        o => String(o.organizationId || o.OrganizationId) === String(organizationId)
      );

      if (!org) {
        Swal.fire("Error", "Organization not found", "error");
        navigate(-1);
        return;
      }

      setForm({
        name: org.name || org.Name || "",
        email: org.email || org.Email || "",
        mobileNo: org.mobileNo || org.MobileNo || "",
        phoneNo: org.phoneNo || org.PhoneNo || "",
        website: org.website || org.Website || "",
        streetAddress: org.streetAddress || org.StreetAddress || ""
      });
    } catch (err) {
      Swal.fire("Error", "Unable to load organization", "error");
    } finally {
      setLoading(false);
    }
  }

  async function submit() {
    if (!form.name) {
      return Swal.fire("Error", "Organization name is required", "error");
    }

    const payload = {
      organizationId: Number(organizationId),
      name: form.name,
      tempEmail: form.tempEmail || null,
      mobileNo: form.mobileNo || null,
      phoneNo: form.phoneNo || null,
      website: form.website || null,
      streetAddress: form.streetAddress || null
    };

    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/Organization/update",
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
        Swal.fire("Success", "Organization updated", "success");
        navigate(-1);
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
      <h1 className="mb-3">Edit Organization</h1>

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
            <label>Phone</label>
            <input
              className="e-input"
              value={form.phoneNo}
              onChange={e =>
                setForm({ ...form, phoneNo: e.target.value })
              }
            />
          </div>

          <div className="mb-3">
            <label>Website</label>
            <input
              className="e-input"
              value={form.website}
              onChange={e =>
                setForm({ ...form, website: e.target.value })
              }
            />
          </div>

          <div className="mb-3">
            <label>Street Address</label>
            <input
              className="e-input"
              value={form.streetAddress}
              onChange={e =>
                setForm({ ...form, streetAddress: e.target.value })
              }
            />
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

export default EditOrganization;
