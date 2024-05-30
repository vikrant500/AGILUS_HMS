import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate, Link } from 'react-router-dom';
import { formatISO9075 } from 'date-fns';
import styles from './styles/index.module.css';

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
    fetchPosts(page, search);
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

  const fetchPosts = async (page, search) => {
    try {
      const response = await fetch(`http://localhost:4000/posts?page=${page}&search=${search}`);
      const data = await response.json();
      setPosts(data.posts);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error('Error fetching posts:', error);
    }
  };

  const handleSearch = async (e) => {
    e.preventDefault();
    await fetchPosts(1, searchQuery); // Fetch posts corresponding to the search query
    handleCloseDropdown(); // Close the dropdown
  };

  const handleSuggestionClick = (suggestion) => {
    setSearchQuery(suggestion.title);
    handleCloseDropdown(); // Close the dropdown
    fetchPosts(1, suggestion.title); // Fetch posts corresponding to the clicked suggestion
  };

  const handlePageChange = (page) => {
    fetchPosts(page, searchQuery);
    navigate(`/?page=${page}&q=${searchQuery}`);
  };

  const renderPageButtons = () => {
    const buttons = [];
    for (let i = 1; i <= totalPages; i++) {
      buttons.push(
        <button
          key={i}
          className={currentPage === i ? styles.selected : ''}
          onClick={() => handlePageChange(i)}
        >
          {i}
        </button>
      );
    }
    return buttons;
  };

  const handleCloseDropdown = () => {
    setSuggestions([]);
  };

  return (
    <div>
      <h1>Posts</h1>
      <form onSubmit={handleSearch} className={styles.searchForm}>
        <input
          type="text"
          placeholder="Search by title or tags..."
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          className={styles.searchBar}
        />
        {suggestions.length > 0 && (
          <ul className={styles.suggestions}>
            {suggestions.map((suggestion, index) => (
              <li
                key={index}
                onClick={() => handleSuggestionClick(suggestion)}
                className={styles.suggestionItem}
              >
                <strong>{suggestion.title}</strong> by {suggestion.author.username}
              </li>
            ))}
          </ul>
        )}
        <button type="submit" className={styles.searchButton}>Search</button>
      </form>
      <div className={styles.posts}>
        {posts.map(post => (
          <Post key={post._id} {...post} />
        ))}
      </div>
      <div className={styles.pagination}>
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

function Post({_id, title, summary, cover, createdAt, author, tags}) {
  return (
    <div className={styles.post}>
      <div className={styles.image}>
        <Link to={`/post/${_id}`}>
          <img src={`http://localhost:4000/${cover}`} alt="" />
        </Link>
      </div>
      <div className={styles.texts}>
        <Link to={`/post/${_id}`}>
          <h2>{title}</h2>
        </Link>
        <p className={styles.info}>
          <span className={styles.author}>{author.username}</span>
          <time>{formatISO9075(new Date(createdAt))}</time>
        </p>
        <p className={styles.summary}>{summary}</p>
        <div className={styles.tags}>
          {tags && tags.map((tag, index) => (
            <span key={index} className={styles.tag}>{tag}</span>
          ))}
        </div>
      </div>
    </div>
  );
}
