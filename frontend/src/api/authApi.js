import axios from "axios";
const API_URL="https://localhost:7113/api/SystemCore/auth";
export const login =async (username, password)=>{
    const res = await axios.post(`${API_URL}/login`, {username, password});
    return res.data;



    
};
export const logout=async()=>{
    const res=await axios.post(`${API_URL}/logout`);
    return res.data;
};
