import React, { useEffect, useState } from "react";
import { Container } from "@mui/system";

import { Close } from "@mui/icons-material";
import { assets } from "../../assets";
const Doctors = () => {
  const galleryImages = [
    assets.doctors.doctor_1,
    assets.doctors.doctor_2,
    assets.doctors.doctor_3,
    assets.doctors.doctor_4,
    assets.doctors.doctor_5,
    assets.doctors.doctor_6,
    assets.doctors.doctor_7,
  ];

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const [modal, setModal] = useState(false);
  const [currentImage, setCurrentImage] = useState(assets.art_01);

  return (
    <div className="relative">
      <div className="py-20 bg-white text-black ">
        <h3 className="text-center text-3xl font-bold mb-10">Our Doctors</h3>
        <Container>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3  gap-4">
            {galleryImages.map((item, i) => (
              <img
                key={i}
                width={400}
                height={600}
                onClick={() => {
                  setCurrentImage(item);
                  setModal(true);
                }}
                alt="doctor"
                className=" bg-green-100 w-[100%] h-[500px] object-fill cursor-pointer border-2 border-orange-500 shadow-md ring-orange-500 ring-opacity-50"
                src={item}
              />
            ))}
          </div>
        </Container>
      </div>
      {modal && (
        <div
          onClick={() => {
            setModal(false);
          }}
          className="fixed  bottom-0 h-[100vh] w-[100%] backdrop-brightness-50 z-20"
        >
          <div className="w-[90%] md:w-[70%] h-[100%]  m-auto flex flex-col justify-center items-center relative ">
            <img
              width={1366}
              height={768}
              src={currentImage}
              className="w-[100%] h-[90%]  object-contain"
              alt="doctor"
            />
          </div>
          <div
            onClick={() => setModal(false)}
            className="absolute top-4 right-4 md:top-4 md:right-4 rounded-full h-12 w-12 bg-secondary flex justify-center items-center cursor-pointer hover:scale-110 duration-200"
          >
            <Close fontSize="large" sx={{ color: "white" }} />
          </div>
        </div>
      )}
    </div>
  );
};

export default Doctors;
