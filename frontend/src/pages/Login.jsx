
import { useState } from "react";
import {login} from "../api/authApi";
import {useNavigate} from "react-router-dom";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import { ButtonComponent , CheckBoxComponent} from "@syncfusion/ej2-react-buttons";

function Login(){
    const [username, setUsername]=useState("");
    const [password, setPassword]=useState("");
    const [rememberMe, setRememberMe] = useState(false);

    const navigate=useNavigate();
    async function hanldeLogin(e){
        e.preventDefault();

        try{
            const data=await login(username, password);
            console.log(data);
            localStorage.setItem("jwtToken", data.token);
           // localStorage.setItem("userId", data.userId);
          //  localStorage.setItem("role", data.role);
        if(data.tempEmail){
          localStorage.setItem("tempEmail", data.tempEmail);
            navigate("/verify-email");
            return;
        }
        localStorage.setItem("role", data.role);
            if(data.role=="SuperAdmin"){
                navigate("/admin");
            }else{
                navigate("/user-dashboard");
            }

        } catch(err){
            alert(err.response?.data || "Login failed");
        }

    }
     return (

     <div
            className="d-flex justify-content-center align-items-center"
            style={{ minHeight: "100vh", backgroundColor: "#f8f9fa" }}
        >
            <div
                className="e-card shadow p-4"
                style={{ width: "100%", maxWidth: "400px" }}
            >
               
                <h3 className="text-center mb-1">AuthVerification</h3>
                <p className="text-center text-muted mb-4">
                    Login to your account
                </p>

                <form onSubmit={hanldeLogin}> 
                    <div className="mb-3">
                        <label className="form-label">Username</label>
                        <TextBoxComponent
                            placeholder="Username"
                            floatLabelType="Never"
                            value={username}
                            change={(e) => setUsername(e.value)}
                        />
                    </div>

                    <div className="mb-3">
                        <label className="form-label">Password</label>
                        <TextBoxComponent
                            type="password"
                            placeholder="Password"
                            floatLabelType="Never"
                            value={password}
                            change={(e) => setPassword(e.value)}
                        />
                    </div>

                    <div className="d-flex justify-content-between align-items-center mb-3">
                        <CheckBoxComponent
                            label="Remember me"
                            checked={rememberMe}
                            change={(e) => setRememberMe(e.checked)}
                        />
                        <a href="/forgot-password" className="small">
                            Forgot password?
                        </a>
                    </div>

                    
                    <ButtonComponent
                        cssClass="e-primary"
                        style={{ width: "100%" }}
                        type="submit"
                    >
                        Login
                    </ButtonComponent>
                </form>

                <div className="text-center my-4">
                    <small className="text-muted">Or continue with</small>
                </div>

                <div className="d-grid gap-2">
<ButtonComponent cssClass="e-outline d-flex align-items-center justify-content-center gap-2">
  <img
    src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/google/google-original.svg"
    alt="google"
    style={{ width: "18px", height: "18px" }}
  />
  Sign in with Google
</ButtonComponent>
           <ButtonComponent cssClass="e-outline d-flex align-items-center justify-content-center gap-2">
  <img
    src="https://upload.wikimedia.org/wikipedia/commons/4/44/Microsoft_logo.svg"
    alt="microsoft"
    style={{ width: "18px", height: "18px" }}
  />
  Sign in with Microsoft
</ButtonComponent>
                </div>

                <div className="text-center mt-4">
                    <small>
                        Don't have an account?{" "}
                        <a href="/register">Sign up</a>
                    </small>
                </div>
            </div>
        </div>     
  );
}
export default Login;