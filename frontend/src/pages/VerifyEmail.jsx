import {useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
//import {useState} from React;
function VerifyEmail(){
    const[pin, setPin]=useState("");
    const [email, setEmail] = useState("");
const [verified, setVerified] = useState(false);

    const [newPassword, setNewPassword] = useState("");
const [confirmPassword, setConfirmPassword] = useState("");

    const[showReset, setShowReset]=useState(false);
    const[message, setMessage]=useState("");
    const[messageType, setmessageType]=useState("");

    const token=localStorage.getItem("jwtToken");
    //const userId=localStorage.getItem("userId");
    const tempEmail=localStorage.getItem("tempEmail");
    const navigate=useNavigate();
    useEffect(()=>{
        if(!tempEmail){
           alert("Unauthorized access.Please Login");
           window.location.href="/login";
        }else {
            setEmail(tempEmail);
        }
        
    },[tempEmail])
    async function verifypin(){
        if(!pin)
        {
            setmessageType("Error");
            setMessage("Please enter the pin!");
            return;
        }
        try{
            
        const res=await fetch("https://localhost:7113/api/SystemCore/verification/Email",{
            method:"POST",
            headers:{
                "Content-Type":"application/json",
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({ tempEmail, pin})
        });
        const data=await res.json();
        if(res.ok){
                    setmessageType("success");
        setMessage("Pin verified. Please reset your password");
            setShowReset(true);
        }
    
        else 
            setmessageType("error");
        setMessage(data.message||"Invalid pin")
        }catch (err){
           console.error(err);
            setmessageType("error");
            setMessage("something went wrong. Please try again.");
        }

    }
    function goToDashboard(){
        localStorage.removeItem("tempEmail");
        navigate("/user-dashboard");
    }

    // async function resetPassword(){
    //     if(!newPassword || !confirmPassword){
    //         setmessageType("error");
    //         setMessage("Please fill all the fields required");
    //         return;
    //     }
    //     if(newPassword!==confirmPassword){
    //         setmessageType("error");
    //         setMessage("Passwords doesnot match");
    //         return;
    //     }
    //     try{
    //         const res=await fetch("https://localhost:7113/api/SystemCore/user/resetPassword",
    //             {
    //                 method:"POST",
    //                 headers:{
    //                     "Content-Type":"application/json",
    //                     "Authorization": "Bearer ${token}"
    //                 },
    //                 body:JSON.stringify({
    //                     //userId:parseInt(userId),
    //                     tempEmail,
    //                     newPassword
    //                 })

    //             }
                
    //         );
    //         const data=await res.json();
    //         if(res.ok){
    //             setmessageType("success");
    //             setMessage("Password reset successfully! Redirecting...");
    //             setTimeout(()=>{
    //                 localStorage.removeItem("tempEmail");
    //                 window.location.href="/login";
    //             },2000);
    //         }else{
    //             setmessageType("error");
    //             setMessage(data.message || "Reset failed" );
    //         }

    //     }catch(err){
    //         console.error(err);
    //         setmessageType("error");
    //         setMessage("something went wrong.");
    //     }


    // }
    return(
       <div className="container mt-5" style={{ maxWidth: "400px" }}>
  <h3 className="mb-4 text-center">Verify Email</h3>

  <input
    type="email"
    className="form-control mb-3"
    placeholder="Enter Email"
    value={email}
    onChange={(e) => setEmail(e.target.value)}
    disabled={!!tempEmail}
  />

  {!verified && (
    <>
      <input
        type="text"
        className="form-control mb-3"
        placeholder="Enter PIN"
        value={pin}
        onChange={(e) => setPin(e.target.value)}
      />

      <button className="btn btn-primary w-100" onClick={verifyPin}>
        Verify Email
      </button>
    </>
  )}

  {verified && (
    <button className="btn btn-success w-100 mt-3" onClick={goToDashboard}>
      Go to Dashboard
    </button>
  )}

  {message && (
    <div
      className={`mt-3 text-center ${
        messageType === "success" ? "text-success" : "text-danger"
      }`}
    >
      {message}
    </div>
  )}
</div>

  );
}

export default VerifyEmail;