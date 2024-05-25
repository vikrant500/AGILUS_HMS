import React, { useState, useEffect, useCallback } from "react";

const Modal = ({ show, popularDiseases, allDiseases, selectDisease, onClose }) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const [filteredDiseases, setFilteredDiseases] = useState(allDiseases);

  // Function to filter diseases based on the search term
  const handleSearchChange = (event) => {
    const term = event.target.value.toLowerCase();
    setSearchTerm(term);

    // Filter the diseases based on the search term
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

  // Function to handle key press events in the search input
  const handleKeyPress = (event) => {
    if (event.key === 'Enter' && searchResults.length > 0) {
      selectDisease(searchResults[0]); // Select the first matching disease from the search results
      setSearchTerm(""); // Clear the search term after selecting a disease
    }
  };

  // Function to handle closing the modal
  const handleClose = () => {
    setSearchTerm(""); // Clear the search term
    onClose(); // Close the modal
  };

  // Function to handle clicking on a disease
  const handleDiseaseClick = (disease) => {
    selectDisease(disease);
    setSearchTerm(""); // Clear the search term
  };

  // Function to close the modal when Esc key is pressed
  const handleEscKey = useCallback((event) => {
    if (event.key === 'Escape') {
      setSearchTerm(""); // Clear the search term
      onClose(); // Close the modal
    }
  }, [onClose]); // Depend on the onClose function

  useEffect(() => {
    window.addEventListener('keydown', handleEscKey);
    return () => {
      window.removeEventListener('keydown', handleEscKey);
    };
  }, [handleEscKey]); // Depend on the handleEscKey function

  return (
    <>
      {show && (
        <div className="modal-overlay fixed inset-0 flex justify-center items-center bg-black bg-opacity-50">
          <div className="modal-content bg-white p-6 rounded-lg shadow-lg w-2/3 h-2/3 relative">
            <button onClick={handleClose} className="close-button absolute top-2 right-2 text-gray-500 hover:text-gray-700 focus:outline-none z-10">
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
              className="search-bar w-full p-4 mb-4 border border-gray-300 rounded"
            />
            {!searchTerm && (
              <div className="popular-diseases mb-4">
                <h4>Most Popular Diseases:</h4>
                <div className="popular-disease-buttons flex overflow-x-auto mb-2">
                  {popularDiseases.map((disease, index) => (
                    <button
                      key={index}
                      onClick={() => handleDiseaseClick(disease)}
                      className="popular-disease-button flex-none h-10 p-2 mr-2 border border-gray-300 rounded hover:bg-gray-200"
                    >
                      {disease}
                    </button>
                  ))}
                </div>
              </div>
            )}
            <div className="other-diseases">
              <h4>All Diseases:</h4>
              <div className="disease-list overflow-auto max-h-40">
                {filteredDiseases.map((disease, index) => (
                  <div
                    key={index}
                    onClick={() => handleDiseaseClick(disease)}
                    className="other-disease-item py-2 border-b border-gray-300 cursor-pointer hover:bg-orange-100"
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
