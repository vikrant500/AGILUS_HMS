import React, { useState, useEffect } from "react";
import Modal from "./components/modal"; // Adjust path according to your structure


const countryCodes = [
  { name: "United States", code: "+1" },
  { name: "Canada", code: "+1" },
  { name: "United Kingdom", code: "+44" },
  { name: "Australia", code: "+61" },
  { name: "India", code: "+91" },
  { name: "Zimbabwe", code: "+263" },
  { name: "Zambia", code: "+260" },
  { name: "Yemen", code: "+967" },
  { name: "Western Sahara", code: "+212" },
  { name: "Wallis and Futuna", code: "+681" },
  { name: "Vietnam", code: "+84" },
  { name: "Venezuela", code: "+58" },
  { name: "Vatican", code: "+379" },
  { name: "Vanuatu", code: "+678" },
  { name: "Uzbekistan", code: "+998" },
  { name: "Uruguay", code: "+598" },
  { name: "Ukraine", code: "+380" },
  { name: "Uganda", code: "+256" },
  { name: "United Arab Emirates", code: "+971" },
  { name: "Tuvalu", code: "+688" },
  { name: "Turkmenistan", code: "+993" },
  { name: "Turkey", code: "+90" },
  { name: "Tunisia", code: "+216" },
  { name: "Tonga", code: "+676" },
  { name: "Tokelau", code: "+690" },
  { name: "Togo", code: "+228" },
  { name: "Thailand", code: "+66" },
  { name: "Tanzania", code: "+255" },
  { name: "Tajikistan", code: "+992" },
  { name: "Taiwan", code: "+886" },
  { name: "Syria", code: "+963" },
  { name: "Switzerland", code: "+41" },
  { name: "Sweden", code: "+46" },
  { name: "Swaziland", code: "+268" },
  { name: "Svalbard and Jan Mayen", code: "+47" },
  { name: "Suriname", code: "+597" },
  { name: "Sudan", code: "+249" },
  { name: "Saint Pierre and Miquelon", code: "+508" },
  { name: "Saint Martin", code: "+590" },
  { name: "Saint Helena", code: "+290" },
  { name: "Sri Lanka", code: "+94" },
  { name: "Spain", code: "+34" },
  { name: "South Sudan", code: "+211" },
  { name: "South Korea", code: "+82" },
  { name: "South Africa", code: "+27" },
  { name: "Somalia", code: "+252" },
  { name: "Solomon Islands", code: "+677" },
  { name: "Slovenia", code: "+386" },
  { name: "Slovakia", code: "+421" },
  { name: "Singapore", code: "+65" },
  { name: "Sierra Leone", code: "+232" },
  { name: "Seychelles", code: "+248" },
  { name: "Serbia", code: "+381" },
  { name: "Senegal", code: "+221" },
  { name: "Saudi Arabia", code: "+966" },
  { name: "Sao Tome and Principe", code: "+239" },
  { name: "San Marino", code: "+378" },
  { name: "Samoa", code: "+685" },
  { name: "Saint Barthelemy", code: "+590" },
  { name: "Rwanda", code: "+250" },
  { name: "Russia", code: "+7" },
  { name: "Romania", code: "+40" },
  { name: "Reunion", code: "+262" },
  { name: "Qatar", code: "+974" },
  { name: "Portugal", code: "+351" },
  { name: "Poland", code: "+48" },
  { name: "Philippines", code: "+63" },
  { name: "Peru", code: "+51" },
  { name: "Paraguay", code: "+595" },
  { name: "Papua New Guinea", code: "+675" },
  { name: "Panama", code: "+507" },
  { name: "Palestine", code: "+970" },
  { name: "Palau", code: "+680" },
  { name: "Pakistan", code: "+92" },
  { name: "Oman", code: "+968" },
  { name: "Norway", code: "+47" },
  { name: "North Korea", code: "+850" },
  { name: "Niue", code: "+683" },
  { name: "Nigeria", code: "+234" },
  { name: "Niger", code: "+227" },
  { name: "Nicaragua", code: "+505" },
  { name: "New Zealand", code: "+64" },
  { name: "New Caledonia", code: "+687" },
  { name: "Netherlands", code: "+31" },
  { name: "Nepal", code: "+977" },
  { name: "Nauru", code: "+674" },
  { name: "Namibia", code: "+264" },
  { name: "Mozambique", code: "+258" },
  { name: "Morocco", code: "+212" },
  { name: "Montenegro", code: "+382" },
  { name: "Mongolia", code: "+976"}
]

