import React from "react";
import { Link } from "react-router-dom";

const Specialities = ({ specialities }) => {
  return (
    <div className="bg-white text-black py-10 flex flex-col gap-4">
      <h3 className="text-2xl text-center font-bold">Our Specialities</h3>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-4 md:px-8 lg:px-16">
        {specialities.map((item, i) => (
          <Link to={item.link} key={i} className="w-full">
            <div
              className="bg-white rounded-md p-4 flex flex-col gap-2 w-full shadow-xl cursor-pointer"
            >
              <h4 className="text-xl font-bold text-orange-500">{item.title}</h4>
              {item?.image && (
                <div className="flex-shrink-0 w-full h-48 overflow-hidden rounded-md bg-red-200 mt-2">
                  <img
                    src={item.image}
                    className="w-full h-full object-cover"
                    alt="specialities"
                  />
                </div>
              )}
              <p className="text-sm mt-2">{item.description}</p>
              <p className="text-green-500 underline mt-2">Click here to know more</p>
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
};

export default Specialities;