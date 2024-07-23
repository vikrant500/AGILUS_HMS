import { Accessible, Person, Vaccines } from "@mui/icons-material";
import React from "react";
import { Container } from "@mui/material";
const Figures = () => {
  const FIGURES = [
    {
      title: "Happy Patients",
      count: "50,000+",
      icon: <Accessible sx={{ fontSize: "48px" }} />,
    },
    {
      title: "Successful Surgeries",
      count: "10,000+",
      icon: <Vaccines sx={{ fontSize: "48px" }} />,
    },
    {
      title: "Doctors",
      count: "50+",
      icon: <Person sx={{ fontSize: "48px" }} />,
    },
  ];
  return (
    <div className="py-10 flex flex-col gap-4 bg-gradient-to-r from-grey to-grey">
      <h2 class="faq__title text-primary leading-[120%] text-[30px] xl:text-[44px] font-semibold capitalize tracking-[0.44px] text-center mb-[50px]">Sharanya Care in Numbers</h2>
      <Container>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {FIGURES.map((figure,i) => (
            <div key={i} className="flex justify-between bg-white text-orange-500 rounded-full shadow-md p-8 popup">
              <div>
                <h3 className="text-2xl font-bold">{figure.count}</h3>
                <p className="text-xl font-light text-[#2C6975]">
                  {figure.title}
                </p>
              </div>
              <div>{figure.icon}</div>
            </div>
          ))}
        </div>
      </Container>
    </div>
  );
};

export default Figures;
