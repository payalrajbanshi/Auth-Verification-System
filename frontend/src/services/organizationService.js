const API_BASE = "https://localhost:7113/api/SystemCore";

const authHeader = () => ({
  Authorization: `Bearer ${localStorage.getItem("jwtToken")}`
});

export async function getOrganizations() {
  const res = await fetch(`${API_BASE}/Organization/getAll`, {
    headers: authHeader()
  });
  if (!res.ok) throw new Error("Failed to fetch organizations");
  return res.json();
}


export async function createOrganization(payload) {
  const res = await fetch(`${API_BASE}/Organization/create`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...authHeader()
    },
    body: JSON.stringify(payload)
  });

  if (!res.ok) throw new Error("Failed to create organization");
  return res.json();
}


export async function toggleOrganizationStatus(organizationId, action) {
  const res = await fetch(`${API_BASE}/Organization/${organizationId}/${action}`, {
    method: "PUT",
    headers: authHeader()
  });

  if (!res.ok) throw new Error(`Failed to ${action} organization`);
  return res.json();
}


export async function updateOrganization(organizationId, payload) {
  const res = await fetch(`${API_BASE}/Organization/update`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...authHeader()
    },
    body: JSON.stringify({ organizationId, ...payload })
  });

  if (!res.ok) throw new Error("Failed to update organization");
  return res.json();
}


export async function verifyOrganizationEmail(organizationId, pin) {
  const res = await fetch(`${API_BASE}/Organization/verify-email`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      ...authHeader()
    },
    body: JSON.stringify({ OrganizationId: organizationId, Pin: pin })
  });

  if (!res.ok) throw new Error("Failed to verify organization email");
  return res.json();
}
