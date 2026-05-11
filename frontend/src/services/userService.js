const API_BASE = "https://localhost:7113/api/SystemCore";

const authHeader = () => ({
    Authorization: `Bearer ${localStorage.getItem("jwtToken")}`
});

export async function getUsers() {
    const res = await fetch(`${API_BASE}/User/getAll`, {
        headers: authHeader()
    });
    return res.json();
}

export async function createUser(payload) {
    return fetch(`${API_BASE}/User/create`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            ...authHeader()
        },
        body: JSON.stringify(payload)
    });

}

export async function toggleUserStatus(userId, action) {
    return fetch(`${API_BASE}/User/${userId}/${action}`, {
        method: "PUT",
        headers: authHeader()
    });
}

export async function adminResetPassword(userId, newPassword) {
    return fetch(`${API_BASE}/password/admin-reset?userId=${userId}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
           // ...authHeader()
        },
        body: JSON.stringify({ newPassword })
    });


}
export async function changePassword(currentPassword, newPassword, confirmPassword){
         const res=await fetch(`${API_BASE}/user/change`,{
            method: "POST",
            headers: { 
                "Content-Type":"application/json",
                "Authorization":`Bearer ${token} `
            }
        })
        return res;
    }


    export async function updateUser(userId, payload) {
  const body = {
    Name: payload.name,
    UserName: payload.username,
    Email: payload.email || null,
    MobileNo: payload.mobileNo || null,
    Role: payload.userType
  };
  const res = await fetch(`${apiUrl}/update?userId=${userId}`, {
    method: "POST", 
    headers: { "Content-Type": "application/json", Authorization: `Bearer ${token}` },
    body: JSON.stringify(body)
  });
  if (!res.ok) throw new Error("Update failed");
}