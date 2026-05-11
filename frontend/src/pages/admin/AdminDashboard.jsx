import React, { useEffect, useState } from "react";
import { getUsers } from "../../services/userService";
import { FaUsers, FaUserCheck, FaUserTimes } from "react-icons/fa";
import {
  DashboardLayoutComponent,
  PanelsDirective,
  PanelDirective
} from '@syncfusion/ej2-react-layouts';



function AdminDashboard() {
  const [stats, setStats] = useState({
    total: 0,
    active: 0,
    inactive: 0,
  });

  useEffect(() => {
    loadStats();
  }, []);

  async function loadStats() {
    try {
      const users = await getUsers();

      const total = users.length;
      const active = users.filter(
        (u) =>
          u.status === "Active" ||
          u.status === true ||
          u.status === "active" ||
          u.status === "1" ||
          u.status === 1 ||
          u.status === "True" ||
          u.status === "true"
      ).length;
      const inactive = total - active;

      setStats({ total, active, inactive });
    } catch (err) {
      console.error("Failed to load users", err);
    }
  }

  const totalUsersPanel = () => (
    <div className="d-flex align-items-center gap-3 p-3">
      <FaUsers size={40} className="text-primary" />
      <div>
        <h5>Total Users</h5>
        <h2 className="text-primary">{stats.total}</h2>
      </div>
    </div>
  );

  const activeUsersPanel = () => (
    <div className="d-flex align-items-center gap-3 p-3">
      <FaUserCheck size={40} className="text-success" />
      <div>
        <h5>Active Users</h5>
        <h2 className="text-success">{stats.active}</h2>
      </div>
    </div>
  );

  const inactiveUsersPanel = () => (
    <div className="d-flex align-items-center gap-3 p-3">
      <FaUserTimes size={40} className="text-danger" />
      <div>
        <h5>Inactive Users</h5>
        <h2 className="text-danger">{stats.inactive}</h2>
      </div>
    </div>
  );

  const systemOverviewPanel = () => (
    <div className="p-3">
      <h5>System Overview</h5>
      <p className="text-muted">
        Welcome to the admin dashboard. Use the sidebar to manage users,
        change passwords, and monitor system activity.
      </p>
    </div>
  );

  return (
    <div className="container-fluid">
      <h1 className="mb-4">Admin Dashboard</h1>

      <DashboardLayoutComponent
        id="dashboard_default"
        columns={6}
        cellSpacing={[10, 10]}
        allowResizing={true}
        draggableHandle=".e-panel-header"
      >
        <PanelsDirective>
          <PanelDirective
            sizeX={2}
            sizeY={1}
            row={0}
            col={0}
            content={totalUsersPanel}
            header="Total Users"
          />
          <PanelDirective
            sizeX={2}
            sizeY={1}
            row={0}
            col={2}
            content={activeUsersPanel}
            header="Active Users"
          />
          <PanelDirective
            sizeX={2}
            sizeY={1}
            row={0}
            col={4}
            content={inactiveUsersPanel}
            header="InActive Users"
          />
          <PanelDirective
            sizeX={6}
            sizeY={1}
            row={1}
            col={0}
           content={systemOverviewPanel}
            header="System Overview Panel"
          />
        </PanelsDirective>
      </DashboardLayoutComponent>
    </div>
  );
}

export default AdminDashboard;
