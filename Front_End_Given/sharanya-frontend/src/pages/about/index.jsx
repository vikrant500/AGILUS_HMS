import { Diamond, TrackChanges, Visibility } from "@mui/icons-material";
import React, { useEffect } from "react";
import Figures from "../../components/others/Figures";
import aboutImage from '../../assets/images/About/about-image.jpg'; // import path for the image that was added in the left

const About = () => {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const data = [
    {
      title: "Our Mission",
      description:
        "To fix the broken patient journey by offering full-stack healthcare services and increasing patient centricity.",
      icon: <TrackChanges sx={{ fontSize: "48px", color: "white" }} />,
    },
    {
      title: "Our Vision",
      description:
        "To ensure consistent quality and advanced surgical care and take the latest medical technologies to Tier 2 and Tier 3 cities.",
      icon: <Visibility sx={{ fontSize: "48px", color: "white" }} />,
    },
    {
      title: "Our Values",
      description:
        "To collaborate as a team and take extreme ownership of our audacious goals to achieve targets and display tremendous Integrity.",
      icon: <Diamond sx={{ fontSize: "48px", color: "white" }} />,
    },
  ];

  return (
    <>
      <div className="py-10 px-4 md:px-8 flex flex-col md:flex-row gap-8 items-center">
        <div className="flex-1">
          <img src={aboutImage} alt="About Sharanya Care" className="w-full h-auto rounded-lg shadow-md" />
        </div>
        <div className="flex-1 flex flex-col gap-6">
          <h4 className="text-2xl font-bold text-gray-800 text-center md:text-left"><center>About Sharanya Care</center></h4>
          <p className="leading-relaxed text-gray-600">
            Sharanya Care is a new-age healthcare company with a laser-sharp focus
            on simplifying the entire surgery journey of a patient and his/her
            attendant by offering care and assistance at each and every step.
          </p>
          <p className="leading-relaxed text-gray-600">
            Sharanya Care ensures that the patient’s experience right from the
            discovery of the right doctor, to booking an appointment at the
            clinic, getting a detailed diagnosis done, booking tests at a
            diagnostic center, getting insurance paperwork done, commuting from
            home to the hospital & back on the day of the surgery,
            admission-discharge processes at the hospital, and follow-up
            consultation after the surgery – is hassle-free and care-filled.
          </p>
          <p className="leading-relaxed text-gray-600">
            Sharanya Care commits to enhancing access and ensuring high quality
            secondary-care surgeries through an innovative care model. Sharanya
            Care operates on an innovative full-stack Care delivery model to
            ensure that high quality surgical care is offered to patients at an
            affordable cost. The company has partnerships with health insurance
            companies and financing providers. These partnerships help in easy and
            faster cashless claim approvals and EMIs facilities at 0% interest.
          </p>
          <p className="leading-relaxed text-gray-600">
            Sharanya Care surgical centers are currently functional across 7 metro
            cities – Mumbai, Pune, Delhi, Bangalore, Hyderabad, Chennai and
            Kolkata – and 35 tier 2 & 3 cities across the country. Operating in
            over 12 surgical categories such as General Surgery, Ophthalmology,
            ENT, Urology, Gynaecology, Sharanya Care, has successfully treated
            over 60,000 patients and completed 1 Million patient interactions.
          </p>
        </div>
      </div>

      <div className="py-10 px-4 md:px-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {data.map((item, i) => (
            <div key={i} className="group border-[1px] border-gray-200 px-6 py-10 rounded-lg flex flex-col gap-6 transition-transform transform hover:scale-105 hover:shadow-lg hover:bg-orange-300 transition-all duration-350 ease-in-out">
              <div className="h-20 w-20 rounded-full bg-gradient-to-r from-orange-400 to-orange-500 flex items-center justify-center mx-auto shadow-md">
                {item.icon}
              </div>
              <h3 className="text-2xl text-center font-bold text-gray-700">{item.title}</h3>
              <p className="text-gray-600 group-hover:text-white">{item.description}</p>
            </div>
          ))}
        </div>
      </div>
      <Figures />
    </>
  );
};

export default About;