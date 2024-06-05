import React, { useEffect, useState } from "react";
import { Container } from "@mui/system";

import { Close } from "@mui/icons-material";
import { assets } from "../../assets";
const doctors = [
  {
    name: "DR. PRADEEP AGGARWAL",
    education: [
      "MBBS, MD (Int Medicine - Cardio, Diabetes)",
      "PGDUS, Adult (AIIMS)",
      "RESD Dermatology, FAM (Germany)",
      "Adv Endocrinology & Diabetes (Mayo Clinic & Cleveland, USA)",
      "PG Rheumatology (EULAR)"
    ],
    expertise: [
      "Internal Medicine Physician",
      "Dermatologist",
      "Diabetologist"
    ],
    experience: [
      "Ex Resident at Safdurjung Hospital, AIIMS, DDU Hospital, KIMS",
      "Ex Assoc Prof. Rajiv Gandhi Medical College"
    ],
    services: [
      "Botox",
      "PRP Therapy",
      "Hydrafacial",
      "Microdermabrasion",
      "Laser Hair Removal",
      "Hair Transplantation",
      "All routine skin & hair disease treatment",
      "Diabetes reversal program",
      "Hypertension",
      "Thyroid",
      "Fever case management"
    ]
  },
  {
    name: "DR. SHWETA ANAND",
    education: ["MBBS, DNB, FSM"],
    expertise: ["Pulmonary & Critical Care Specialist"],
    experience: ["Formerly at NITRD", "ESIC Hospital"],
    services: [
      "Bronchiectasis",
      "Asthma",
      "Tuberculosis",
      "Shortness of Breath",
      "Pneumonia",
      "Bronchitis",
      "Wheezing",
      "Asphyxia",
      "COPD",
      "Sleep Apnea",
      "Persistent Cough",
      "Fluid in the chest",
      "Occupational Lung Disease",
      "Sleep Medicine",
      "Asbestosis"
    ]
  },
  {
    name: "DR. NIKHIL KUMAR",
    education: ["Dental Surgeon, BDS"],
    experience: [
      "Junior Resident at Sanjay Gandhi Hospital",
      "Honorary at ESIC Basai Hospital",
      "Have worked with NGO-Mega Health Camp",
      "Have organized many health-related camps in Uttam Nagar Area, West Delhi",
      "Have attended the future dental professionals program with Colgate and IDA",
      "Consultant at Ayushman Hospital, Dwarka"
    ],
    expertise: [
      "Meeting with patients to discuss and treat dental concerns",
      "Performing regular cleanings and other preventative procedures, and establishing a plan for better dental hygiene",
      "Performing dental procedures, such as extractions, root canals, and filling cavities",
      "Applying helpful agents to teeth, such as sealants or whiteners",
      "Prescribing medications for dental problems, such as pain medications or antibiotics",
      "Managing and communicating with other staff members to provide care to patients"
    ]
  },
  {
    name: "DR. ANJULA NARANG",
    education: [
      "BHMS (DU)",
      "MD (Hom)",
      "SCPH (Mumbai)",
      "Senior Research Fellow (CCRH)"
    ],
    expertise: ["Homoeopathic Physician"],
    
    services: [
      "Child Health Care",
      "Skin & Hair Care",
      "Women Health Care",
      "Lifestyle & Diet Care",
      "Stomach Disorders",
      "Ortho Care",
      "Thyroid",
      "Migraine",
      "Sinusitis"
    ]
  },
  {
    name: "NUTRITIONIST MEGHA BISWAS",
    education: [
      "Graduate with Diploma in Dietetics, Health and Nutrition",
      "Graduate with Diploma in Yoga Therapy"
    ],
    certifications: [
      "Diploma in Dietetics, Health and Nutrition from VLCC Institute",
      "Certificate in Weight Management and Slimming Therapies from VLCC Institute",
      "Certified Yoga Instructor from Bhartiya Vidya Bhawan"
    ],
    experience: [
      "Internship from Sir Ganga Ram Hospital",
      "VLCC Slimming Department",
      "Diabetes Educator in a Diabetes reversal program"
    ],
    expertise: [
      "Weight Management (Loss and Gain)",
      "Yoga therapy",
      "Slimming therapies",
      "Diet therapies (with Ayurveda)",
      "Therapeutic diets for all types of disease (PCOD, Hypertension, Diabetes, Thyroid (hyper or hypo), Cancer etc)",
      "Diet for Pregnancy and Lactation period"
    ]
  },
  {
    name: "DR. ANAMIKA (Physiotherapist)",
    education: [
      "Bachelor of Physiotherapy (BPT)",
      "Member of Delhi Council of Physiotherapy (DCPT) - PR-4164"
    ],
    experience: [
      "ESIC Hospital",
      "Star Physiotherapy Clinic",
      "Eva Physiotherapy And Wellness Clinic",
      "Intern of Sir Ganga Ram Hospital"
    ],
    certifications: [
      "Certified Global Taping Therapist by Physio Foundation India",
      "Spasticity Management in Cerebral Palsy Disorder",
      "PNF - Advanced Skill Training for Lower Extremity and Gait Rehabilitation in Neuro-Musculoskeletal Disorder",
      "Complex Spine Trauma Treatment",
      "Practiced in MFR and Dry Needling"
    ],
    conferences: [
      "Meniscal Injuries: A Comprehensive Approach",
      "Foot Anomalies: Pathophysiology and Management",
      "Moral Values in Health Care",
      "Musculoskeletal Infections",
      "Management of Breast Cancer: A Holistic Approach, Optimising Movement in Transforming Society: Advances in Orthopaedic Rehabilitation",
      "Recent Advancement in Neuro-Rehabilitation",
      "Introduction of Human Motion Analysis",
      "Awareness of Tuberculosis in Community"
    ],
    expertise: [
      "Worked with many neurological, orthopaedical, and paediatric patients in field of physiotherapy"
    ]
  },
  {
    name: "DR. SHASHANK SAURABH",
    education: [
      "MBBS, DNB - ENT & HNS",
      "Fellowship in Otology (KKR-Chennai)",
      "Allergy Specialist (VPCI-New Delhi)"
    ],
    experience: [
      "Former Resident at Northern Railway Central Hospital, New Delhi",
      "Former Resident at Hindu Rao Hospital, New Delhi",
      "Former Senior Resident at ESIC Hospital Jhilmil & ESIC Basaidarapur",
      "Former Senior Resident at Safdarjung Hospital"
    ],
    expertise: [
      "Allergy Testing and Immunotherapy",
      "Lobuloplasty",
      "Tympanoplasty",
      "Mastoidectomy",
      "Tonsillectomy",
      "Adenoidectomy",
      "Snoring Surgery",
      "Septoplasty",
      "Nasal Bone Fracture Reduction",
      "FESS (Sinus Surgery)",
      "Hearing Impairment",
      "Stapes Surgery",
      "Thyroid Surgery",
      "Parotid Surgery",
      "Vocal Cord Surgery"
    ]
  }
];

