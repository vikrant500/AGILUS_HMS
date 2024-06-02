import React from "react";
import { INFORMATION } from "../../../constants/info";
import { assets } from "../../../assets";

const HeroSection = () => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 bg-[#fcfaf7]">
      <div className="order-2 md:order-1 p-4 md:p-10 lg:p-20 flex flex-col gap-4">
        <h1 className="text-2xl md:text-3xl font-bold">Sharanya Care, Apki health ka humsaffar</h1>
        <a
          className="bg-orange-500 w-fit  text-white p-2 px-4 rounded-md"
          href="tel:"
        >
          CALL US : {INFORMATION.call_number}{" "}
        </a>
        <p className="text-xl font-bold">
          BOOK APPOINTMENTs With Our Expert Doctors Near You
        </p>

        <div className="flex flex-col gap-4">
          <p>Get consultation for 50+ diseases across India</p>
          <p>In-person and online consultation with experienced doctors</p>
          <p>Extensive medical assistance throughout your treatment</p>
        </div>
      </div>

      <div className="order-1 md:order-2 p-8">
        <img src={assets.hero} alt="hero" />
      </div>
    </div>
  );
};

export default HeroSection;
