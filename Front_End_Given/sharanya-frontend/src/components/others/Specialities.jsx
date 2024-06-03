import React, { useState } from "react";
import { Link } from "react-router-dom";

const Specialities = ({ specialities }) => {
  const [hoveredIndex, setHoveredIndex] = useState(-1);

  return (
    <div className="bg-white text-black py-10 flex flex-col gap-4">
      <h3 className="text-2xl text-center font-bold">Our Specialities</h3>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-4 md:px-8 lg:px-16">
        {specialities.map((item, i) => (
          <div
            key={i}
            className="bg-white rounded-md p-2 flex gap-2 w-full shadow-xl transition-transform duration-300 ease-in-out transform hover:scale-105 hover:bg-orange-200 hover:shadow-2xl cursor-pointer relative"
            style={{ transition: "all 0.3s cubic-bezier(0.68, -0.55, 0.265, 1.55)" }}
            onMouseEnter={() => setHoveredIndex(i)}
            onMouseLeave={() => setHoveredIndex(-1)}
          >
            <Link to={item.link} className={hoveredIndex === i ? "opacity-0 absolute inset-0 flex justify-center items-center" : "opacity-100 flex justify-center items-center"}>
              <h4 className="text-xl font-bold text-orange-500">{item.title}</h4>
            </Link>
            <div className={hoveredIndex !== i ? "opacity-0 absolute inset-0" : "opacity-100"}>
              {item?.image && (
                <div className="flex-shrink-0 w-24 h-24 overflow-hidden rounded-md bg-red-200">
                  <img
                    src={item.image}
                    className="w-50 h-50 object-cover"
                    alt="specialities"
                  />
                </div>
              )}
              <div className="flex flex-col gap-2">
                <h4 className="text-xl font-bold text-orange-500">{item.title}</h4>
                <p className="text-sm">{item.description}</p>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Specialities;
