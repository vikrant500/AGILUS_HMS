import React, { useEffect, useState } from "react";
import Section1 from "./components/Section1";
import Section2 from "./components/Section2";
import Reviews from "../home/components/Reviews";
import Figures from "../../components/others/Figures";
import WhyUs from "../../components/others/WhyUs";
import FAQ from "../../components/others/FAQ";
import { assets } from "../../assets";
import Specialities from "../../components/others/Specialities";
import { useParams } from "react-router-dom";
import { examples } from "./constant/examples";
const Treatments = () => {
  const [data, setData] = useState({
    faq_list: [],
    section_1_data: {},
    specialities: [],
    section_2_data: {},
  });
  const [loading, setLoading] = useState(true);
  // const faq_list = [
  //   {
  //     question: "test 2",
  //     answer: "test 2",
  //   },
  //   {
  //     question: "test 3",
  //     answer: "test 3",
  //   },
  // ];

  // const section_1_data = {
  //   heading: "Specialized center for Proctology Treatment",
  //   description:
  //     "Sharanya Care is a Multispecialty healthcare provider which aims to deliver a hassle-free surgical experience to all patients by leveraging technology, and a set of advanced operations and powerful processes. Proctology is a branch of medicine that deals with the diagnosis and treatment of diseases and medical conditions related to the anus and the rectum.",
  // };

  // const specialities = [
  //   {
  //     title: "Gynaecology",
  //     description:
  //       "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quas, voluptatum.",
  //   },
  //   {
  //     title: "Laparoscopy",
  //     description:
  //       "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quas, voluptatum.",
  //   },
  //   {
  //     title: "Proctology",
  //     description:
  //       "Lorem ipsum dolor sit amet consectetur adipisicing elit. Quas, voluptatum.",
  //   },
  // ];

  // const section_2_data = {
  //   title: "Proctology Treatment With Advanced Laser Treatment",
  //   description:
  //     "Proctology is the branch of medicine that deals with diagnosis, managing and treating problems related to anal, colon and rectum. It is also known as colorectal treatment. Sharanya Care is a multispeciality clinic that aims to provide best proctology treatment in India. The highly experienced Proctologists at Sharanya Care conduct minimally invasive laser treatment for problems that include hemorrhoids, anal fistula, fissure, chronic constipation and pilonidal sinus.",
  //   list: [
  //     "USFDA-Approved Procedure",
  //     "No Cuts, No Wounds, No Stitches",
  //     "Daycare Procedure",
  //     "Quick Recovery",
  //     "Covered By Insurance",
  //   ],
  // };

  const { id } = useParams();
  console.log(id);

  useEffect(() => {
    window.scrollTo(0, 0);
    if (examples[id]) {
      setData({
        faq_list: examples[id].faq_list,
        section_1_data: examples[id].section_1_data,
        specialities: examples[id].specialities,
        section_2_data: examples[id].section_2_data,
      });
      setLoading(false);
    }
  }, [id]);

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);
  return (
    <div>
      {!loading && (
        <>
          <Section1 section_1_data={data.section_1_data} />
          <Section2 section_2_data={data.section_2_data} />
          <Specialities specialities={data.specialities} />
          <Figures />
          <Reviews />
          <WhyUs />
          <FAQ faq_list={data.faq_list} />
        </>
      )}
    </div>
  );
};

export default Treatments;
