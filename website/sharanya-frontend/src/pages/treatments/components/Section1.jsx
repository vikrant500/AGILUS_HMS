import React, { useEffect } from "react";
import { INFORMATION } from "../../../constants/info";
import specialitiespic from "../../../assets/images/Specialitiespic/healthcheck.jpg";
const Section1 = ({ section_1_data }) => {
  useEffect(() => {
    window.scrollTo(0, 0);
    window.Tally.loadEmbeds();
  }, []);

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 bg-gray-100 ">
      <div className="order-2 md:order-1 p-4 md:p-10 lg:p-20 flex flex-col gap-4">
        <h1 className="text-3xl md:text-4xl font-bold text-orange-500">
          {section_1_data.heading}
        </h1>
        <p className="">{section_1_data.description}</p>
        <a
          className="bg-orange-500 w-fit  text-white p-2 px-4 rounded-md"
          href="tel:"
        >
          CALL US : {INFORMATION.call_number}{" "}
        </a>

        <div className="flex flex-col gap-4">
          <p className="text-xl font-bold">
            BOOK APPOINTMENTs With Our Expert Doctors Near You
          </p>
        </div>
      </div>

      <div className="order-1 md:order-2">
      <img src={specialitiespic} alt="Health Check" className="w-full h-auto hidden xl:flex max-w-[814px] self-end" />
      </div>
    </div>
  );
};

export default Section1;
