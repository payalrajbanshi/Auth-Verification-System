import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import VerifyEmail from "./pages/VerifyEmail";
import UserDashboard from "./pages/UserDashboard";
import AdminLayout from "./layouts/AdminLayouts";
import ManageUsers from "./pages/admin/ManageUsers";
import ManageOrganizations from "./pages/admin/ManageOrganizations";
import AdminChangePassword from "./pages/admin/AdminChangePassword";
import AdminDashboard from "./pages/admin/AdminDashboard";
import AdminRoute from "./components/admin/AdminRoute";
import EditUser from "./pages/admin/EditUser";
import EditOrganization from "./pages/admin/EditOrganization";
import UserDetails from "./pages/admin/UserDetail";
import OrganizationDetails from "./pages/admin/OrganizationDetails";


function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/verify-email" element={<VerifyEmail />} />
      <Route path="/user-dashboard" element={<UserDashboard />} />

     
      <Route
        path="/admin"
        element={
          <AdminRoute>
            <AdminLayout />
          </AdminRoute>
        }
      >
        <Route index element={<Navigate to="dashboard" />} />
        <Route path="dashboard" element={<AdminDashboard />} />
        <Route path="manage-users" element={<ManageUsers />} />
        <Route path="manage-organizations" element={<ManageOrganizations />} />
        <Route path="admin-password" element={<AdminChangePassword />} />
        <Route path="manage-users/:userId" element={<UserDetails />} />
        <Route path="manage-organizations/:organizationId" element={<OrganizationDetails />} />
        <Route path="manage-users/:userId/edit" element={<EditUser />} />
        <Route path="manage-organizations/:organizationId/edit" element={<EditOrganization />} />
      </Route>
    </Routes>
  );
}

export default App;
