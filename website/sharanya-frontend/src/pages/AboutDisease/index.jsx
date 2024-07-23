import React, { useEffect, useState } from "react";
import Disease from '../../components/Disease';
import { useNavigate } from "react-router-dom";
import styles from '../IndexPage/css/CommonStyles.module.css'; // Adjust the path as necessary
import Header from '../../components/Header';
import config from '../../config/config'; // Import the config file

export default function AboutDisease() {
    const [diseases, setDiseases] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [searchResults, setSearchResults] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(10); // Number of diseases per page
    const [showSuggestions, setShowSuggestions] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        window.scrollTo(0, 0);
        fetch(`${config.apiUrl}/disease`)
            .then(response => response.json())
            .then(diseases => {
                setDiseases(diseases);
            });
    }, []);

    const handleDiseaseClick = (id) => {
        navigate(`/userlogin/disease/${id}`);
    };

    const handleSearch = (event) => {
        setSearchTerm(event.target.value);
        setShowSuggestions(true);
    };

    const handleSearchButtonClick = () => {
        const filteredDiseases = diseases.filter(disease =>
            disease.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setSearchResults(filteredDiseases);
        setCurrentPage(1);
        setShowSuggestions(false);
    };

    const handleSuggestionClick = (suggestion) => {
        setSearchTerm(suggestion);
        setShowSuggestions(false);
    };

    // Pagination logic
    const indexOfLastDisease = currentPage * itemsPerPage;
    const indexOfFirstDisease = indexOfLastDisease - itemsPerPage;
    const currentDiseases = (searchResults.length > 0 ? searchResults : diseases).slice(indexOfFirstDisease, indexOfLastDisease);

    const totalPages = Math.ceil((searchResults.length > 0 ? searchResults : diseases).length / itemsPerPage);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const handlePreviousPage = () => {
        if (currentPage > 1) {
            setCurrentPage(prevPage => prevPage - 1);
        }
    };

    const handleNextPage = () => {
        if (currentPage < totalPages) {
            setCurrentPage(prevPage => prevPage + 1);
        }
    };

    const filteredSuggestions = diseases.filter(disease =>
        disease.name.toLowerCase().includes(searchTerm.toLowerCase())
    ).slice(0, 5); // Limit the number of suggestions displayed

    return (
        <div className={styles.commonStyles}>
                 <header>
        <Header />
      </header>
            <div className={styles.searchContainer}>
                <input
                    type="text"
                    placeholder="Search diseases..."
                    value={searchTerm}
                    onChange={handleSearch}
                    className={styles.searchBar}
                />
                <button onClick={handleSearchButtonClick} className={styles.searchButton}>Search</button>
                {showSuggestions && searchTerm && (
                    <div className={styles.suggestions}>
                        {filteredSuggestions.map(suggestion => (
                            <div
                                key={suggestion._id}
                                onClick={() => handleSuggestionClick(suggestion.name)}
                                className={styles.suggestionItem}
                            >
                                {suggestion.name}
                            </div>
                        ))}
                    </div>
                )}
            </div>
            {currentDiseases.length > 0 ? (
                currentDiseases.map(disease => (
                    <div key={disease._id} onClick={() => handleDiseaseClick(disease._id)}>
                        <Disease {...disease} />
                    </div>
                ))
            ) : (
                <p>No diseases found.</p>
            )}
           <div className={styles.pagination}>
    <div className={styles.prevNextButtons}>
        <button
            onClick={handlePreviousPage}
            disabled={currentPage === 1}
            className={`${styles.pageButton} ${currentPage === 1 ? styles.disabled : ''}`}
        >
            Previous
        </button>
        {Array.from({ length: totalPages }, (_, index) => (
            <button
                key={index}
                onClick={() => handlePageChange(index + 1)}
                className={`${styles.pageButton} ${currentPage === index + 1 ? styles.selected : ''}`}
            >
                {index + 1}
            </button>
        ))}
        <button
            onClick={handleNextPage}
            disabled={currentPage === totalPages}
            className={`${styles.pageButton} ${currentPage === totalPages ? styles.disabled : ''}`}
        >
            Next
        </button>
    </div>
</div>

        </div>
    );
}