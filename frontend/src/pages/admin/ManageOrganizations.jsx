import { useEffect, useState } from "react";
import Swal from "sweetalert2";
import { useNavigate } from "react-router-dom";
import OrganizationGrid from "../../components/admin/OrganizationGrid";
import CreateOrganizationDialog from "../../components/admin/CreateOrganizationDialog";

import {
  getOrganizations,
  createOrganization,
  toggleOrganizationStatus,
  updateOrganization,
  verifyOrganizationEmail
} from "../../services/organizationService";

function ManageOrganizations() {
  const [organizations, setOrganizations] = useState([]);
  const [showCreate, setShowCreate] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    loadOrganizations();
  }, []);

  async function loadOrganizations() {
    const data = await getOrganizations();

    const mapped = data.map(o => ({
      organizationId: o.organizationId || o.OrganizationId,
      code: o.code || o.Code,
      name: o.name || o.Name,
      //tempEmail: o.tempEmail || o.TempEmail,
      email: o.email || o.Email,
      mobileNo: o.mobileNo || o.MobileNo,
      status:
        o.status === true ||
        o.status === "Active" ||
        o.status === "ACTIVE" ||
        o.status === 1 ||
        o.status === "1"
    }));

    setOrganizations(mapped);
  }

  async function handleCreate(payload) {
    await createOrganization(payload);
    Swal.fire("Success", "Organization created", "success");
    setShowCreate(false);
    loadOrganizations();
  }

  async function handleToggle(org) {
    const action = org.status ? "deactivate" : "activate";

    const confirm = await Swal.fire({
      title: `${action} organization?`,
      icon: "warning",
      showCancelButton: true
    });

    if (!confirm.isConfirmed) return;

    await toggleOrganizationStatus(org.organizationId, action);
    Swal.fire("Success", "Status updated", "success");
    loadOrganizations();
  }

  async function handleVerifyEmail(org) {
    const { value: pin } = await Swal.fire({
      title: "Verify Organization Email",
      input: "text",
      inputLabel: "Enter verification PIN",
      showCancelButton: true
    });

    if (!pin) return;

    await verifyOrganizationEmail(org.organizationId, pin);
    Swal.fire("Success", "Email verified", "success");
    loadOrganizations();
  }

  function handleEdit(organizationId) {
    navigate(`/admin/manage-organizations/${organizationId}/edit`);
  }

  function handleDetails(organizationId) {
    navigate(`/admin/manage-organizations/${organizationId}`);
  }

  return (
    <div className="container-fluid">
      <div className="d-flex justify-content-between mb-3">
        <h1>Organizations</h1>
        <button
          className="e-btn e-primary"
          onClick={() => setShowCreate(true)}
        >
          Create Organization
        </button>
      </div>

      <OrganizationGrid
        organizations={organizations}
        onToggle={handleToggle}
        onVerifyEmail={handleVerifyEmail}
        onEdit={handleEdit}
        onDetails={handleDetails}
      />

      <CreateOrganizationDialog
        visible={showCreate}
        onClose={() => setShowCreate(false)}
        onCreate={handleCreate}
      />
    </div>
  );
}

export default ManageOrganizations;
