import React, { useEffect } from "react";
import HeroSection from "./components/HeroSection";
import Specialities from "../../components/others/Specialities";
import WatchOurAds from "./components/WatchOurAds";
import Reviews from "./components/Reviews";
import WhyUs from "../../components/others/WhyUs";
import FAQ from "../../components/others/FAQ";
import Treatment from "./components/Treatment";
import Figures from "../../components/others/Figures";
import { assets } from "../../assets";

const Home = () => {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const faq_list = [
    {
      question: "How does a care coordinator help a patient at Sharanya Care?",
      answer:
        "The care coordinator, on day 1 or during the first interaction tries to understand the patient’s problem and guides him/ her with the best treatment options. The care coordinator will assist you in getting your OPD scheduled and will help you connect with the best doctor according to your needs.",
    },
    {
      question: "Does Sharanya Care provide any emergency surgical treatment?",
      answer:
        "No, Sharanya Care does not attend to any kind of emergency surgeries or treatments. Sharanya Care only provides elective surgical care, i.e; surgeries that are scheduled in advance because it does not involve a medical emergency.",
    },
    {
      question: "Can I consult with a doctor online?",
      answer:
        "Yes, Sharanya Care offers online doctor consultations. With online consultation in place, patients can now consult a doctor based on their specialization from anywhere, anytime as per the available slots. Our patients can opt to talk to our doctors via call or online chat to discuss their health issues. Our doctors thoroughly understand the patient’s issue and provide them with the necessary medications and tests.",
    },
    {
      question: "Does Sharanya Care have insurance coverage for all surgeries?",
      answer:
        "Sharanya Care is not an insurance regulatory. Your insurance coverage depends on the health insurance type (personal, corporate) and the terms and conditions set by your insurance provider. Our insurance team only helps you get the maximum benefits of your policy.",
    },
    {
      question: "Does Sharanya Care provide a second opinion for any disease?",
      answer:
        "Yes, you can avail second medical opinion by specialized doctors for all diseases. Our doctor tracks your pathology reports, your post-operative report (if you had surgery earlier), your discharge summary (if you were hospitalized earlier for the disease), your current treatment and medication plan, and guides you with the required/ new treatment course ahead.",
    },
  ];

  const specialities = [
    {
      title: "Dermatology",
      image: assets.specialities.dermatology,
      link: "/treatment/dermatology",
      description:
        "Dermatology focuses on the diagnosis and treatment of skin, hair, and nail disorders. Dermatologists address a wide range of conditions, from acne to skin cancer, providing comprehensive care for the largest organ of the body.",
    },
    {
      title: "ENT",
      image: assets.specialities.ent,
      link: "/treatment/ent",
      description:
        "ENT specialists, or otolaryngologists, deal with disorders related to the ear, nose, throat, and head and neck structures. They manage conditions like hearing loss, sinusitis, and throat infections.",
    },
    {
      title: "Medicine",
      link: "/treatment/medicine",
      image: assets.specialities.medicine,
      description:
        "Internal Medicine encompasses a broad range of adult healthcare. Internists, or doctors of internal medicine, diagnose and treat various illnesses, offering comprehensive care for the entire body.",
    },
    {
      title: "Respiratory",
      link: "/treatment/respiratory",
      image: assets.specialities.respiratory,
      description:
        "Respiratory specialists focus on the diagnosis and treatment of conditions affecting the respiratory system, including diseases like asthma, chronic obstructive pulmonary disease (COPD), and pneumonia.",
    },
    {
      title: "Orthopaedic",
      link: "/treatment/orthopaedic",
      image: assets.specialities.orthopedics,
      description:
        "Orthopaedics deals with the musculoskeletal system, addressing issues related to bones, joints, muscles, ligaments, and tendons. Orthopaedic specialists provide care for fractures, arthritis, and sports injuries.",
    },
    {
      title: "Physiotherapy",
      link: "/treatment/physiotherapy",
      image: assets.specialities.physiotherapy,
      description:
        "Physiotherapy, or physical therapy, involves the use of exercises and manual techniques to promote healing and improve physical function.",
    },
    {
      title: "Dental",
      link: "/treatment/dental",
      image: assets.specialities.dentistry,
      description:
        "Dentistry focuses on oral health, including the diagnosis and treatment of issues related to teeth, gums, and the oral cavity. Dentists provide services such as cleanings, fillings, and dental surgeries.",
    },
    {
      title: "Homoeopathy",
      link: "/treatment/homoeopathy",
      image: assets.specialities.homeopathy,
      description:
        "Homoeopathy is a system of alternative medicine that uses highly diluted substances to stimulate the body's natural healing processes.",
    },
    {
      title: "Diet",
      link: "/treatment/diet",
      image: assets.specialities.diet,
      description:
        "Dietetics involves the study of nutrition and its impact on health. Dietitians work with individuals to create personalized nutrition plans, addressing specific health goals and dietary needs.",
    },
    // {
    //   title: "Diagnostics",
    //   link: "/treatment/diagnostics",
    //   image: assets.hero,
    //   description:
    //     "Diagnostics is the branch of medicine that focuses on the identification of diseases and conditions.It include imaging, laboratory tests, and other techniques to aid in the accurate diagnosis of illnesses.",
    // },
  ];

  return (
    <div>
      <HeroSection />
      {/* <Treatment /> */}
      <Specialities specialities={specialities} />
      <Figures />
      <Reviews />
      <WatchOurAds />
      <WhyUs />
      <FAQ faq_list={faq_list} />
    </div>
  );
};

export default Home;
