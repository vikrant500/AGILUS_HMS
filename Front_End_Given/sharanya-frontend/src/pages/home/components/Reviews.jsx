import React from "react";
import Rating from "@mui/material/Rating";
import Divider from "@mui/material/Divider";
const Reviews = () => {
  const reviews = [
    {
      name: "Shashi Sharma",
      city: "Delhi",
      rating: 5,
      review:
        "Very good experience. All the doctors are very experienced. Fully satisfied with dental treatment RCT and capping",
    },
    {
      name: "Khushbu Bhansali",
      city: "Delhi",
      rating: 5,
      review:
        "Very nice clinic. Doctors are good and cooperative. We would recommend to choose this clinic for their treatment",
    },
    {
      name: "Naresh Kumar",
      city: "Delhi",
      rating: 5,
      review:
        "Excellent service regarding dental treatment",
    },
    {
      name: "Suman Sinha",
      city: "Delhi",
      rating: 5,
      review:
        "Clean and well organised centre in the locality with experienced doctors, who make u comfortable and well versed with the treatment process. Had a pleasant experience with Dr.Nikhil nd team",
    },
    {
      name: "Sanjay Sehgal",
      city: "Delhi",
      rating: 5,
      review:
        "Advanced healthcare facilities available at reasonable cost, qualified and well experienced doctors. staff behaviour is polite and helpful. must opt for Sharanya health care",
    },
    {
      name: "Jaswant Gupta",
      city: "Delhi",
      rating: 5,
      review: "Good staff, hygiene, good quality treatment. ENT DOCTOR is well experienced. He explained very well.Must recommend",
    },
  ];
  return (
    <div className="bg-[#fcfaf7] py-10 flex flex-col gap-4">
      <h3 className="text-2xl font-bold text-center">Our Patient Love Us</h3>
      <p className="text-orange-500 text-center">
        Based on 5981 Recommendations | Rated 5.0 Out of 5
      </p>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-4 md:px-8 lg:px-16">
        {reviews.map((item, i) => (
          <div key={i} className="bg-white rounded-md p-2 flex flex-col gap-2">
            <div className="flex gap-2">
              <div className="w-12 h-12 bg-purple-300 rounded-full flex justify-center items-center">
                {item.name[0]}
              </div>
              <div>
                <p>{item.name}</p>
                <Rating value={5} readOnly />
              </div>
            </div>
            <p className="text-sm">{item.review}</p>
            <Divider sx={{ marginTop: "auto" }} />
            <p className="text-sm">City : {item.city}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Reviews;
