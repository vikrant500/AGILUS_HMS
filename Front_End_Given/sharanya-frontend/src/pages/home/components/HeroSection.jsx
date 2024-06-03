import React from "react";
import { INFORMATION } from "../../../constants/info";
import { assets } from "../../../assets";
import { RiHeartPulseFill } from "@remixicon/react";

const HeroSection = () => {
  return (
    <div class="hero bg-grey py-12 xl:pt-12 xl:pb-0">
    <div className="container mx-auto">
  {/* text and image */}
  <div className="flex flex-col xl:flex-row items-center justify-between h-full">
    
    {/* text */}
    <div className="hero__text xl:w-[48%] text-center xl:text-left">
      
      {/* badge */}
      <div className="flex items-center bg-white py-[10px] px-[20px] w-max gap-x-2 mb-[26px] rounded-full mx-auto xl:mx-0">
        <RiHeartPulseFill className="text-orange-500" />
        <div className="uppercase text-base font-medium text-[#9ab4b7] tracking-[2.24px]">Live your life</div>
      </div>
      
      {/* title */}
      <h2 className="text-primary leading-[120%] text-[30px] xl:text-[44px] font-semibold capitalize tracking-[0.44px] mb-6">Sharanya Care, Apki health ka humsaffar</h2>
      
      {/* description */}
      <p className="text-secondary text-[17px] leading-8 mb-[42px] md:max-w-xl">
        BOOK FREE APPOINTMENTs With Our Expert Doctors Near You
        <br />
        Get consultation for 50+ diseases across India
        <br />
        In-person and online consultation with experienced doctors
        <br />
        Extensive medical assistance throughout your treatment
      </p>
      
      {/* Contact Us button */}
      <button class="btn btn-lg btn-accent mx-auto xl:mx-0">Contact Us</button>
    </div>
    
    {/* image */}
    <div className="hero__img hidden xl:flex max-w-[814px] self-end">
      <img src={assets.hero} alt="Hero" />
    </div>
  </div>
</div>
</div>
  );
};

export default HeroSection;