const BookAppointment = () => {
  const [modalVisible, setModalVisible] = useState(false);
  const [selectedDisease, setSelectedDisease] = useState("");
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedCountryCode, setSelectedCountryCode] = useState(countryCodes[0].code);

  const popularDiseases = [
    "Circumcision",
    "Piles",
    "Cataract",
    "Fissure",
    "Abortion",
    "Gynecomastia",
    "Arthritis",
    "Diabetes"
  ];

  const allDiseases = [
      "Circumcision",
    "Piles",
    "Cataract",
    "Fissure",
    "Abortion",
    "Gynecomastia",
    "Pilonidal Sinus",
    "Rectal Prolapse",
    "Fissure",
    "Fistula",
    "Fecal Incontinence",
    "Constipation",
    "Hemorrhoids",
    "Umbilical Hernia",
    "Hydrocele",
    "Inguinal Hernia",
    "Incisional Hernia",
    "Appendicitis",
    "Gallstone",
    "Dermabrasion",
    "Fractional Laser",
    "CO2 Laser",
    "HydraFacial",
    "PhotoFacial",
    "TCA Peel",
    "Glyco-Peel",
    "Under Eye Peel",
    "TCA Cross",
    "Scar Marks Abrasion Surgical",
    "Permanent Hair Reduction",
    "Tattoo Removal",
    "Warts/Corn/Skin Tag Removal",
    "Dermal Fillers",
    "PRP (Platelet-Rich Plasma)",
    "Vampire Facial",
    "Stem Cell Therapy",
    "Ear Lobuloplasty",
    "Non-Surgical Liposuction",
    "Circumcision",
    "Diabetic Foot Care",
    "Subcision",
    "Allergy Testing and Immunotherapy",
    "Lobuloplasty",
    "Tympanoplasty",
    "Mastoidectomy",
    "Tonsillectomy",
    "Adenoidectomy",
    "Snoring Surgery",
    "Septoplasty",
    "Nasal Bone Fracture Reduction",
    "FESS (Functional Endoscopic Sinus Surgery)",
    "Hearing Impairment Procedures",
    "Stapes Surgery",
    "Thyroid Surgery",
    "Parotid Surgery",
    "Vocal Cord Surgery",
    "Diabetes Foot",
    "Diabetes Management",
    "Diabetes Reversal Programme",
    "Blood Pressure Management",
    "COPD (Chronic Obstructive Pulmonary Disease)",
    "Pneumonia",
    "Lung Cancer",
    "Interstitial Lung Disease",
    "Sarcoidosis",
    "Pulmonary Embolism",
    "Tuberculosis",
    "Asthma & Allergy",
    "Pleural Diseases",
    "Chronic Respiratory Failure",
    "ACL Injuries",
    "Ganglion Treatment",
    "PRP Injection",
    "Intra-Articular Injection",
    "Fracture Management",
    "Pain Management",
    "Bell’s Palsy",
    "Facial Palsy",
    "Gait Training",
    "Paralysis Management",
    "Geriatric Rehabilitation",
    "Pre/Post Operative Rehabilitation",
    "Scaling with Polishing",
    "Tooth Whitening",
    "Tooth-Colored Filling",
    "Impaction & Extraction",
    "Orthodontic Treatment",
    "Denture",
    "Capping (Dental Crowns)",
    "Root Canal Treatment",
    "Implant (Dental Implants)",
    "Asthma",
    "Allergies",
    "Sinusitis",
    "Nasal Polyps",
    "Gastric Troubles",
    "Migraine",
    "Anxiety",
    "Depression",
    "Menstrual Problems",
    "Arthritis",
    "Spondylosis",
    "Skin Disorders",
    "Children Disorders",
    "Weight Loss Therapy",
    "Weight Gain Therapy",
    "Gynaecology",
    "Laparoscopy",
    "Proctology"
    // Diseases list...
  ];

  useEffect(() => {
    window.scrollTo(0, 0);
  }, []);

  const toggleModal = () => {
    setModalVisible(!modalVisible);
  };

  const selectDisease = (disease) => {
    setSelectedDisease(disease);
    setModalVisible(false);
  };

  return (
    <div className="bg-[#fcfaf7] min-h-screen flex justify-center items-center">
      <form className="bg-white p-4 rounded-lg shadow-lg w-full md:max-w-md">
        <div className="flex mb-2 space-x-2">
          <div className="w-1/2">
            <label htmlFor="first-name" className="block font-bold mb-1">
              First Name
            </label>
            <input
              type="text"
              id="first-name"
              name="first-name"
              required
              className="w-full p-1 border border-gray-300 rounded"
            />
          </div>
          <div className="w-1/2">
            <label htmlFor="last-name" className="block font-bold mb-1">
              Last Name
            </label>
            <input
              type="text"
              id="last-name"
              name="last-name"
              required
              className="w-full p-1 border border-gray-300 rounded"
            />
          </div>
        </div>

        <div className="flex mb-2 space-x-2">
          <div className="w-1/2">
            <label htmlFor="date-of-birth" className="block font-bold mb-1">
              Date of Birth
            </label>
            <input
              type="date"
              id="date-of-birth"
              name="date-of-birth"
              required
              className="w-full p-1 border border-gray-300 rounded"
            />
          </div>
          <div className="w-1/2">
            <label htmlFor="phone-number" className="block font-bold mb-1">
              Enter Mobile Number
            </label>
            <div className="flex space-x-1">
              <select
                value={selectedCountryCode}
                onChange={(e) => setSelectedCountryCode(e.target.value)}
                className="p-1 border border-gray-300 rounded"
              >
                {countryCodes.map((country) => (
                  <option key={country.code} value={country.code}>
                    {country.code}
                  </option>
                ))}
              </select>
              <input
                type="tel"
                id="phone-number"
                name="phone-number"
                pattern="[0-9]{10}"
                required
                className="w-full p-1 border border-gray-300 rounded"
              />
            </div>
          </div>
        </div>

        <div className="mb-2 relative">
          <label htmlFor="disease-search" className="block font-bold mb-1">
            Select Disease
          </label>
          <input
            type="text"
            id="disease-search"
            placeholder="Enter your disease"
            value={selectedDisease}
            onClick={toggleModal}
            readOnly
            className="w-full p-1 border border-gray-300 rounded cursor-pointer"
          />
          <Modal
            show={modalVisible}
            onClose={toggleModal}
            popularDiseases={popularDiseases}
            allDiseases={allDiseases}
            selectDisease={selectDisease}
            searchTerm={searchTerm}
            setSearchTerm={setSearchTerm}
          />
        </div>

        <div className="mb-2">
          <label htmlFor="comments" className="block font-bold mb-1">
            Comments
          </label>
          <textarea
            id="comments"
            name="comments"
            rows="3"
            className="w-full p-1 border border-gray-300 rounded"
          ></textarea>
        </div>

        <button
          type="submit"
          className="w-full py-1 bg-blue-500 text-white rounded hover:bg-blue-700"
        >
          Book Your FREE Consultation
        </button>
      </form>
    </div>
  );
};

export default BookAppointment;
