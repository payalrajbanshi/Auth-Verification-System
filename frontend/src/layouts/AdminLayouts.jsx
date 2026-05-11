
import Sidebar from "../components/admin/Sidebar";
import { Outlet } from "react-router-dom";
function AdminLayout({children}){
    return (
        <div className="d-flex">
            <Sidebar/>
            <main className="flex-grow-1 p-4">
                <Outlet/>
                {children}

            </main>

        </div>
    )
}
export default AdminLayout;