import Swal from "sweetalert2";
import { adminResetPassword, toggleUserStatus } from "../../services/userService";

function UserActions({ user, onSuccess }) {

    async function resetPassword() {
        const { value } = await Swal.fire({
            title: "Reset Password",
            input: "password",
            showCancelButton: true
        });

        if (!value) return;

        await adminResetPassword(user.userId, value);
        Swal.fire("Success", "Password reset", "success");
    }

    async function toggleStatus() {
        const action = user.status ? "deactivate" : "activate";

        const confirm = await Swal.fire({
            title: `${action} user?`,
            icon: "warning",
            showCancelButton: true
        });

        if (!confirm.isConfirmed) return;

        await toggleUserStatus(user.userId, action);
        Swal.fire("Success", `User ${action}d`, "success");
        onSuccess();
    }

    return (
        <div className="d-flex gap-2 justify-content-center">
            <button className="btn btn-sm btn-warning" onClick={resetPassword}>
                Reset
            </button>
            <button
                className={`btn btn-sm ${user.status ? "btn-danger" : "btn-success"}`}
                onClick={toggleStatus}
            >
                {user.status ? "Deactivate" : "Activate"}
            </button>
        </div>
    );
}

export default UserActions;
