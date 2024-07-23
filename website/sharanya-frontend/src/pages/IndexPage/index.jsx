import { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import Post from '../../components/Post';
import styles from './css/CommonStyles.module.css';
import Header from '../../components/Header';
import config from '../../config/config'; // Import the config file

export default function IndexPage() {
  const [posts, setPosts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [suggestions, setSuggestions] = useState([]);
  const [tags] = useState([]);
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    window.scrollTo(0, 0);
    const params = new URLSearchParams(location.search);
    const page = parseInt(params.get('page')) || 1;
    const search = params.get('q') || '';
    setCurrentPage(page);
    setSearchQuery(search);
    fetchPosts(page, search);
  }, [location.search]);

  useEffect(() => {
    if (searchQuery && !tags.includes(searchQuery)) {
      fetch(`${config.apiUrl}/suggestions?q=${searchQuery}`)
        .then(response => response.json())
        .then(data => setSuggestions(data));
    } else {
      setSuggestions([]);
    }
  }, [searchQuery, tags]);

  const fetchPosts = async (page, search) => {
    try {
      const response = await fetch(`${config.apiUrl}/posts?page=${page}&search=${search}`);
      const data = await response.json();
      setPosts(data.posts);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error('Error fetching posts:', error);
    }
  };

  const handleSearch = async (e) => {
    e.preventDefault();
    navigate(`?page=1&q=${searchQuery}`);
    fetchPosts(1, searchQuery);
    setSuggestions([]); // Hide suggestions after search
  };

  const handlePageChange = (page) => {
    navigate(`?page=${page}&q=${searchQuery}`);
    fetchPosts(page, searchQuery);
  };

  const handleSuggestionClick = (suggestion) => {
    setSearchQuery(suggestion.title);
    setSuggestions([]);
    navigate(`?page=1&q=${suggestion.title}`); // Navigate to search results
    fetchPosts(1, suggestion.title); // Fetch posts for the selected suggestion
  };

  const renderPageButtons = () => {
    const buttons = [];
    for (let i = 1; i <= totalPages; i++) {
      buttons.push(
        <button
          key={i}
          className={`px-3 py-1 mx-1 rounded ${i === currentPage ? 'bg-white text-green-500 border border-green-500' : 'bg-green-500 text-white'}`}
          onClick={() => handlePageChange(i)}
        >
          {i}
        </button>
      );
    }
    return buttons;
  };

  return (
    <div className={styles.commonStyles}>
      <header>
        <Header />
      </header>
      <div className={styles.searchContainer}>
        
        <form onSubmit={handleSearch} className={styles.searchForm}>
          <input
            type="text"
            placeholder="Search by title or tags..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className={styles.searchBar}
          />
          <button type="submit" className={styles.searchButton}>
            Search
          </button>
          {suggestions.length > 0 && (
            <ul className={styles.suggestions}>
              {suggestions.map((suggestion, index) => (
                <li
                  key={index}
                  onClick={() => handleSuggestionClick(suggestion)}
                  className={styles.suggestionItem}
                >
                  <strong>{suggestion.title}</strong> by {suggestion.author.username}
                  <br />
                  Tags: {suggestion.tags.join(', ')}
                </li>
              ))}
            </ul>
          )}
        </form>
      </div>
      <div className="space-y-8">
        {posts.map(post => (
          <Post key={post._id} {...post} />
        ))}
      </div>
      <div className="flex items-center justify-center mt-4">
        <button
          disabled={currentPage === 1}
          onClick={() => handlePageChange(currentPage - 1)}
          className="px-3 py-1 mx-1 bg-green-500 text-white rounded disabled:bg-gray-300"
        >
          Previous
        </button>
        {renderPageButtons()}
        <button
          disabled={currentPage === totalPages}
          onClick={() => handlePageChange(currentPage + 1)}
          className="px-3 py-1 mx-1 bg-green-500 text-white rounded disabled:bg-gray-300"
        >
          Next
        </button>
      </div>
    </div>
  );
}