function ResetPassword() {
    const [pin, setPin] = useState("");
    const [newPass, setNew] = useState("");
    const [confirm, setConfirm] = useState("");

    async function resetPassword() {
        if (!pin || !newPass || !confirm) return alert("All fields required");
        if (newPass !== confirm) return alert("Passwords do not match");

        const res = await fetch(
            "https://localhost:7113/api/SystemCore/password/reset",
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    Pin: pin,
                    NewPassword: newPass,
                    ConfirmPassword: confirm,
                }),
            }
        );

        const data = await res.json();
        if (res.ok) {
            alert("Password reset successful");
            window.location.href = "/login";
        } else {
            alert(data.message || "Reset failed");
        }
    }

    return (
        <div className="card p-4 mx-auto" style={{ maxWidth: 400 }}>
            <h5>Enter Reset Code</h5>

            <input className="form-control mt-2"
                placeholder="PIN"
                value={pin}
                onChange={e => setPin(e.target.value)} />

            <input className="form-control mt-2"
                type="password"
                placeholder="New Password"
                value={newPass}
                onChange={e => setNew(e.target.value)} />

            <input className="form-control mt-2"
                type="password"
                placeholder="Confirm Password"
                value={confirm}
                onChange={e => setConfirm(e.target.value)} />

            <button className="btn btn-success w-100 mt-3" onClick={resetPassword}>
                Reset Password
            </button>
        </div>
    );
}

export default ResetPassword;