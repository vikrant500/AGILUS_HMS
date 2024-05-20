import React from "react";
import { Link } from "react-router-dom";

const WhyUs = () => {

    const WHY_US = [
        {
            title: "Sharanya Care is COVID-19 safe",
            description: "Your safety is taken care of by thermal screening, social distancing, sanitized clinics and hospital rooms, sterilized surgical equipment and mandatory PPE kits during surgery."
        },
        {
          "title": "Social Distancing",
          "description": "Maintaining a safe environment is crucial. Our facilities strictly adhere to social distancing guidelines to reduce the risk of viral transmission."
        },
        {
          "title": "Sanitized Clinics and Hospital Rooms",
          "description": "Our clinics and hospital rooms undergo rigorous sanitization procedures to ensure a clean and hygienic environment for your well-being."
        },
        {
          "title": "Sterilized Surgical Equipment and Mandatory PPE Kits",
          "description": "We prioritize infection control with sterilized surgical equipment and ensure the use of mandatory Personal Protective Equipment (PPE) kits during surgeries for the safety of both patients and healthcare providers."
        }
    ]
  return (
    <div className="grid grid-cols-1 md:grid-cols-3">
      <div className="flex flex-col justify-center items-center gap-4 p-4 h-full">
        <h4 className="text-xl font-bold ">Why Sharanya Care</h4>
        <p>Delivering Seamless Surgical Experience in India</p>
        <Link to={'/book-appointment'} className="bg-orange-500 text-white px-4 py-2 rounded-md">
          BOOK FREE APPOINTMENT
        </Link>
      </div>
      <div className="col-span-1 md:col-span-2 bg-gray-200 grid grid-cols-1 md:grid-cols-2">
        {WHY_US.map((item,i)=>
        <div key={i} className="flex flex-col gap-4 justify-center items-center p-4">
          <div className="w-full flex flex-col gap-4">
            <h1 className="text-5xl font-bold text-orange-500">0{i+1}</h1>
            <h4 className="text-xl font-semibold">
                {item.title}
            </h4>
            <p>
             {item.description}
            </p>
          </div>
        </div>
        )}

      </div>
    </div>
  );
};

export default WhyUs;
