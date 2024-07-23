import React from "react";
const Section2 = ({section_2_data}) => {


  return (
    <div className="py-10 flex flex-col gap-4 px-4 md:px-10 lg:px-20">
      <h3 className="text-2xl font-bold">{section_2_data.title}</h3>
      <p>{section_2_data.description}</p>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        {section_2_data.list.map((item, index) => (
          <div className="border-[1px] p-4 rounded-md">
            <p className="font-bold">{item}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Section2;
