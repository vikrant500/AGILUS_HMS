import { Diamond, TrackChanges, Visibility } from "@mui/icons-material";
import React, { useEffect } from "react";
import Figures from "../../components/others/Figures";

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
    <div className="py-10 px-4 md:px-8 flex flex-col gap-4">
      <div className="flex flex-col gap-4">
        <h4 className="text-xl font-bold"><center>Overview</center></h4>
        <p>
          Sharanya Care is a new-age healthcare company with a laser-sharp focus
          on simplifying the entire surgery journey of a patient and his/her
          attendant by offering care and assistance at each and every step.
        </p>
        <p>
          Sharanya Care ensures that the patient’s experience right from the
          discovery of the right doctor, to booking an appointment at the
          clinic, getting a detailed diagnosis done, booking tests at a
          diagnostic center, getting insurance paperwork done, commuting from
          home to the hospital & back on the day of the surgery,
          admission-discharge processes at the hospital, and follow-up
          consultation after the surgery – is hassle-free and care-filled.
        </p>
        <p>
          Sharanya Care commits to enhancing access and ensuring high quality
          secondary-care surgeries through an innovative care model. Sharanya
          Care operates on an innovative full- stack Care delivery model to
          ensure that high quality surgical care is offered to patients at an
          affordable cost. The company has partnerships with health insurance
          companies and financing providers. These partnerships help in easy and
          faster cashless claim approvals and EMIs facilities at 0% interest.
        </p>
        <p>
          Sharanya Care surgical centers are currently functional across 7 metro
          cities – Mumbai, Pune, Delhi, Bangalore, Hyderabad, Chennai and
          Kolkata – and 35 tier 2 & 3 cities across the country. Operating in
          over 12 surgical categories such as General Surgery, Ophthalmology,
          ENT, Urology, Gynaecology, Sharanya Care, has successfully treated
          over 60,000 patients and completed 1 Million patient interactions.
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {data.map((item, i) => (
          <div className="border-[1px] border-gray-400 px-4 py-8 rounded-md flex flex-col gap-4">
            <div className="h-20 w-20 rounded-full bg-orange-500 flex flex-col items-center justify-center mx-auto">
              {item.icon}
            </div>
            <h3 className="text-2xl text-center font-bold">{item.title}</h3>
            <p>{item.description}</p>
          </div>
        ))}
      </div>
    </div>
      <Figures />
    </>
  );
};

export default About;
