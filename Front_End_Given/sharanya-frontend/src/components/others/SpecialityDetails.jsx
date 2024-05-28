import React from "react";
import { useParams } from "react-router-dom";
import { examples } from "../../pages/treatments/constant/examples";
import WhyUs from "./WhyUs";
import FAQ from "./FAQ";
import Figures from "./Figures";
import WatchOurAds from "../../pages/home/components/WatchOurAds";

const SpecialityDetails = () => {
  const { treatmentId, specialityId } = useParams();
  const treatment = examples[treatmentId];
  const speciality = treatment?.specialities.find(
    (spec) => spec.title.toLowerCase().replace(/ /g, "-") === specialityId
  );

  if (!speciality) {
    return <div>Speciality not found.</div>;
  }

  return (
    <div className="bg-white text-black py-10 flex flex-col gap-4">
      <h3 className="text-2xl text-center font-bold">{speciality.title}</h3>
      <p className="text-center">{speciality.description}</p>
      <Figures />
      <WatchOurAds />
      <WhyUs />
    </div>
  );
};

export default SpecialityDetails;
