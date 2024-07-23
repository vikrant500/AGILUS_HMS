import React from "react";
import { Container } from "@mui/material";
import { assets } from "../../../assets";
const Treatment = () => {
  const TREATMENT = [
    { title: "adenoidectomy", image: assets.treatment.adenoidectomy },
    { title: "dental braces", image: assets.treatment.dental_braces },
    { title: "dental implant", image: assets.treatment.dental_implant },
    // {
    //   title: "diabetic foot ulcers",
    //   image: assets.treatment.diabetic_foot_ulcers,
    // },
    { title: "fess", image: assets.treatment.fess },
    { title: "gynecosmatia", image: assets.treatment.gynecosmatia },
    { title: "hair transplant", image: assets.treatment.hair_transplant },
    // { title: "laser circumcision", image: assets.treatment.laser_circumcision },
    { title: "piles", image: assets.treatment.piles },
    // { title: "rhinoplasty", image: assets.treatment.rhinoplasty },
    // { title: "sebaceous cyst", image: assets.treatment.sebaceous_cyst },
    { title: "septoplasty", image: assets.treatment.septoplasty },
    { title: "teeth aligners", image: assets.treatment.teeth_aligners },
    { title: "tonsil", image: assets.treatment.tonsil },
    { title: "typanoplasty", image: assets.treatment.typanoplasty },
  ];
  return (
    <div className="py-10 mb-10"> {/* Added mb-10 for margin bottom */}
      <Container>
        <div className="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-8 p-2 gap-4">
          {TREATMENT.map((item, i) => (
            <div key={i} className="flex flex-col items-center justify-center gap-2">
              <div className="w-20 h-20 border-[1px] overflow-hidden border-black rounded-md">
                <img src={item.image} className="w-full h-full object-cover" alt={item.title} />
              </div>
              <p className="uppercase text-xs">{item.title}</p>
            </div>
          ))}
        </div>
      </Container>
    </div>
  );
};  

export default Treatment;
