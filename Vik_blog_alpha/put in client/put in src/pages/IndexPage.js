import { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import Post from '../Post';

export default function IndexPage() {
  const [posts, setPosts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [suggestions, setSuggestions] = useState([]);
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const page = parseInt(params.get('page')) || 1;
    const search = params.get('q') || '';
    setCurrentPage(page);
    setSearchQuery(search);
    fetch(`http://localhost:4000/posts?page=${page}&search=${search}`)
      .then(response => response.json())
      .then(data => {
        setPosts(data.posts);
        setTotalPages(data.totalPages);
      });
  }, [location.search]);

  useEffect(() => {
    if (searchQuery) {
      fetch(`http://localhost:4000/suggestions?q=${searchQuery}`)
        .then(response => response.json())
        .then(data => setSuggestions(data));
    } else {
      setSuggestions([]);
    }
  }, [searchQuery]);

  const handleSearch = (e) => {
    e.preventDefault();
    navigate(`/?page=1&q=${searchQuery}`);
  };

  const handleSuggestionClick = (suggestion) => {
    setSearchQuery(suggestion.title);
    setSuggestions([]);
    navigate(`/?page=1&q=${suggestion.title}`);
  };

  const handlePageChange = (page) => {
    navigate(`/?page=${page}&q=${searchQuery}`);
  };

  const renderPageButtons = () => {
    const buttons = [];
    for (let i = 1; i <= totalPages; i++) {
      buttons.push(
        <button
          key={i}
          className={currentPage === i ? 'selected' : ''}
          onClick={() => handlePageChange(i)}
        >
          {i}
        </button>
      );
    }
    return buttons;
  };

  return (
    <div>
      <h1>Posts</h1>
      <form onSubmit={handleSearch} className="search-form">
        <input
          type="text"
          placeholder="Search by title or tags..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          className="search-bar"
        />
        {suggestions.length > 0 && (
          <ul className="suggestions">
            {suggestions.map((suggestion, index) => (
              <li
                key={index}
                onClick={() => handleSuggestionClick(suggestion)}
                className="suggestion-item"
              >
                <strong>{suggestion.title}</strong> by {suggestion.author.username}
              </li>
            ))}
          </ul>
        )}
        <button type="submit" className="search-button">Search</button>
      </form>
      <div className="posts">
        {posts.map(post => (
          <Post key={post._id} {...post} />
        ))}
      </div>
      <div className="pagination">
        <button
          disabled={currentPage === 1}
          onClick={() => handlePageChange(currentPage - 1)}
        >
          Previous
        </button>
        {renderPageButtons()}
        <button
          disabled={currentPage === totalPages}
          onClick={() => handlePageChange(currentPage + 1)}
        >
          Next
        </button>
      </div>
    </div>
  );
}
