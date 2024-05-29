import React, { useEffect, useState } from "react";
import { useParams, Outlet } from "react-router-dom";
import Section1 from "./components/Section1";
import Section2 from "./components/Section2";
import Reviews from "../home/components/Reviews";
import Figures from "../../components/others/Figures";
import WhyUs from "../../components/others/WhyUs";
import FAQ from "../../components/others/FAQ";
import Specialities from "../../components/others/Specialities";
import { examples } from "./constant/examples";

const Treatments = () => {
  const [data, setData] = useState({
    faq_list: [],
    section_1_data: {},
    specialities: [],
    section_2_data: {},
  });
  const [loading, setLoading] = useState(true);
  const { treatmentId } = useParams();

  useEffect(() => {
    window.scrollTo(0, 0);
    if (examples[treatmentId]) {
      setData({
        faq_list: examples[treatmentId].faq_list,
        section_1_data: examples[treatmentId].section_1_data,
        specialities: examples[treatmentId].specialities,
        section_2_data: examples[treatmentId].section_2_data,
      });
      setLoading(false);
    }
  }, [treatmentId]);

  return (
    <div>
      {!loading && (
        <>
          <Section1 section_1_data={data.section_1_data} />
          <Section2 section_2_data={data.section_2_data} />
          <Specialities
            specialities={data.specialities.map((speciality) => ({
              ...speciality,
              link: `/treatment/${treatmentId}/specialities/${speciality.title.toLowerCase().replace(/ /g, "-")}`,
            }))}
          />
          <Figures />
          <Reviews />
          <WhyUs />
          <FAQ faq_list={data.faq_list} />
          <Outlet />
        </>
      )}
    </div>
  );
};

export default Treatments;
