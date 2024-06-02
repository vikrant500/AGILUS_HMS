import React from "react";
import Rating from "@mui/material/Rating";
import Divider from "@mui/material/Divider";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";


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

  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 3,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3000,
  };

  return (
    <div className="bg-gradient-to-r from-grey to-grey py-10 flex flex-col gap-5">
      <h2 class="faq__title text-primary leading-[120%] text-[30px] xl:text-[44px] font-semibold capitalize tracking-[0.44px] text-center mb-[50px]">Our Patients Love Us</h2>
      <p className="text-white-500 text-center">
        Based on 5981 Recommendations | Rated 5.0 Out of 5
      </p>

      <div className="p-4 md:px-8 lg:px-18">
        <Slider {...settings}>
          {reviews.map((item, i) => (
            <div key={i} className="p-2">
              <div className="bg-white hover:bg-orange-200 rounded-xl shadow-lg p-4 flex flex-col gap-2 transition-colors duration-300">
                <div className="flex gap-2">
                  <div className="w-12 h-12 bg-[#2c6975] rounded-full flex justify-center items-center text-white">
                    {item.name[0]}
                  </div>
                  <div>
                    <p className="text-black">{item.name}</p>
                    <Rating value={item.rating} readOnly />
                  </div>
                </div>
                <p className="text-sm text-black">{item.review}</p>
                <Divider sx={{ marginTop: "auto" }} />
                <p className="text-sm text-black">City : {item.city}</p>
              </div>
            </div>
          ))}
        </Slider>
      </div>
    </div>
  );
};

export default Reviews;
