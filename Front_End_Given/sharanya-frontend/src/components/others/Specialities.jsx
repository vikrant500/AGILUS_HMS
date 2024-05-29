import React from "react";
import { ArrowRightAlt } from "@mui/icons-material";
import { Link } from "react-router-dom";

const Specialities = ({ specialities }) => {
  return (
    <div className="bg-white text-black  py-10 flex flex-col gap-4 ">
      <h3 className="text-2xl text-center font-bold">Our Specilities</h3>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-4 md:px-8 lg:px-16: padding:10px">
        {specialities.map((item, i) => (
          <div key={i} className="bg-white rounded-md p-2  flex gap-2 w-full shadow-xl">
            {item?.image && (
              <div className="flex-shrink-0 w-24 h-24 overflow-hidden rounded-md bg-red-200">
                <img
                  src={item.image}
                  className="w-50 h-50 object-cover"
                  alt="specialities"
                />
              </div>
            )}
            <div className="flex flex-col gap-2 w-full">
              <div className="flex justify-between cursor-pointer">
                <h4 className="text-xl font-bold text-orange-500">{item.title}</h4>
                <Link to={item.link}>
                  <ArrowRightAlt />
                </Link>
              </div>
              <p className="text-sm">{item.description}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Specialities;
