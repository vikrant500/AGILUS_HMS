import React, { useState, useEffect } from 'react';
import { useLocation, Link } from 'react-router-dom';
import { formatISO9075 } from 'date-fns';
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import styles from './styles/blog.module.css';

export default function IndexPage() {
  const [posts, setPosts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [suggestions, setSuggestions] = useState([]);
  const [tags, setTags] = useState([]);
  const location = useLocation();

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const page = parseInt(params.get('page')) || 1;
    const search = params.get('q') || '';
    setCurrentPage(page);
    setSearchQuery(search);
    fetchPosts(page, search);
    fetchTags(); // Fetch tags when the component mounts
  }, [location.search]);

  useEffect(() => {
    if (searchQuery && !tags.includes(searchQuery)) { // Only fetch suggestions if the search query is not a tag
      fetch(`http://localhost:4000/suggestions?q=${searchQuery}`)
        .then(response => response.json())
        .then(data => setSuggestions(data));
    } else {
      setSuggestions([]);
    }
  }, [searchQuery, tags]);

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

  const fetchTags = async () => {
    try {
      const response = await fetch('http://localhost:4000/tags');
      const data = await response.json();
      setTags(data);
    } catch (error) {
      console.error('Error fetching tags:', error);
    }
  };

  const handleSearch = async (e) => {
    e.preventDefault();
    await fetchPosts(1, searchQuery); // Fetch posts corresponding to the search query
    setCurrentPage(1);
    setSuggestions([]); // Clear suggestions
  };

  const handleSuggestionClick = (suggestion) => {
    setSearchQuery(suggestion.title);
    fetchPosts(1, suggestion.title).then(() => {
      setSuggestions([]); // Hide the dropdown after fetching posts
    });
  };

  const handlePageChange = (page) => {
    fetchPosts(page, searchQuery);
    setCurrentPage(page);
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

  const reloadPage = () => {
    window.location.href = '/blog'; // Reload the blog page
  };

  const handleCategoryClick = (category) => {
    let tag;
    if (category === 'category1') {
      tag = 'game';
    } else if (category === 'category2') {
      tag = 'experiment';
    } else if (category === 'category3'){
      tag = 'wallpaper'
      // Handle other categories
    }
    fetchPosts(1, tag); // Fetch posts with the specified tag
  };

  const handleTagClick = (tag) => {
    fetchPosts(1, tag); // Fetch posts with the specified tag
  };

  const carouselSettings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 2, // Display 2.5 post cards in one horizontal line
    slidesToScroll: 1, // Scroll one post card at a time
    //centerMode: true, // Center the current post card
  };

  return (
    <div className={styles.container}>
      <h1 className={styles.blogTitle}>Blog</h1>
      <div className={styles.headerContainer}>
        <div className={styles.leftContainer}>
          <div className={styles.categories}>
            <div className={`${styles.category} ${location.pathname === '/blog' ? styles.active : ''}`} onClick={reloadPage}>
              View All
            </div>
            <div className={`${styles.category} ${location.pathname === '/category1' ? styles.active : ''}`} onClick={() => handleCategoryClick('category1')}>
              Category 1
            </div>
            <div className={`${styles.category} ${location.pathname === '/category2' ? styles.active : ''}`} onClick={() => handleCategoryClick('category2')}>
              Category 2
            </div>
            <div className={`${styles.category} ${location.pathname === '/category3' ? styles.active : ''}`} onClick={() => handleCategoryClick('category3')}>
              Category 3
            </div>
          </div>
        </div>
        
        <div className={styles.searchBarContainer}>
          <form onSubmit={handleSearch} className={styles.searchForm}>
            <input
              type="text"
              placeholder="Search title or tag..."
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
        </div>
       
      </div>
  
      <div className={styles.mainContent}>
        <div className={styles.leftSidebar}>
          {posts.slice(0, 6).map(post => (
            <Post key={post._id} {...post} className={styles.fullWidthPost} />
          ))}
        </div>
        <div className={styles.rightSidebar}>
          <h1 className={styles.tagHeading}>Tag Cloud</h1>
          <div className={styles.tagsContainer}>
            {['experiment', 'test', 'xyz', 'cars', 'wallpaper', 'movie', 'game', 'influsencer', 'winnerwinnerchickendinner', 'PuneVisitWasLit', 'mongo', 'fuck everything', 'react', 'shit', 'fun', 'exciting', 'whatever tag we can put now', 'hello', 'hi', 'bye', 'action', 'pvp', 'gg'].map((tag, index) => (
              <div key={index} className={styles.tagItem} onClick={() => handleTagClick(tag)}>{tag}</div>
            ))}
            {tags.map((tag, index) => (
              <div key={index} className={styles.tagItem} onClick={() => handleTagClick(tag)}>{tag}</div>
            ))}
          </div>
        </div>
      </div>
  
      {posts.length > 6 && (
        <div className={`${styles.carousel} carousel`}>
          <h1>Featured Posts</h1>
          <Slider {...carouselSettings}>
            {posts.slice(6).map(post => (
              <Post key={post._id} {...post} className={`${styles.carouselPost} carouselPost`} />
            ))}
          </Slider>
        </div>
      )}
  
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

function Post({_id, title, summary, cover, createdAt, author, tags, className}) {
  return (
    <div className={`${styles.post} ${className}`}>
      <div className={styles.image}>
        <Link to={`/post/${_id}`}>
          <img src={`http://localhost:4000/${cover}`} alt="" />
        </Link>
      </div>
      <div className={styles.texts}>
        <Link to={`/post/${_id}`}>
          <h2 className={`${styles.carouselPostTitle}`}>{title}</h2>
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
