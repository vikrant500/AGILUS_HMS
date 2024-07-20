import Disease from "../Disease";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export default function AboutDisease() {
    const [diseases, setDiseases] = useState([]);
    const [filteredDiseases, setFilteredDiseases] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [showDropdown, setShowDropdown] = useState(false);
    const [searchClicked, setSearchClicked] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage] = useState(10);
    const navigate = useNavigate();

    useEffect(() => {
        fetch('http://localhost:4000/disease')
            .then(response => response.json())
            .then(diseases => {
                setDiseases(diseases);
                if (!searchClicked) {
                    setFilteredDiseases(diseases);
                }
            });
    }, [searchClicked]);

    useEffect(() => {
        if (searchQuery && searchClicked) {
            const results = diseases.filter(disease =>
                disease.name.toLowerCase().includes(searchQuery.toLowerCase())
            );
            setFilteredDiseases(results);
        } else if (!searchQuery) {
            setFilteredDiseases(diseases);
        }
    }, [searchQuery, diseases, searchClicked]);

    // Calculate the current posts based on pagination
    const indexOfLastPost = currentPage * postsPerPage;
    const indexOfFirstPost = indexOfLastPost - postsPerPage;
    const currentPosts = filteredDiseases.slice(indexOfFirstPost, indexOfLastPost);

    const handleDiseaseClick = (id) => {
        navigate(`/disease/${id}`);
    };

    const handleSearchChange = (event) => {
        setSearchQuery(event.target.value);
        setShowDropdown(true);
        if (!event.target.value) {
            setSearchClicked(false);
            setFilteredDiseases(diseases);
        }
    };

    const handleSearchClick = () => {
        setSearchClicked(true);
        setShowDropdown(false);
    };

    const handleDropdownItemClick = (id) => {
        setSearchQuery('');
        setSearchClicked(false);
        setShowDropdown(false);
        navigate(`/disease/${id}`);
    };

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const handlePrevious = () => {
        setCurrentPage(prevPage => Math.max(prevPage - 1, 1));
    };

    const handleNext = () => {
        setCurrentPage(prevPage => Math.min(prevPage + 1, Math.ceil(filteredDiseases.length / postsPerPage)));
    };

    // Generate pagination buttons
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(filteredDiseases.length / postsPerPage); i++) {
        pageNumbers.push(i);
    }

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', width: '100%', position: 'relative' }}>
                <div style={{ display: 'flex', marginBottom: '10px', alignItems: 'center', position: 'relative', width: '400px' }}>
                    <input 
                        type="text" 
                        placeholder="Search diseases..." 
                        value={searchQuery} 
                        onChange={handleSearchChange} 
                        style={{ flex: 1, padding: '8px', marginRight: '10px', borderRadius: '4px', border: '1px solid #ccc' }}
                    />
                    <button 
                        onClick={handleSearchClick} 
                        style={{ padding: '7px 7px', fontSize: '16px', borderRadius: '4px', border: '1px solid #007bff', backgroundColor: 'green', color: '#fff', cursor: 'pointer' }}
                    >
                        Search
                    </button>
                    {showDropdown && searchQuery && (
                        <div style={{ position: 'absolute', top: '100%', left: '0', right: '0', border: '1px solid #ccc', maxHeight: '200px', overflowY: 'auto', backgroundColor: '#fff', zIndex: 10 }}>
                            {diseases.filter(disease => 
                                disease.name.toLowerCase().includes(searchQuery.toLowerCase())
                            ).map(disease => (
                                <div 
                                    key={disease._id} 
                                    onClick={() => handleDropdownItemClick(disease._id)}
                                    style={{ padding: '8px', cursor: 'pointer' }}
                                >
                                    {disease.name}
                                </div>
                            ))}
                        </div>
                    )}
                </div>
            </div>
            <div style={{ padding: '10px', textAlign: 'left' }}>
                {currentPosts.length > 0 ? (
                    currentPosts.map(disease => (
                        <div 
                            key={disease._id} 
                            onClick={() => handleDiseaseClick(disease._id)}
                            style={{ cursor: 'pointer', marginBottom: '10px' }}
                        >
                            <Disease {...disease} />
                        </div>
                    ))
                ) : (
                    <p>No diseases found.</p>
                )}
                <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                    <button 
                        onClick={handlePrevious} 
                        disabled={currentPage === 1} 
                        style={{ padding: '7px 15px', margin: '0 5px', cursor: 'pointer', backgroundColor: 'green', color: '#fff', border: 'none', borderRadius: '4px' }}
                    >
                        Previous
                    </button>
                    {pageNumbers.map(number => (
                        <button 
                            key={number} 
                            onClick={() => handlePageChange(number)} 
                            style={{ padding: '7px 15px', margin: '0 5px', cursor: 'pointer', backgroundColor: currentPage === number ? '#6bbf7c' : 'green', color: '#fff', border: 'none', borderRadius: '4px' }}
                        >
                            {number}
                        </button>
                    ))}
                    <button 
                        onClick={handleNext} 
                        disabled={currentPage === pageNumbers.length} 
                        style={{ padding: '7px 15px', margin: '0 5px', cursor: 'pointer', backgroundColor: 'green', color: '#fff', border: 'none', borderRadius: '4px' }}
                    >
                        Next
                    </button>
                </div>
            </div>
        </>
    );
}