const DoctorCard = ({ doctor }) => {
  const [expanded, setExpanded] = useState(false);

  return (
    <div
      className={`bg-[#E0ECDE] hover:bg-[#CDE0C9] rounded-xl shadow-lg p-4 transition-all duration-300 ease-in-out text-black ${
        expanded ? "max-h-[1000px]" : "h-auto max-h-[200px]"
      } overflow-hidden w-full`}
      onClick={() => setExpanded(!expanded)}
    >
      <div className="cursor-pointer flex flex-col md:flex-row items-start md:items-center">
        <h4 className="text-xl font-bold text-[#2C6975] md:w-1/3">{doctor.name}</h4>
        <p className={`text-md text-[#68B2A0] md:w-2/3 ${!expanded ? "block" : "hidden"}`}>
          {doctor.expertise.join(", ")}
        </p>
      </div>
      <div className={`mt-4 ${expanded ? "block" : "hidden"} text-[#2C6975]`}>
        <ul className="list-disc pl-4">
          {doctor.expertise.map((item, index) => (
            <li key={index}>{item}</li>
          ))}
        </ul>
        {doctor.education && (
          <>
            <h5 className="font-semibold">Education:</h5>
            <ul className="list-disc pl-4">
              {doctor.education.map((item, index) => (
                <li key={index}>{item}</li>
              ))}
            </ul>
          </>
        )}
        {doctor.experience && (
          <>
            <h5 className="font-semibold">Experience:</h5>
            <ul className="list-disc pl-4">
              {doctor.experience.map((item, index) => (
                <li key={index}>{item}</li>
              ))}
            </ul>
          </>
        )}
        {doctor.services && (
          <>
            <h5 className="font-semibold">Services Provided:</h5>
            <ul className="list-disc pl-4">
              {doctor.services.map((item, index) => (
                <li key={index}>{item}</li>
              ))}
            </ul>
          </>
        )}
        {doctor.certifications && (
          <>
            <h5 className="font-semibold">Professional Certification:</h5>
            <ul className="list-disc pl-4">
              {doctor.certifications.map((item, index) => (
                <li key={index}>{item}</li>
              ))}
            </ul>
          </>
        )}
        {doctor.conferences && (
          <>
            <h5 className="font-semibold">Certified Participant in Conferences:</h5>
            <ul className="list-disc pl-4">
              {doctor.conferences.map((item, index) => (
                <li key={index}>{item}</li>
              ))}
            </ul>
          </>
        )}
      </div>
    </div>
  );
};

const App = () => {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  return (
    <div className="min-h-screen bg-[#FFFFFF] flex flex-col items-center justify-center p-4">
      <h1 className="text-3xl font-bold mb-6 text-[#2C6975]">Meet Our Doctors</h1>
      <div className="w-full max-w-4xl space-y-4">
        {doctors.map((doctor, index) => (
          <DoctorCard key={index} doctor={doctor} />
        ))}
      </div>
    </div>
  );
};

export default App;