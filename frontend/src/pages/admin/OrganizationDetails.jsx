import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Swal from "sweetalert2";

function OrganizationDetails() {
  const { organizationId } = useParams();
  const navigate = useNavigate();
  const token = localStorage.getItem("jwtToken");

  const [organization, setOrganization] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadOrganization();
  }, [organizationId]);

  async function loadOrganization() {
    try {
      const res = await fetch(
        "https://localhost:7113/api/SystemCore/Organization/getAll",
        { headers: { Authorization: `Bearer ${token}` } }
      );

      if (!res.ok) throw new Error();

      const orgs = await res.json();
      const org = orgs.find(
        x => String(x.organizationId || x.OrganizationId) === String(organizationId)
      );

      if (!org) {
        Swal.fire("Error", "Organization not found", "error");
        return navigate(-1);
      }

      const mappedOrg = {
        organizationId: org.organizationId || org.OrganizationId,
        name: org.name || org.Name,
        email: org.email || org.Email || "-",
        mobileNo: org.mobileNo || org.MobileNo || "-",
        phoneNo: org.phoneNo || org.PhoneNo || "-",
        website: org.website || org.Website || "-",
        streetAddress: org.streetAddress || org.StreetAddress || "-",
        status: org.status === true || org.status === 1 || org.Status === 1
      };

      setOrganization(mappedOrg);
    } catch {
      Swal.fire("Error", "Unable to load organization details", "error");
    } finally {
      setLoading(false);
    }
  }

  if (loading) return <p>Loading...</p>;
  if (!organization) return null;

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between mb-3">
        <h1>Organization Details</h1>
        <button className="e-btn e-primary" onClick={() => navigate(-1)}>
          Back
        </button>
      </div>

      <div className="card shadow-sm">
        <div className="card-body">
          <p><b>Name:</b> {organization.name}</p>
          <p><b>Email:</b> {organization.email}</p>
          <p><b>Mobile:</b> {organization.mobileNo}</p>
          <p><b>Phone:</b> {organization.phoneNo}</p>
          <p><b>Website:</b> {organization.website}</p>
          <p><b>Street Address:</b> {organization.streetAddress}</p>

          <p>
            <b>Status:</b>{" "}
            <span style={{ color: organization.status ? "green" : "red" }}>
              {organization.status ? "Active" : "Inactive"}
            </span>
          </p>

          <button
            className="e-btn e-primary mt-3"
            onClick={() =>
              navigate(
                `/admin/manage-organizations/${organization.organizationId}/edit`
              )
            }
          >
            Edit Organization
          </button>
        </div>
      </div>
    </div>
  );
}

export default OrganizationDetails;
