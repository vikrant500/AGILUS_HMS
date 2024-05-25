// pages/BookAppointment/index.jsx
import React, { useState, useEffect } from "react";
import Modal from "./components/modal"; // Adjust path according to your structure
 // Adjust path according to your structure

const BookAppointment = () => {
  const [modalVisible, setModalVisible] = useState(false);
  const [selectedDisease, setSelectedDisease] = useState("");
  const [searchTerm, setSearchTerm] = useState("");

  const popularDiseases = [
    "Circumcision",
    "Piles",
    "Cataract",
    "Fissure",
    "Abortion",
    "Gynecomastia",
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
    // Add more diseases as needed
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
    <div className="bg-[#fcfaf7] h-screen overflow-hidden flex justify-center items-center">
      <form className="bg-white p-6 rounded-lg shadow-lg w-96">
        <div className="mb-4">
          <label htmlFor="patient-name" className="block font-bold mb-2">
            Patient Name
          </label>
          <input
            type="text"
            id="patient-name"
            name="patient-name"
            required
            className="w-full p-2 border border-gray-300 rounded"
          />
        </div>
        <div className="mb-4">
          <label htmlFor="phone-number" className="block font-bold mb-2">
            Enter 10 Digit Mobile Number
          </label>
          <input
            type="tel"
            id="phone-number"
            name="phone-number"
            pattern="[0-9]{10}"
            required
            className="w-full p-2 border border-gray-300 rounded"
          />
        </div>
        <div className="mb-4 relative">
          <label htmlFor="disease-search" className="block font-bold mb-2">
            Select Disease
          </label>
          <input
            type="text"
            id="disease-search"
            placeholder="Start typing to select disease"
            value={selectedDisease}
            onClick={toggleModal}
            readOnly
            className="w-full p-2 border border-gray-300 rounded cursor-pointer"
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
        <button
          type="submit"
          className="w-full py-2 bg-blue-500 text-white rounded hover:bg-blue-700"
        >
          Book Your FREE Consultation
        </button>
      </form>
    </div>
  );
};

export default BookAppointment;
