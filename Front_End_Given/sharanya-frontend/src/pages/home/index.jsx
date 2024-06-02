import React, { useEffect } from "react";
import HeroSection from "./components/HeroSection";
import WatchOurAds from "./components/WatchOurAds";
import Reviews from "./components/Reviews";
import WhyUs from "../../components/others/WhyUs";
import FAQ from "../../components/others/FAQ";
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
        "Dermatology focuses on diagnosing and treating skin, hair, and nail disorders.<br> <a href='/treatment/dermatology'>Click here to know more.</a>",
    },
    {
      title: "ENT",
      image: assets.specialities.ent,
      link: "/treatment/ent",
      description:
        "ENT specialists treat ear, nose, and throat disorders.<br> <a href='/treatment/ent'>Click here to know more.</a>",
    },
    {
      title: "Medicine",
      link: "/treatment/medicine",
      image: assets.specialities.medicine,
      description:
        "Internists diagnose and treat adult illnesses, offering comprehensive care.<br> <a href='/treatment/medicine'>Click here to know more.</a>",
    },
    {
      title: "Respiratory",
      link: "/treatment/respiratory",
      image: assets.specialities.respiratory,
      description:
        "Specialists treat respiratory conditions like asthma, COPD, and pneumonia.<br> <a href='/treatment/respiratory'>Click here to know more.</a>",
    },
    {
      title: "Orthopaedic",
      link: "/treatment/orthopaedic",
      image: assets.specialities.orthopedics,
      description:
        "Orthopaedic specialists treat musculoskeletal issues like fractures and arthritis.<br> <a href='/treatment/orthopaedic'>Click here to know more.</a>",
    },
    {
      title: "Physiotherapy",
      link: "/treatment/physiotherapy",
      image: assets.specialities.physiotherapy,
      description:
        "Physical therapists use exercises and techniques to improve physical function.<br> <a href='/treatment/physiotherapy'>Click here to know more.</a>",
    },
    {
      title: "Dental",
      link: "/treatment/dental",
      image: assets.specialities.dentistry,
      description:
        "Dentists diagnose and treat oral health issues, providing cleanings, fillings, and surgeries.<br> <a href='/treatment/dental'>Click here to know more.</a>",
    },
    {
      title: "Homoeopathy",
      link: "/treatment/homoeopathy",
      image: assets.specialities.homeopathy,
      description:
        "Homoeopathy uses diluted substances to stimulate the body's healing processes.<br> <a href='/treatment/homoeopathy'>Click here to know more.</a>",
    },
    {
      title: "Diet",
      link: "/treatment/diet",
      image: assets.specialities.diet,
      description:
        "Dietitians create personalized nutrition plans to meet health goals.<br> <a href='/treatment/diet'>Click here to know more.</a>",
    },
  ];

  return (
    <div className="p-4 sm:p-6 lg:p-8 mt-20">
      <HeroSection />
      <div style={{ padding: "70px" }} />
      <h2 className="faq__title text-primary leading-[120%] text-[30px] xl:text-[44px] font-semibold capitalize tracking-[0.44px] text-center mb-[50px]">
        Specialities
      </h2> {/* Specialities Section Heading */}
      <div className="max-w-screen-xl mx-auto">
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-1 sm:gap-4 lg:gap-6 mx-auto">
          {specialities.map((speciality, index) => (
            <div key={index} className="relative flex flex-col overflow-hidden rounded-lg shadow-lg group mx-auto custom-container">
              <div className="bg-orange-400 absolute bottom-0 left-0 text-white text-base tracking-[2.24px] font-medium uppercase py-[6px] px-[18px]">
                {speciality.title}
              </div>
              <img
                src={speciality.image}
                alt={speciality.title}
                className="w-200px h-200px object-contain transition-transform duration-300 group-hover:scale-105"
              />
              <div className="absolute inset-0 bg-orange-500 bg-opacity-50 flex items-center justify-center opacity-0 transition-opacity duration-300 group-hover:opacity-100">
                <p className="text-white text-center p-2" dangerouslySetInnerHTML={{ __html: speciality.description }}></p>
              </div>
            </div>
          ))}
        </div>
      </div>
      <div style={{ padding: "70px" }} />
      <Figures />
      <div style={{ padding: "30px" }} />
      <Reviews />
      <div style={{ padding: "70px" }} />
      <WhyUs />
      <div style={{ padding: "70px" }} />
      <FAQ faq_list={faq_list} />
    </div>
  );
};

export default Home;
