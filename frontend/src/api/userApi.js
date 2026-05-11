import axios from "axios";
const API_Url="";
export const getallusers=async(token)=>{
    const res=await axios.get(API_Url,{
        headers:{ Authorization:'Bearer ${token}'},

        });
};
export const createUser=async (dto, token)=>{
    const res= await axios.get('${API_Url}/create',dto,{

        header:{Authorization: 'Bearer ${token}'} 
    });
};
export const activateUser=async (id, token)=>{
    const res=await axios.put('${API_Url}/${id}/activate',null,{
        headers:{Authorization:'Bearer ${token}'}
    });
    return res.data;
};

export const deactivateUser=async (id, token)=>{
    const res=await axios.put('${API_Url}/${id}/deactivate',null,{
        headers:{Authorization:'Bearer ${token}'}

    });
    return res.data;
};
 
