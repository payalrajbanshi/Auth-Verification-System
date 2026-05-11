import {useState} from "react";
function DashboardCards({title, value}){
    return (
        <div className="col-md-3 mb-3">
            <div className="card shadow-sm">
                <div className="card-body">
                    <h6>{title}</h6>
                    <p className="mb-0">{value} </p>
                </div>

            </div>

        </div>
    );
}

export default DashboardCards;