import React from "react";
import { Link } from "react-router-dom";

const WhyUs = () => {
  const WHY_US = [
    {
      title: "Sharanya Care is COVID-19 safe",
      description: "Your safety is taken care of by thermal screening, social distancing, sanitized clinics and hospital rooms, sterilized surgical equipment and mandatory PPE kits during surgery."
    },
    {
      title: "Social Distancing",
      description: "Maintaining a safe environment is crucial. Our facilities strictly adhere to social distancing guidelines to reduce the risk of viral transmission."
    },
    {
      title: "Sanitized Clinics and Hospital Rooms",
      description: "Our clinics and hospital rooms undergo rigorous sanitization procedures to ensure a clean and hygienic environment for your well-being."
    },
    {
      title: "Sterilized Surgical Equipment and Mandatory PPE Kits",
      description: "We prioritize infection control with sterilized surgical equipment and ensure the use of mandatory Personal Protective Equipment (PPE) kits during surgeries for the safety of both patients and healthcare providers."
    }
  ];

  const darkerColor = "#e8f0f1"; // Darker green color
  const lighterColor = "#e8f0f1"; // Lighter green color
  const numberColor = "#6BB2A0"; // Number color

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-8 p-8">
      <div className="-mx-8 md:mx-0 flex flex-col justify-center items-center gap-6 p-6 md:col-span-full" style={{ background: `linear-gradient(to right, ${darkerColor}, ${lighterColor})`, color: "#000000", borderRadius: "15px", boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)", marginTop: "-20px", paddingTop: "20px" }}>
      <h2 class="faq__title text-primary leading-[120%] text-[30px] xl:text-[44px] font-semibold capitalize tracking-[0.44px] text-center mb-[50px]">Why Sharanya Care</h2>
        <p className="text-center">Delivering Seamless Surgical Experience in India</p>
        <Link to={'/book-appointment'} className="bg-orange-500 text-white px-6 py-3 rounded-full shadow hover:bg-orange-600 hover:text-white transition duration-300">
          BOOK   APPOINTMENT
        </Link>
      </div>
      {WHY_US.map((item, i) => (
        <div key={i} className={`flex flex-col gap-4 justify-center items-center p-6 bg-white shadow-md rounded-lg hover:shadow-xl transition duration-300 ${i % 2 === 0 ? 'md:text-left' : 'md:text-right'}`}>
          <div className="w-full flex flex-col gap-2 text-center md:text-left">
            <h1 className="text-5xl font-bold text-orange-500">0{i + 1}</h1>
            <h4 className="text-xl font-semibold text-gray-800">
              {item.title}
            </h4>
            <p className="text-gray-600">
              {item.description}
            </p>
          </div>
        </div>
      ))}
    </div>
  );
};

export default WhyUs;
