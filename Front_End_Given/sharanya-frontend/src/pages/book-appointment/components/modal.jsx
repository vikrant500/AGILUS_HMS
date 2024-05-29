import React, { useState, useEffect, useCallback } from "react";

const Modal = ({ show, popularDiseases, allDiseases, selectDisease, onClose }) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [filteredDiseases, setFilteredDiseases] = useState(allDiseases);

  const handleSearchChange = (event) => {
    const term = event.target.value.toLowerCase();
    setSearchTerm(term);
    const filtered = allDiseases.filter(disease => disease.toLowerCase().includes(term));
    setSearchResults(filtered);
  };

  useEffect(() => {
    if (searchTerm) {
      setFilteredDiseases(searchResults);
    } else {
      setFilteredDiseases(allDiseases);
    }
  }, [searchTerm, searchResults, allDiseases]);

  const handleKeyPress = (event) => {
    if (event.key === 'Enter' && searchResults.length > 0) {
      selectDisease(searchResults[0]);
      setSearchTerm("");
    }
  };

  const handleClose = () => {
    setSearchTerm("");
    onClose();
  };

  const handleDiseaseClick = (disease) => {
    selectDisease(disease);
    setSearchTerm("");
  };

  const handleEscKey = useCallback((event) => {
    if (event.key === 'Escape') {
      setSearchTerm("");
      onClose();
    }
  }, [onClose]);

  useEffect(() => {
    window.addEventListener('keydown', handleEscKey);
    return () => {
      window.removeEventListener('keydown', handleEscKey);
    };
  }, [handleEscKey]);

  return (
    <>
      {show && (
        <div className="modal-overlay fixed inset-0 flex justify-center items-center bg-black bg-opacity-50">
          <div className="modal-content bg-white p-8 rounded-lg shadow-lg w-11/12 md:w-2/3 lg:w-1/2 xl:w-1/3 h-auto relative transition-transform transform duration-300 ease-in-out">
            <button onClick={handleClose} className="close-button absolute top-2 right-2 text-gray-600 hover:text-gray-800 focus:outline-none z-10">
              <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
            <input
              type="text"
              value={searchTerm}
              onChange={handleSearchChange}
              onKeyPress={handleKeyPress}
              placeholder="Search disease"
              className="search-bar w-full p-3 mb-4 border border-gray-300 rounded-lg focus:outline-none focus:ring focus:border-blue-700 transition duration-300"
            />
            {!searchTerm && (
              <div className="popular-diseases mb-4">
                <h4 className="text-gray-800 text-lg font-semibold mb-2">Most Popular Diseases:</h4>
                <div className="popular-disease-buttons flex flex-wrap gap-2">
                  {popularDiseases.map((disease, index) => (
                    <button
                      key={index}
                      onClick={() => handleDiseaseClick(disease)}
                      className="popular-disease-button h-8 px-3 border border-gray-300 rounded-md text-sm font-medium text-gray-600 bg-white hover:bg-blue-700 hover:text-white hover:border-[#6BB2A0] focus:outline-none focus:ring focus:border-blue-700 transition duration-300"
                    >
                      {disease}
                    </button>
                  ))}
                </div>
              </div>
            )}
            <div className="other-diseases">
              <h4 className="text-gray-800 text-lg font-semibold mb-2">All Diseases:</h4>
              <div className="disease-list max-h-40 overflow-auto">
                {filteredDiseases.map((disease, index) => (
                  <div
                    key={index}
                    onClick={() => handleDiseaseClick(disease)}
                    className="other-disease-item py-2 px-3 mb-1 border-b border-gray-200 cursor-pointer text-sm text-gray-600 bg-white hover:bg-blue-700 hover:text-white hover:border-blue-700 transition duration-300 rounded-md"
                  >
                    {disease}
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default Modal;
