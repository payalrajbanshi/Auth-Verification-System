import axios from 'axios'
const API_Url="";
export const changePassword=async (dto, token)=>{
    const res=await axios.post(API_Url, dto,{
        headers:{ Authentication: 'Bearer $token'},

    });
    return res.data;

};
export const forgotPassword=async (email)=>{
    const res=await axios.post('${API_Url}/ForgotPassword', {email});
        return res.data;

};

export const resetPassword=async (dto)=>{
    const res=await axios.post('${API_Url}/ResetPassword', {dto});
    return res.data;
}

export const adminResetPassword=async(dto)=>{
    const res=await axios.post('${API_Url}/AdminResetPassword',dto, {
        headers: {Authentication:'Bearer $token'},
    });
    return res.data;
}